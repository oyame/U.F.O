using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakebleObjManager : MonoBehaviour {

    //オブジェの吸い込まれる速さ
    float m_objVacuumSpeed = 0.03f;

    //オブジェ生成のインターバル
    float m_createTime = 2;
    
    [SerializeField]
    GameObject[] m_objs;

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

            int rand = Random.Range(0, m_objs.Length);

            GameObject obj = Instantiate(m_objs[rand], new Vector3(Random.Range(-4.0f, 4.0f), -7, 0), Quaternion.identity);
            obj.GetComponent<BrakebleObj>().SetSpeed(m_objVacuumSpeed);
            obj.transform.parent = transform;


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
