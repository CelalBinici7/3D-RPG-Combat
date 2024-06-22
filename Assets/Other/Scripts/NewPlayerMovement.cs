using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    private NewControls playerInput;
    Rigidbody Rigidbody;
    private float speed = 5;

    Vector2 movementInput;
    Vector3 currentPos;
    bool isMovementPressed;
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    void Awake()
    {
        playerInput = new();
        playerInput.PlayerController.Movement.started += Move;
        playerInput.PlayerController.Movement.performed += Move;
        playerInput.PlayerController.Movement.canceled += Move;
    }
    void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        currentPos.x = movementInput.x;
        currentPos.z = movementInput.y;
        isMovementPressed = movementInput.x !=0 || movementInput.y!=0;
    }
     void FixedUpdate()
    {
        if (isMovementPressed)
        {
            Rigidbody.MovePosition(transform.position + currentPos.normalized * speed * Time.deltaTime); ;
        }
    }
    private void OnEnable()
    {
        playerInput.PlayerController.Enable();
    }
    private void OnDisable()
    {
        playerInput.PlayerController.Disable();
    }
}
