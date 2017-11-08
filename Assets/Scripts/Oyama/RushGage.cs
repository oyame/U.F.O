using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RushGage : MonoBehaviour {

    Image m_image;

	// Use this for initialization
	void Start () {

        m_image = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
        m_image.fillAmount += 0.0001f;
    }

    //加算
    public void AddGage(float arg_num)
    {
        m_image.fillAmount += arg_num;
    }

    //必殺技は使えるか？
    public bool IsSpecialAttack()
    {
        if (m_image.fillAmount >= 1)
            return true;
        else return false;
    } 
}
