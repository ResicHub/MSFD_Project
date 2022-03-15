using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private Vector3 startPoint;
    private float mZCoord;
    [SerializeField]
    private float goalZCoord = 4.5f;
    [SerializeField]
    private float approachSpeed = 10f;
    private bool goToGoal = false;

    private void OnMouseDown()
    {
        
        startPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        goToGoal = true;
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
}
