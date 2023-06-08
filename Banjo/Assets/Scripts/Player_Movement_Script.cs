using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Movement_Script : MonoBehaviour
{

    Rigidbody rb;
    public float moveSpeed;
    public float dragSpeed;
    float horizontalInput;
    float verticalInput;
    public float jumpForce;
    public bool isHoldingSomething;
    public Transform orientation;
    Vector3 moveDirection;

    [Header("Climbing")]
    public bool canClimb;
    public float climbSpeed;
    public float checkDistance;

    [Header("Grounded")]
    public float playerHeight;
    public bool isGrounded;
    public LayerMask ground;
    float airDampFactor;
    public float checkHeight;


    [Header("Camera")]
    public GameObject cameraGrab;
    public float horizontalCamInput;
    public float verticalCamInput;
    public float camTurnSpeed;



    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        DoCameraRotation();
        CheckIfGrounded();
        ClimbingHandle();
    }

    public void FixedUpdate()
    {
        DoMovement();
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalCamInput = -1 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalCamInput = 1 * Time.deltaTime;
        }
        else
        {
            horizontalCamInput = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        orientation.transform.Rotate(0, horizontalCamInput * camTurnSpeed, 0, 0);

    }

    public void DoMovement()
    {

        moveDirection = (orientation.forward * verticalInput) * airDampFactor + (orientation.right * horizontalInput) * airDampFactor;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);


        if (isGrounded)
        {
            rb.drag = dragSpeed;
            airDampFactor = 1f;

        }
        else
        {
            rb.drag = 0f;
            airDampFactor = .001f;
        }
    }

    public void DoCameraRotation()
    {
        cameraGrab.transform.Rotate(0, horizontalCamInput * camTurnSpeed, 0);

    }

    public void CheckIfGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + checkHeight))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }

    public void ClimbingHandle()
    {
        if(Physics.Raycast(transform.position, Vector3.forward, playerHeight * .1f + checkDistance))
        {
            canClimb = true;
        }
        else
        {
            canClimb = false;
        }
    }
}
