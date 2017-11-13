using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

    //プレイヤー
    [SerializeField]
    Player m_player;

    [SerializeField]
    BrakebleObjManager m_brakebleManager;

    [SerializeField]
    RushGage m_rushGage;

    //吸い込まれる速度
    float m_vacuumSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        if (m_player.m_endFlag)
        {
            SceneManager.LoadScene("Result");
        }

	}

    //吸い込む速度を変える
    public void ChangeVacuumSpeed(float arg_num)
    {
        m_player.ChangeSpeed(arg_num);
        m_brakebleManager.ChangeSpeed(arg_num);
    }
}
