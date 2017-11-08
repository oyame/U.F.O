using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    private static MainManager instance = null;
    public static MainManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("MainManager");
                instance = obj.AddComponent<MainManager>();
            }
            return instance;
        }
    }

    //プレイヤー
    [SerializeField]
    Player m_player;

    //吸い込まれる速度
    float m_vacuumSpeed = 0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    //吸い込む速度を変える
    void ChangeVacuumSpeed()
    {

    }
}
