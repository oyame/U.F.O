using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //初期の吸い込まれ具合
    float m_playerVacuumSpeed = 0.01f;

    //左右の移動力
    float m_horizontalSpeed = 0.03f;

    //一回の下降量
    float m_HunbariNum = -0.1f;

    //最終的な主人公の移動量
    float m_resuleSpeed = 0;

    //ステート
    enum State {

        Idle,
        Attack,
        Damage,
        Special,
        Stop

    }

    State state;

    //被ダメ時に復帰するまでの最大時間　被ダメごとに増える　下降ボタン連打で短縮できる
    float m_respawnTime = 1;

    //リジッドボデェ
    Rigidbody2D m_rigid;

    //攻撃範囲のコライダー
    GameObject m_attackArea;

    Vector3 m_attackAreaPos, m_attackAreaScale;

    //攻撃時間
    float m_attackTime = 0.5f;

    //必殺技展開時間
    float m_specailAttackTime = 2;

    //必殺技エフェクト
    GameObject m_specialEffect;

    //コライダー
    CapsuleCollider2D m_myCollider;

    //経過時間格納庫
    float m_Time = 0; 

    //アニメーターコントローラー
    Animator m_anim;

    [SerializeField]
    RushGage m_rushGage;

    // Use this for initialization
    void Start () {
        m_rigid = GetComponent<Rigidbody2D>();
        m_attackArea = transform.FindChild("AttackArea").gameObject;

        m_attackAreaPos = m_attackArea.transform.position;
        m_attackAreaScale = m_attackArea.transform.localScale;

        m_attackArea.SetActive(false);

        m_specialEffect = transform.FindChild("EF_specailAttack").gameObject;
        m_specialEffect.SetActive(false);

        m_anim = GetComponent<Animator>();

        m_myCollider = GetComponent<CapsuleCollider2D>();

        state = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {

        m_attackArea.transform.position = transform.position + new Vector3(0,-0.8f,0);

        switch (state)
        {

            case State.Idle:

                transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * m_horizontalSpeed, 0));

                //zx連打で下降
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    m_resuleSpeed += m_HunbariNum;
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    m_resuleSpeed += m_HunbariNum;
                }

                

                //エンターでパンチ
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //音

                    if (m_rushGage.IsSpecialAttack())
                    {
                        state = State.Special;
                        m_specialEffect.SetActive(true);
                        m_myCollider.enabled = false;
                    }
                    else {
                        state = State.Attack;
                    }

                    m_anim.SetBool("Attack",true);
                    m_anim.SetBool("Idle", false);
                }
                break;

            case State.Attack:

                transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * m_horizontalSpeed, 0));

                m_Time += Time.deltaTime;

                if (m_Time > 0.1f)
                    m_attackArea.SetActive(true);

                if(m_Time > m_attackTime)
                {
                    state = State.Idle;
                    
                    m_attackArea.SetActive(false);
                    m_anim.SetBool("Attack", false);
                    m_anim.SetBool("Idle", true);

                    m_Time = 0;
                }

                break;

            case State.Special:

                //左右移動二倍速
                transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * (m_horizontalSpeed * 2), 0));

                m_Time += Time.deltaTime;

                m_resuleSpeed -= 0.3f;

                if (m_Time > 0.1f)
                {
                    m_attackArea.SetActive(true);
                    m_attackArea.transform.position = transform.position;
                    m_attackArea.transform.localScale = new Vector3(10,3,1);
                }

                if (m_Time > m_specailAttackTime)
                {
                    state = State.Idle;
                    m_specialEffect.SetActive(false);

                    //攻撃範囲を元に戻す
                    m_attackArea.transform.position = m_attackAreaPos;
                    m_attackArea.transform.localScale = m_attackAreaScale;

                    m_myCollider.enabled = true;

                    m_rushGage.AddGage(-1);

                    m_attackArea.SetActive(false);
                    m_anim.SetBool("Attack", false);
                    m_anim.SetBool("Idle", true);

                    m_Time = 0;
                }

                break;

            case State.Damage:
                //zx連打で復帰を早める
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    m_Time -= 0.1f;
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    m_Time -= 0.1f;
                }

                m_Time -= Time.deltaTime;

                if(m_Time <= 0)
                {

                    m_anim.SetBool("Damage", false);
                    m_anim.SetBool("Idle", true);

                    m_Time = 0;

                    state = State.Idle;
                }

                break;
        }

        Vacuum();

        Clamp();

    }

    //吸い込み関数　stateがStopの時以外に吸い込む
    void Vacuum()
    {
        
        m_resuleSpeed += m_playerVacuumSpeed;

        if (state != State.Stop)
        {
            
            m_rigid.MovePosition((Vector2)transform.position + new Vector2(0, m_resuleSpeed));
        }

        m_resuleSpeed = 0;
    }

    //ダメージ関数
    public void Damage()
    {
        m_anim.SetBool("Attack", false);
        m_anim.SetBool("Idle", false);
        m_anim.SetBool("Damage", true);

        //必殺技中に被ダメしたら…
        m_attackArea.transform.position = m_attackAreaPos;
        m_attackArea.transform.localScale = m_attackAreaScale;
        m_specialEffect.SetActive(false);

        m_rushGage.AddGage(-0.01f);

        m_Time = m_respawnTime;

        m_respawnTime += 0.5f;

        state = State.Damage;

    }

    //移動制限
    void Clamp()
    {
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // プレイヤーの座標を取得
        Vector2 pos = transform.position;

        // プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, min.y + 2, max.y);

        // 制限をかけた値をプレイヤーの位置とする
        transform.position = pos;
    }

    public void ChangeSpeed(float arg_num)
    {
        m_playerVacuumSpeed += arg_num;
    }
}
