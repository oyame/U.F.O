using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

    //プレイヤー
    [SerializeField]
    Player m_player;

    [SerializeField]
    BrakebleObjManager m_brakebleManager;

    //必殺ゲージ
    [SerializeField]
    RushGage m_rushGage;

    //Goのあれ
    [SerializeField]
    GameObject m_startObj;

    //UFOからでてるエフェクト
    [SerializeField]
    GameObject m_catleEfect;

    //朝夕暮れ夜へと遷移する神秘のイルミネーション
    [SerializeField]
    Animator m_BGAnim;

    //スコア
    [SerializeField]
    Text m_score;

    float m_scoreBuff = 0;

    //吸い込まれる速度
    float m_vacuumSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);

        m_player.enabled = false;
        m_brakebleManager.enabled = false;
        m_rushGage.enabled = false;

        StartCoroutine("StartObj");
    }
	
	// Update is called once per frame
	void Update () {

        //終了
        if (m_player.m_endFlag)
        {
            SceneManager.LoadScene("Result");
            AppManager.Instance.SetScore(m_scoreBuff);
        }

	}

    //吸い込む速度を変える
    public void ChangeVacuumSpeed(float arg_num)
    {
        m_player.ChangeSpeed(arg_num);
        m_brakebleManager.ChangeSpeed(arg_num);
    }

    //スコア加算
    public void AddScore(float arg_num)
    {
        m_scoreBuff += arg_num;
        m_score.text = m_scoreBuff.ToString();
    }

    IEnumerator StartObj()
    {

        GameObject Go = m_startObj.transform.FindChild("SP_PlayerFace_Go").gameObject;

        int i = 0;

        while (true) {

            yield return new WaitForSeconds(1);

            if (i == 3) break;

            m_startObj.transform.FindChild("SP_PlayerFace_" + i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            i++;

            
        }

        while (Go.transform.position.y < 10)
        {
            Go.transform.position += new Vector3(0,0.2f,0);

            yield return null;
        }

        m_player.enabled = true;
        m_brakebleManager.enabled = true;
        m_rushGage.enabled = true;
        m_BGAnim.enabled = true;

        m_catleEfect.GetComponent<ParticleSystem>().Play();

        Destroy(m_startObj);

    }
}
