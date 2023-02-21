using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target = null;
    public float speed = 180;
    public float distance = 5;
    public float pullBackSpeed = 8;
    public float zoomSpeed = 8;
    private float currentDistance = 0;
    private float heightOffset = 1.5f;


    Vector3 GetTargetPosition()
    {
        return target.position + heightOffset * Vector3.up - new Vector3(0, 0.5f, 0);
    }
    void Update()
    {

        // right drag rotates the camera
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            float dx = -Input.GetAxis("Mouse Y");
            float dy = Input.GetAxis("Mouse X");

            angles.x = Mathf.Clamp(angles.x + dx * speed * Time.deltaTime, 0, 70);
        
            angles.y += dy * speed * Time.deltaTime;
            transform.eulerAngles = angles;
        }

        RaycastHit hit;
        
        if(Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, distance))
        {
            currentDistance = hit.distance;
        }
        else
        {
            currentDistance = Mathf.MoveTowards(currentDistance, distance, Time.deltaTime * pullBackSpeed);
        }
       // currentDistance = Mathf.Clamp(currentDistance - Input.GetAxis("Mouse ScrollWheel") * 1, 2, 10);
        transform.position = GetTargetPosition() - currentDistance * transform.forward;
        

    }

   
}
