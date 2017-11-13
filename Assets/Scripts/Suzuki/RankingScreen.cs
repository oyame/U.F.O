using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScreen : MonoBehaviour {
    int i = 0;

	// Use this for initialization
	void Start () {
        RankingDashBoard nasu = GetComponent<RankingDashBoard>();
        nasu.debugmodeRanking();
        nasu.getRanking();
        nasu.screenRanking();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Return)&&i==0)
        {
            RankingDashBoard nasu = GetComponent<RankingDashBoard>();
            nasu.saveRanking(80);
            Debug.Log(nasu.ranking[0]);
            Debug.Log(nasu.ranking[1]);
            Debug.Log(nasu.ranking[2]);
            nasu.screenRanking();
            i++;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            RankingDashBoard nasu = GetComponent<RankingDashBoard>();
            nasu.screenRanking();
        }
        if (Input.GetKey(KeyCode.S))
        {
            RankingDashBoard nasu = GetComponent<RankingDashBoard>();
            nasu.saveRanking(120);
        }
    }
}
