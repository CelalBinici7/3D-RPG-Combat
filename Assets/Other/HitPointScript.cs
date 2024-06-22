using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            collision.transform.GetComponent<CarController>().HealthCar -= collision.transform.GetComponent<CarController>().HealthCar;
            print(collision.transform.GetComponent<CarController>().HealthCar);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
