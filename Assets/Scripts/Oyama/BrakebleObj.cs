using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObj : MonoBehaviour {

    GameObject m_circle;

    GameObject UFO;

    public bool m_activeFlag = true;

    public float m_myPoint = 0;

    //Managerから値をセットされる
    float m_speed = 0;

    //キャッシュ
    Transform m_trans;

	// Use this for initialization
	void Start () {
        UFO = GameObject.FindGameObjectWithTag("UFO");
        m_circle = transform.FindChild("WeakCircle").gameObject;
        m_trans = transform;

    }

	// Update is called once per frame
	void Update () {

        if(m_activeFlag)
            m_trans.position += new Vector3(0, m_speed, 0);

        if (m_trans.position.y > UFO.transform.position.y)
        {
            m_activeFlag = false;
            m_trans.localScale -= new Vector3(0.1f, 0.1f, 0);

            if (m_trans.localScale.x < 0)
            {
                FindObjectOfType<MainManager>().ChangeVacuumSpeed(0.002f);
                UFO.transform.FindChild("SP_UFO(仮)").GetComponent<Huwahuwa>().num += 2;
                UFO.transform.FindChild("EF_Catle").GetComponent<ParticleSystem>().playbackSpeed += 0.2f;

                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_speedUp"), Camera.main.transform.position);

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
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/SE/SE_touki_break"), Camera.main.transform.position);
                StartCoroutine("Break");
            }
        }

    }

    IEnumerator Break()
    {

        

        if (transform.GetComponent<Huwahuwa>())
        {
            transform.GetComponent<Huwahuwa>().enabled = false;
        }
        if (transform.GetComponent<Rotate>())
        {
            transform.GetComponent<Rotate>().enabled = false;
        }

        m_circle.SetActive(false);
        m_activeFlag = false;

        GameObject left = m_trans.FindChild("Left").gameObject;
        GameObject right = m_trans.FindChild("Right").gameObject;

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

    public void SetSpeed(float arg_speed)
    {
        m_speed = arg_speed;
    }
}
