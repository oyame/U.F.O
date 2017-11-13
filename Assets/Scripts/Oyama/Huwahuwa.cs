using UnityEngine;
using System.Collections;

public class Huwahuwa : MonoBehaviour
{

    public float num = .2f;
    float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(startX + (Mathf.Sin(Time.time * num) / 5),
         transform.position.y, transform.position.z);


    }
}


