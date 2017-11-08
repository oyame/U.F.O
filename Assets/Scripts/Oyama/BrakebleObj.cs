using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObj : MonoBehaviour {

    GameObject m_circle;
    

    public bool m_activeFlag = true;

	// Use this for initialization
	void Start () {
        m_circle = transform.FindChild("WeakCircle").gameObject;
	}

	// Update is called once per frame
	void Update () {
		
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
            else if (col.transform.tag == "UFO")
            {

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
