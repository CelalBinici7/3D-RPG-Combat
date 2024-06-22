using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashTime = 0.5f;
    private float currentDashTime;
    private bool isDashing = false;
    private Vector3 dashDirection;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Dash fonksiyonunu �a��rmak i�in �rnek bir giri� kontrol�
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        // Dash i�lemi devam ediyorsa, karakteri ileri do�ru hareket ettir
        if (isDashing)
        {
            characterController.Move(2*dashDirection * dashDistance * Time.deltaTime / dashTime);

            currentDashTime -= Time.deltaTime;

            // Dash s�resi dolarsa, dash i�lemi bitmi� demektir
            if (currentDashTime <= 0)
            {
                isDashing = false;
            }
        }
    }

    void Dash()
    {
        // Dash y�netimi
        if (!isDashing)
        {
            // Dash yap�lacak y�ne karar ver
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDirection =transform.right*horizontalInput + transform.forward*verticalInput;

            // E�er hi�bir tu�a bas�lmam��sa karakterin mevcut y�n�ne dash yap
            if (inputDirection.magnitude == 0)
            {
                dashDirection = transform.forward;
            }
            else
            {
                dashDirection = inputDirection;
            }

            // Dash i�lemi ba�las�n
            isDashing = true;
            currentDashTime = dashTime;
        }
    }
}
