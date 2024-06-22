using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRigidbody : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1f, 20f)]
    public float rotationSpeed;
    float normalPov;
    public float sprintPov;
    public float StrafeTurnSpeed;

    float inputX;
    float inputY;
    float maxSpeed;


    public Transform Model;
    Animator anim;
    Camera mainCam;

    Vector3 StickDirection;

    Rigidbody rb;
    public float normalHeightCollider;
    public float jumpheightCollider = 0.4f;
    public float timeJump;
    CapsuleCollider capsuleCollider;
    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.C;

    public enum MovementType
    {
        Directional,
        Strafe
    }

    public MovementType movementType;

    
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalPov = mainCam.fieldOfView;
        rb = GetComponent<Rigidbody>();
        normalHeightCollider = capsuleCollider.height;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Movemetn();

       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            rb.AddForce(transform.forward * 20f * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("roll");
        }
    }
    public void Movemetn()
    {

        if (movementType == MovementType.Strafe)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            
           
            //Strafe blender tree
            
            anim.SetFloat("iX",inputX,damp,Time.deltaTime *10);
            anim.SetFloat("iY",inputY,damp,Time.deltaTime *10);
            anim.SetBool("strafeMoving",true);
            
            var hareketEdiyor = inputX!=0||inputY!=0;

            if (hareketEdiyor)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0),StrafeTurnSpeed * Time.fixedDeltaTime);
                anim.SetBool("strafeMoving", true);
            }
            else
            {
                anim.SetBool("strafeMoving", false);
            }

        }
        if (movementType == MovementType.Directional)
        {
            InputMove();

            InputRotation();
            StickDirection = new Vector3(inputX, 0, inputY);
            if (Input.GetKey(SprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, sprintPov, Time.deltaTime * 2);
                maxSpeed = 2f;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(WalkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalPov, Time.deltaTime * 2);

                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalPov, Time.deltaTime * 2);

                maxSpeed = 1f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");

            }
        }


       

       
    }

    public void InputMove()
    {
        anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp ,10 * Time.deltaTime);
        
    }
    public void InputRotation() {
        Vector3 rotOffset  = mainCam.transform.TransformDirection(StickDirection);
        rotOffset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward,rotOffset,rotationSpeed * Time.deltaTime);
    }

   
}
