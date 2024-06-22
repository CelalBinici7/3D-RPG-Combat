using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Axel
{
    Front,
    Rear
}
[Serializable]
public struct Wheel
{
    public GameObject wheelModel;
    public WheelCollider wheelCollider;
    public GameObject whellEffect;
    public ParticleSystem smoke;
    public Axel axel;

}
public class CarController : MonoBehaviour
{

    public float maxAcceleration = 200f;
    public float breakeAcceleration = 50f;

    public List<Wheel> wheels;

    float InputX, InputY;
    float moveInput;
    float steerInput;

    public float turnSensivity = 1f;
    public float maxSteerAngle = 45f;

    public Vector3 _centerOfMass  ;
    private Rigidbody carRb ;

    public float HealthCar;
    void Start()
    {
        carRb = GetComponent<Rigidbody>(); 
        carRb.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        wheelsTurn();
        GetInputs();
      

     
    }
    void LateUpdate()
    {
        Move();
        sterr();
        carbreak(); 
        WheelEffects();
        VelocitySeetings();
    }

    void GetInputs()
    {
        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");

        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {

            wheel.wheelCollider.motorTorque =-InputY *1000* maxAcceleration * Time.deltaTime;
        }
    }

    public void sterr()
    {
        foreach (var item in wheels)
        {
            if (item.axel == Axel.Front)
            {
                var _steerAngle = InputX * turnSensivity * maxSteerAngle;
                item.wheelCollider.steerAngle = Mathf.Lerp(item.wheelCollider.steerAngle,_steerAngle,0.6f);
            }
        }
    }

    void wheelsTurn()
    {
        foreach (var wheel in wheels)
        {
          

                Quaternion _rot;
                Vector3 _pos;
                wheel.wheelCollider.GetWorldPose(out _pos, out _rot);
                wheel.wheelModel.transform.position = _pos;
                wheel.wheelModel.transform.rotation = _rot;
        }
    }

    void carbreak()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 10000;// ne kadar fazla o kadar sert
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;// ne kadar fazla o kadar sert
            }
        }
    } 

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel ==Axel.Rear && wheel.wheelCollider.isGrounded&&carRb.velocity.magnitude>=10f)
            {
                wheel.whellEffect.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smoke.Emit(1);
            }
            else
            {
                wheel.whellEffect.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }

    public void VelocitySeetings()
    {
        if (InputY ==0 && InputX ==0)
        {
            if (carRb.velocity.magnitude>2f)
            {
                if (carRb.velocity.z>0)
                {
                    foreach (var wheel in wheels)
                    {

                        wheel.wheelCollider.motorTorque = -0.2f * 1000 * maxAcceleration * Time.deltaTime;
                    }
                }
                else
                {
                    foreach (var wheel in wheels)
                    {

                        wheel.wheelCollider.motorTorque = 0.2f * 1000 * maxAcceleration * Time.deltaTime;
                    }
                }
              
            }
          
        }
    }
}
