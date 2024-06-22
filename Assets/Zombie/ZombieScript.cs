using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{

    public enum ZombieState
    {
        Walking,
        Ragdoll
    }
    public Rigidbody[] _ragdollRigidbodies;
    private ZombieState _state = ZombieState.Walking;
    private Animator anim;
    private CharacterController characterController;
    Rigidbody rb;
    public Vector3 pos;
    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
       
        anim = GetComponent<Animator>();
        DisableRagdoll();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*  switch (_state)
          {

              case ZombieState.Walking:
                  WalkingBehaviour();
                  break;

              case ZombieState.Ragdoll:
                  RagdollBehaviour();
                  break;
          }*/


    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            if (rigidbody!=null)
            {
                rigidbody.isKinematic = true;
            }
          
        }
        anim.enabled = true;
       // characterController.enabled = true;
    }

    public void EnableRagdoll()
    {
      
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            if (rigidbody != null)
            {
                rigidbody.isKinematic = false;
            }
        }
        anim.enabled = false;
     




        //characterController.enabled = false;
    }

    public void WalkingBehaviour()
    {
        DisableRagdoll();
        _state = ZombieState.Walking;
    }

    public void RagdollBehaviour()
    {
        EnableRagdoll();
        _state = ZombieState.Ragdoll;
    }
    
}
