using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : MonoBehaviour
{
    public float power;
    Rigidbody enemyRb;
    Rigidbody carRb;
    Vector3 pos;
    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }
   /* private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("enemy"))
        {
            print("b");
            enemyRb = other.transform.GetComponent<Rigidbody>();

            if (carRb.velocity.magnitude > 0.2f)
            {

                print("a");
                enemyRb.AddForce(carRb.velocity * power * Time.deltaTime, ForceMode.Impulse);
                other.transform.GetComponent<Animator>().enabled = false;
                if (other.transform.GetComponent<CapsuleCollider>())
                {
                    other.transform.GetComponent<CapsuleCollider>().enabled = false;

                }
                if (other.transform.GetComponent<CharacterController>())
                {
                    other.transform.GetComponent<CharacterController>().enabled = false;

                }



            }
        }
    }
   */
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("enemy"))
        {
            if (GetComponent<Rigidbody>().velocity.magnitude>7f)
            {
                other.transform.GetComponent<ZombieScript>().pos = carRb.velocity;

                other.transform.GetComponent<ZombieScript>().EnableRagdoll();
            }
          
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("hitPoint"))
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > 7f)
            {
              //  collision.transform.GetComponent<ZombieScript>().pos = carRb.velocity;

                collision.transform.GetComponentInParent<ZombieScript>().EnableRagdoll();
            }
        }
    }

}
