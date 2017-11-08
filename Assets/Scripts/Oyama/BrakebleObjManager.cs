using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObjManager : MonoBehaviour {

    //現在出しているオブジェのリスト
    List<BrakebleObj> m_objList = new List<BrakebleObj>();

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
		
        if(m_objList.Count != 0)
        {

            foreach(var obj in m_objList)
            {
                if (obj.m_activeFlag)
                {
                    obj.transform.position += new Vector3(0, m_objVacuumSpeed,0);
                }
            }

        }

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
                    m_objList.Add(obj.GetComponent<BrakebleObj>());
                    break;

                case 1:
                    GameObject obj2 = Instantiate(m_middle, new Vector3(Random.Range(-4.0f, 4.0f), -7, 0), Quaternion.identity);
                    m_objList.Add(obj2.GetComponent<BrakebleObj>());
                    break;


                case 2:
                    GameObject obj3 = Instantiate(m_big, new Vector3(Random.Range(-4.0f, 4.0f), -7, 0), Quaternion.identity);
                    m_objList.Add(obj3.GetComponent<BrakebleObj>());
                    break;
            }
            

        }

    }


}
