using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform carTarget;

    void HandleMOvement()
    {
        Vector3 targetPos= new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position =  Vector3.Lerp(transform.position,targetPos,moveSmoothness * Time.deltaTime);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMOvement();
        HandleRotation();
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position;
        var rotaion = new Quaternion();
        rotaion = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotaion,rotSmoothness * Time.deltaTime);

    }
   
}
