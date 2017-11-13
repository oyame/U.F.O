using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    bool unko = false;

    void Start()
    {
        Screen.SetResolution(1280, 1024, Screen.fullScreen);
        AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !unko)
        {
            unko = true;
            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
        }

        if (unko && !AppManager.Instance.m_fade.IsFading())
        {
            SceneManager.LoadScene("Main");
        }

    }
}
