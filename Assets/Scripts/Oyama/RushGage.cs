using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RushGage : MonoBehaviour {

    Image m_image;
    Animator m_anim;

    [SerializeField]
    GameObject m_maxEffect;
    bool m_flag = false;

	// Use this for initialization
	void Start () {

        m_image = GetComponent<Image>();
        m_anim = GetComponent<Animator>();

        m_maxEffect.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        m_image.fillAmount += 0.0001f;

        if(m_image.fillAmount >= 1 && !m_flag)
        {
            m_image.color = new Color(1, 1, 1, 1);
            m_anim.speed = 0;

            m_maxEffect.SetActive(true);

            m_flag = true;
        }

        if (m_image.fillAmount < 1)
        {
            m_flag = false;
            m_maxEffect.SetActive(false);
            m_anim.speed = 1;
        }
    }

    //加算
    public void AddGage(float arg_num)
    {
        m_image.fillAmount += arg_num;
    }

    //セット
    public void SetGage(float arg_num)
    {
        m_image.fillAmount = arg_num;
    }

    //必殺技は使えるか？
    public bool IsSpecialAttack()
    {
        if (m_image.fillAmount >= 1)
            return true;
        else return false;
    } 
}
