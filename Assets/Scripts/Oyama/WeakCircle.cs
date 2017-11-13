using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakCircle : MonoBehaviour {

    BrakebleObj m_bObj;
    Animator m_myAnim;

    RushGage rush;
    Player player;

	// Use this for initialization
	void Start () {

        m_myAnim = GetComponent<Animator>();
        m_myAnim.speed = 0;

        m_bObj = transform.root.GetComponent<BrakebleObj>();

        player = FindObjectOfType<Player>();
        rush = FindObjectOfType<RushGage>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (m_bObj.m_activeFlag)
        {
            if (col.transform.tag == "AttackArea")
            {
                m_bObj.StartCoroutine("Break");

                GameObject eff = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/EF_Broken"), transform.position, Quaternion.identity);
                Destroy(eff, 1);

                if(!player.IsSpecailNow())
                rush.AddGage(0.1f);
            }
        }

    }

    void OnBecameVisible()
    {

        m_myAnim.speed = 1;

    }
}
