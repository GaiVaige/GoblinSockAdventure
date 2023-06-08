using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_SCript_2 : MonoBehaviour
{
    Rigidbody rb;
    public bool isGrounded;
    public float playerSittingHeight;
    public float groundCheckDistance;
    public float playerRideHeight;
    public float springStrength;
    public float springDamper;
    public bool rayDidHit;

    public float moveSpeed;
    public float dragSpeed;
    public float horizontalInput;
    public float verticalInput;
    public float jumpForce;
    float airDampFactor;
    public Transform orientation;
    public Transform modelOrientation;
    public Animator anim;
    Vector3 moveDirection;
    public float jumpTimeOut;
    public float jumpTimer;
    public bool canJump;
    public float postJumpTimeOut; // checks if player can jump after already jumping
    public float postJumpTimer;
    public bool isJumping;

    [Header("Camera")]
    public GameObject cameraGrab;
    public float horizontalCamInput;
    public float verticalCamInput;
    public float camTurnSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, Vector3.down, out rayHit))
        {
            rayDidHit = true;
        }

        if(Physics.Raycast(transform.position, Vector3.down, playerSittingHeight * .5f + groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        if (rayDidHit)
        {
            Vector3 vel = rb.velocity;
            Vector3 rayDirection = transform.TransformDirection(Vector3.down);
        
            Vector3 otherVelocity = Vector3.zero;
            Rigidbody hitBody = rayHit.rigidbody;
            if(hitBody != null)
            {
                otherVelocity = hitBody.velocity;
            }
        
            float rayDirVel = Vector3.Dot(rayDirection, vel);
            float otherDirVel = Vector3.Dot(rayDirection, otherVelocity);

            float relVel = rayDirVel - otherDirVel;

            float x = rayHit.distance - playerRideHeight;
        
            float springForce = (x * springStrength) - (relVel * springDamper);
            Debug.Log(springForce);


            if (!isJumping)
            {
                rb.AddForce(rayDirection * springForce);
            }

       
        }
        GetInput();
        DoMovement();
    }

    public void DoMovement()
    {
        moveDirection = (orientation.forward * verticalInput) * airDampFactor + (orientation.right * horizontalInput) * airDampFactor;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);


        if (isGrounded && ((horizontalInput == 0) && (verticalInput == 0)))
        {
            rb.drag = dragSpeed;
            airDampFactor = 1f;

        }
        else
        {
            rb.drag = 0f;
            airDampFactor = 2f;
        }
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0) || (verticalInput != 0) && !isJumping)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalCamInput = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalCamInput = 1;
        }
        else
        {
            horizontalCamInput = 0;
        }



        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            isJumping = true;
            canJump = false;
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpTimer = 0;


        }

        if (isJumping)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Force);
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpTimeOut)
            {
                isJumping = false;
                jumpTimer = 0;
            }
            
        }

        if (!isJumping && !canJump)
        {
            postJumpTimer += Time.deltaTime;

            if (postJumpTimer >= postJumpTimeOut)
            {
                canJump = true;
                postJumpTimer = 0;
            }
        }

        orientation.transform.Rotate(0, horizontalCamInput * camTurnSpeed, 0, 0);
        modelOrientation.transform.Rotate(0, horizontalCamInput * camTurnSpeed, 0, 0);
        cameraGrab.transform.Rotate(0, horizontalCamInput * camTurnSpeed, 0);


    }
}
