using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBeltMovung : MonoBehaviour
{
    private Rigidbody myRigidbody;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float speed;
    private Vector3 currentPosition;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentPosition = myRigidbody.position;
    }

    void FixedUpdate()
    {
        myRigidbody.position += Vector3.forward * speed * Time.fixedDeltaTime;
        myRigidbody.MovePosition(currentPosition);
    }
}
