using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private Vector3 startPoint;

    [SerializeField]
    private float goalZCoord = 4.5f;

    [SerializeField]
    private float approachSpeed = 10f;
    private bool goToGoal = false;

    private Rigidbody rigidbody;
    bool isDropped = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        rigidbody.useGravity = false;
        startPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        goToGoal = true;
        isDropped = false;
    }

    private void OnMouseDrag()
    {
        gameObject.transform.forward = Vector3.one;
        gameObject.transform.rotation = new Quaternion();
        if (!goToGoal)
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = goalZCoord;
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }

    private void OnMouseUp()
    {
        isDropped = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = true;
    }

    private void Update()
    {
        if (goToGoal)
        {
            GoingToTheGoal();
        }
    }

    private void GoingToTheGoal()
    {
        startPoint.z -= Time.deltaTime * approachSpeed;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(startPoint);
        if (startPoint.z <= goalZCoord)
        {
            goToGoal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDropped)
        {
            if (other.transform.tag == "Container")
            {
                isDropped = false;
                StartCoroutine(GoToContainerCoroutine(transform.position, other.transform.position));
                StartCoroutine(SqueezeCoroutine());
                StartCoroutine(DestroyCoroutine());
            }
        }
    }

    private IEnumerator GoToContainerCoroutine(Vector3 startPosition, Vector3 goal)
    {
        float t = 0;
        while (t <= 1)
        {
            yield return transform.position = Vector3.Lerp(startPosition, goal, t);
            t += Time.deltaTime * 2;
        }
    }

    private IEnumerator SqueezeCoroutine()
    {
        float t = 0;
        while (t <= 1)
        {
            yield return transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            t += Time.deltaTime * 2;
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
