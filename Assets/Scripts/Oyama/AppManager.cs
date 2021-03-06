﻿//担当：森田　勝
//概要：アプリケーション内のパブリック機能および
//　　　パブリックなマネージャーを管理しているクラス
//　　　ユーティリティを使用するためにはこのクラスにアクセスする
//参考：なし

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    #region Singleton実装
    private static AppManager m_instance;

    float score = 0;

    public static AppManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("AppManager").AddComponent<AppManager>();
                m_instance.Initialize();
            }
            return m_instance;
        }
    }

    private AppManager() { }
    #endregion

    //フェード環境
    public Fade m_fade { get; private set; }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize()
    {

        if (!m_fade)
        {
            m_fade = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/FadeCanvas")).GetComponentInChildren<Fade>();
        }
        m_fade.Initialize();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(m_fade.transform.parent.gameObject);
    }

    public void SetScore(float arg_score)
    {
        score = arg_score;
    }

    public float GetScore()
    {
        return score;
    }
}
