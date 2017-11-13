using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
