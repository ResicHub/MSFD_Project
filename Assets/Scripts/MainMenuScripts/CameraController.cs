using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 cameraStartPosition;
    private Quaternion cameraStartRotation;
    private float wTimer = 0;

    private void Start()
    {
        cameraStartPosition = transform.position;
        cameraStartRotation = transform.rotation;
    }

    public void Move(Vector3 goalPosition, Vector3 goalRotation)
    {
        wTimer = 0;
        StartCoroutine(MovingCoroutine(cameraStartPosition, goalPosition));
        StartCoroutine(RotatingCoroutine(cameraStartRotation, Quaternion.Euler(goalRotation)));
        //transform.position = goalPosition;
        //transform.rotation = Quaternion.Euler(goalRotation);
    }

    public void MoveBack(Vector3 presentPosition, Quaternion presentRotation)
    {
        wTimer = 0;
        StartCoroutine(MovingCoroutine(presentPosition, cameraStartPosition));
        StartCoroutine(RotatingCoroutine(presentRotation, cameraStartRotation));
        //transform.position = cameraStartPosition;
        //transform.rotation = cameraStartRotation;
    }

    private IEnumerator MovingCoroutine(Vector3 startPosition, Vector3 goalPosition)
    {
        while (wTimer <= 0 || transform.position != goalPosition)
        {
            yield return transform.position = Vector3.Lerp(startPosition, goalPosition, wTimer);
            wTimer += Time.deltaTime;
        }
    }

    private IEnumerator RotatingCoroutine(Quaternion startRotation, Quaternion goalRotation)
    {
        while (wTimer <= 0 || transform.rotation != goalRotation)
        {
            yield return transform.rotation = Quaternion.Lerp(startRotation, goalRotation, wTimer);
            wTimer += Time.deltaTime;
        }
    }
}
