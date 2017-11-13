using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObj : MonoBehaviour {

    GameObject m_circle;

    GameObject UFO;

    public bool m_activeFlag = true;

	// Use this for initialization
	void Start () {
        UFO = GameObject.FindGameObjectWithTag("UFO");
        m_circle = transform.FindChild("WeakCircle").gameObject;
	}

	// Update is called once per frame
	void Update () {

        if (transform.position.y > UFO.transform.position.y)
        {
            m_activeFlag = false;
            transform.localScale -= new Vector3(0.1f, 0.1f, 0);

            if (transform.localScale.x < 0)
            {
                FindObjectOfType<MainManager>().ChangeVacuumSpeed(0.001f);
                UFO.transform.FindChild("SP_UFO(仮)").GetComponent<Huwahuwa>().num += 3;
                UFO.transform.FindChild("EF_Catle").GetComponent<ParticleSystem>().playbackSpeed += 0.2f;

                Destroy(gameObject);
            }

        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (m_activeFlag)
        {

            if (col.transform.tag == "Player")
            {
                col.GetComponent<Player>().Damage();
                StartCoroutine("Break");
            }
        }

    }

    IEnumerator Break()
    {
        m_circle.SetActive(false);
        m_activeFlag = false;

        GameObject left = transform.FindChild("Left").gameObject;
        GameObject right = transform.FindChild("Right").gameObject;

        float addY = 0;

        while (addY < 5)
        {
            left.transform.position -= new Vector3(0.1f,addY,0);
            right.transform.position -= new Vector3(-0.1f, addY, 0);

            addY += Time.deltaTime;

            yield return null;
            
            
        }
        Destroy(this.gameObject);
    }
}
