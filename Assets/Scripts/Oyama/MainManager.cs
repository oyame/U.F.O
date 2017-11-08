using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    //プレイヤー
    [SerializeField]
    Player m_player;

    [SerializeField]
    BrakebleObjManager m_brakebleManager;

    //吸い込まれる速度
    float m_vacuumSpeed = 0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    //吸い込む速度を変える
    public void ChangeVacuumSpeed(float arg_num)
    {
        m_player.ChangeSpeed(arg_num);
        m_brakebleManager.ChangeSpeed(arg_num);
    }
}
