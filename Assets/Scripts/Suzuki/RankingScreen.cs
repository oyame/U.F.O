using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScreen : MonoBehaviour {

    RankingDashBoard nasu;

    // Use this for initialization
    void Start () {
        nasu = GetComponent<RankingDashBoard>();
        //nasu.debugmodeRanking();
        nasu.getRanking();

        nasu.saveRanking(AppManager.Instance.GetScore());
        nasu.screenRanking();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RankingDashBoard nasu = GetComponent<RankingDashBoard>();
            nasu.screenRanking();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //RankingDashBoard nasu = GetComponent<RankingDashBoard>();
            nasu.saveRanking(120);
        }

        //リセット
        if (Input.GetKeyDown(KeyCode.D))
        {
            nasu.debugmodeRanking();
            nasu.getRanking();
        }
    }
}
