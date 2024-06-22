using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class aimStateScript : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    float rotationY;
    float rotationX;
    [SerializeField] float mouseSensivity;

    [SerializeField] float minVerticleAngle = -45f;
    [SerializeField] float maxVerticleAngle = 45f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }
    private void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minVerticleAngle, maxVerticleAngle);

        rotationY += Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;

        var targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);
        camFollowPos.rotation = targetRotation;
    }
}
