using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimScript : MonoBehaviour
{
    public Transform debugTransform;
    public LayerMask aimMask;

    public Transform CameraRoot;
    public Transform Cameraa;
    public float mouseSensivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray , out RaycastHit hit,400f,aimMask))
        {

            debugTransform.position = Vector3.Lerp(debugTransform.position,hit.point,4f*Time.deltaTime);
          //  debugTransform.position=Vector3.MoveTowards(debugTransform.position, hit.point, 0.2f);
           
        }

       
    }
    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        Cameraa.transform.position = CameraRoot.transform.position;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        Cameraa.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX *1.5f);
    }
}
