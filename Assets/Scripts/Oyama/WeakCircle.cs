using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakCircle : MonoBehaviour {

    BrakebleObj m_bObj;
    Animator m_myAnim;

	// Use this for initialization
	void Start () {

        m_myAnim = GetComponent<Animator>();
        m_myAnim.speed = 0;

        m_bObj = transform.root.GetComponent<BrakebleObj>();

	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (m_bObj.m_activeFlag)
        {
            if (col.transform.tag == "AttackArea")
            {
                m_bObj.StartCoroutine("Break");
            }
        }

    }

    void OnBecameVisible()
    {

        m_myAnim.speed = 1;

    }
}
