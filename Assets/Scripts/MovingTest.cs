using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTest : MonoBehaviour
{
    private Transform myTransform;
    private Rigidbody myRigidbody;
    [Range(0.1f, 10f)]
    public float speed;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 newPosition = myTransform.position + new Vector3(0,0,Time.deltaTime*speed*-1f);
        myTransform.position = newPosition;
    }
}
