using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemetn : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float  Distance = 5;
    [SerializeField] float mouseSensivity;

    [SerializeField] float minVerticleAngle =-45f;
    [SerializeField] float maxVerticleAngle =45f;

    float rotationY;
    float rotationX;


    
    public void Update()
    {
        rotationX+= Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minVerticleAngle, maxVerticleAngle);

        rotationY += Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        
        var targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);
        transform.position = followTarget.position - targetRotation * new Vector3(0f,0f,Distance);
        transform.rotation = targetRotation;
    

    }
}
