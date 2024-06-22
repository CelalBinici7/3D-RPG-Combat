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
        // Dash fonksiyonunu çaðýrmak için örnek bir giriþ kontrolü
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        // Dash iþlemi devam ediyorsa, karakteri ileri doðru hareket ettir
        if (isDashing)
        {
            characterController.Move(2*dashDirection * dashDistance * Time.deltaTime / dashTime);

            currentDashTime -= Time.deltaTime;

            // Dash süresi dolarsa, dash iþlemi bitmiþ demektir
            if (currentDashTime <= 0)
            {
                isDashing = false;
            }
        }
    }

    void Dash()
    {
        // Dash yönetimi
        if (!isDashing)
        {
            // Dash yapýlacak yöne karar ver
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDirection =transform.right*horizontalInput + transform.forward*verticalInput;

            // Eðer hiçbir tuþa basýlmamýþsa karakterin mevcut yönüne dash yap
            if (inputDirection.magnitude == 0)
            {
                dashDirection = transform.forward;
            }
            else
            {
                dashDirection = inputDirection;
            }

            // Dash iþlemi baþlasýn
            isDashing = true;
            currentDashTime = dashTime;
        }
    }
}
