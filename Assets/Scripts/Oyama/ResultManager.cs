using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    bool unpo = false;

	// Use this for initialization
	void Start () {
        AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            unpo = true;
            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
            
        }

        if(unpo && !AppManager.Instance.m_fade.IsFading())
        {
            SceneManager.LoadScene("Title");
        }
	}
}
