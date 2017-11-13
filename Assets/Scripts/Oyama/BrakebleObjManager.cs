using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObjManager : MonoBehaviour {

    //オブジェの吸い込まれる速さ
    float m_objVacuumSpeed = 0.03f;

    //オブジェ生成のインターバル
    float m_createTime = 2;

    [SerializeField]
    GameObject m_small, m_middle, m_big;

	// Use this for initialization
	void Start () {

        StartCoroutine("CreateObj");

	}
	
	// Update is called once per frame
	void Update () {
		

	}

    IEnumerator CreateObj()
    {

        while (true)
        {

            yield return new WaitForSeconds(m_createTime);

            int rand = Random.Range(0, 3);

            switch (rand)
            {
                case 0:
                    GameObject obj = Instantiate(m_small, new Vector3(Random.Range(-4.0f,4.0f), -7, 0), Quaternion.identity);
                    obj.GetComponent<BrakebleObj>().SetSpeed(m_objVacuumSpeed);
                    obj.transform.parent = transform;
                    break;

                case 1:
                    GameObject obj2 = Instantiate(m_middle, new Vector3(Random.Range(-4.0f, 4.0f), -7, 0), Quaternion.identity);
                    obj2.GetComponent<BrakebleObj>().SetSpeed(m_objVacuumSpeed);
                    obj2.transform.parent = transform;
                    break;


                case 2:
                    GameObject obj3 = Instantiate(m_big, new Vector3(Random.Range(-4.0f, 4.0f), -7, 0), Quaternion.identity);
                    obj3.GetComponent<BrakebleObj>().SetSpeed(m_objVacuumSpeed);
                    obj3.transform.parent = transform;
                    break;
            }
            

        }

    }


    public void ChangeSpeed(float arg_num)
    {
        m_objVacuumSpeed += arg_num;

        foreach(Transform child in transform)
        {
            child.GetComponent<BrakebleObj>().SetSpeed(m_objVacuumSpeed);
        }
    }

}
