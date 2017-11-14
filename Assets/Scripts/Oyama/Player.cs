using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //軽量化を図るため、Transformをキャッシュ
    Transform m_trans;

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
        Dead

    }

    State state;

    //被ダメ時に復帰するまでの最大時間　被ダメごとに増える　下降ボタン連打で短縮できる
    float m_respawnTime = 1;

    //リジッドボデェ
    Rigidbody2D m_rigid;

    //攻撃範囲のコライダー
    GameObject m_attackArea;

    //キャッシュ
    Transform m_attackAreaTransform;

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

    float DeadLine;

    [HideInInspector]
    public bool m_endFlag;

    MainManager m_manager;

    // Use this for initialization
    void Start () {

        m_trans = transform;

        m_rigid = GetComponent<Rigidbody2D>();
        m_attackArea = m_trans.FindChild("AttackArea").gameObject;

        m_attackAreaTransform = m_attackArea.transform;

        m_attackAreaPos = m_attackAreaTransform.position;
        m_attackAreaScale = m_attackAreaTransform.localScale;

        m_attackArea.SetActive(false);

        m_specialEffect = m_trans.FindChild("EF_specailAttack").gameObject;
        m_specialEffect.SetActive(false);

        m_anim = GetComponent<Animator>();

        m_myCollider = GetComponent<CapsuleCollider2D>();

        DeadLine = GameObject.FindGameObjectWithTag("UFO").transform.position.y;

        m_endFlag = false;

        m_manager = FindObjectOfType<MainManager>();

        state = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {

        m_attackAreaTransform.position = m_trans.position + new Vector3(0,-0.8f,0);

        switch (state)
        {

            case State.Idle:

                m_trans.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * m_horizontalSpeed, 0));

                //zx連打で下降
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                {
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_mogaku"), Camera.main.transform.position);

                    m_resuleSpeed += m_HunbariNum;
                    m_manager.AddScore(1);
                }

                //エンターでパンチ
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //音

                    if (m_rushGage.IsSpecialAttack())
                    {
                        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_final_attack"), Camera.main.transform.position);

                        state = State.Special;
                        m_specialEffect.SetActive(true);
                        m_myCollider.enabled = false;
                    }
                    else {

                        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_middle_punch1"), Camera.main.transform.position);

                        m_resuleSpeed += m_HunbariNum * 3;
                        state = State.Attack;
                    }

                    m_anim.SetBool("Attack",true);
                    m_anim.SetBool("Idle", false);
                }
                break;

            case State.Attack:

                m_trans.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * m_horizontalSpeed, 0));

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
                m_trans.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * (m_horizontalSpeed * 2), 0));

                m_Time += Time.deltaTime;

                m_rushGage.AddGage(-(1 / m_specailAttackTime)*Time.deltaTime);

                m_resuleSpeed -= 0.1f;

                if (m_Time > 0.1f)
                {
                    m_attackArea.SetActive(true);
                    m_attackAreaTransform.position = m_trans.position;
                    m_attackAreaTransform.localScale = new Vector3(10,3,1);
                }

                if (m_Time > m_specailAttackTime)
                {
                    state = State.Idle;
                    m_specialEffect.SetActive(false);

                    //攻撃範囲を元に戻す
                    m_attackAreaTransform.position = m_attackAreaPos;
                    m_attackAreaTransform.localScale = m_attackAreaScale;

                    m_myCollider.enabled = true;

                    m_rushGage.SetGage(0);

                    m_attackArea.SetActive(false);
                    m_anim.SetBool("Attack", false);
                    m_anim.SetBool("Idle", true);

                    m_Time = 0;
                }

                break;

            case State.Damage:
                
                //zx連打で復帰を早める
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                {
                    AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_mogaku"), Camera.main.transform.position);
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

            case State.Dead:

                m_Time += Time.deltaTime;

                if(m_Time > 1f)
                {
                    Debug.Log("in");
                    AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);

                    Invoke("GameEnd",1);

                    //糞みたいな再発防止策
                    m_Time = -100;
                }

                

                break;
        }

        if(m_trans.position.y > DeadLine && state != State.Dead)
        {
            
            //ゲームオーバー
            m_anim.SetBool("Idle", false);
            m_anim.SetBool("Attack", false);
            m_anim.SetBool("Special", false);
            m_anim.SetBool("Damage", false);
            m_specialEffect.SetActive(false);
            m_attackArea.SetActive(false);

            m_myCollider.enabled = false;

            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_Dead"), Camera.main.transform.position);

            m_anim.SetTrigger("Dead");

            //リザルト遷移用
            m_Time = 0;


            state = State.Dead;
        }

        Vacuum();

        Clamp();

    }

    //吸い込み関数
    void Vacuum()
    {
        
        m_resuleSpeed += m_playerVacuumSpeed;

        m_rigid.MovePosition(((Vector2)m_trans.position + new Vector2(0, m_resuleSpeed)));

        m_resuleSpeed = 0;
    }

    //ダメージ関数
    public void Damage()
    {
        m_anim.SetBool("Attack", false);
        m_anim.SetBool("Idle", false);
        m_anim.SetBool("Damage", true);

        //必殺技中に被ダメしたら…
        m_attackAreaTransform.position = m_attackAreaPos;
        m_attackAreaTransform.localScale = m_attackAreaScale;
        m_specialEffect.SetActive(false);

        m_attackArea.SetActive(false);

        m_rushGage.AddGage(-0.01f);

        m_Time = m_respawnTime;

        m_respawnTime += 0.5f;

        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_damage"), Camera.main.transform.position);

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
        Vector2 pos = m_trans.position;

        // プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, min.y + 2, max.y);

        // 制限をかけた値をプレイヤーの位置とする
        m_trans.position = pos;
    }

    //必殺技中かどうかを取得
    public bool IsSpecailNow()
    {
        if (state == State.Special) return true;
        else return false;
    }

    //すばやい
    public void ChangeSpeed(float arg_num)
    {
        m_playerVacuumSpeed += arg_num;
    }

    //終わる
    void GameEnd()
    {
        m_endFlag = true;
    }

}
