using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCntrl : MonoBehaviour
{
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}
