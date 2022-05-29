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
    private bool isMoving;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentPosition = myRigidbody.position;
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            myRigidbody.position += Vector3.forward * speed * Time.fixedDeltaTime;
            myRigidbody.MovePosition(currentPosition);
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetMovement(bool value)
    {
        isMoving = value;
    }
}
