using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    float x;
    float z;
    float gravity = -9.81f;
    Vector3 velocity;
    public float speed= 12f;
    public float jumoHeight = 3f;

    public Transform groundCheck;
    public float checkDistance = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, groundMask);

        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumoHeight * -2f * gravity);
        }
        Vector3 move = transform.right * x + transform.forward  *z;
        characterController.Move(move * speed *Time.deltaTime);

        velocity.y += gravity *  Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }


   /* public void InputRotation()
    {
        Vector3 rotOffset = mainCam.transform.TransformDirection(StickDirection);
        rotOffset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOffset, rotationSpeed * Time.deltaTime);
    }*/
}
