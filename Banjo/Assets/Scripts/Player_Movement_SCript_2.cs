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
    Vector3 moveDirection;
    Vector3 strafeDirection;
    public float jumpTimeOut;
    public float jumpTimer;
    public bool canJump;
    public float postJumpTimeOut; // checks if player can jump after already jumping
    public float postJumpTimer;
    public bool isJumping;
    public float camTurnSpeed;
    public float strafeForce;
    public int strafeMultiplier;
    public Vector3 speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
        speed = rb.velocity;
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

    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    public void DoMovement()
    {
        moveDirection = (orientation.forward * verticalInput) * airDampFactor;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);


        if ((horizontalInput != 0) && (verticalInput != 0) && verticalInput != 0)
        {
            rb.drag = dragSpeed;
            airDampFactor = 1f;

        }
        else if (verticalInput == 0 && horizontalInput == 0)
        {
            rb.drag = dragSpeed * 1.2f;
        }

        transform.Rotate(0, horizontalInput * camTurnSpeed, 0);

        strafeDirection = (orientation.right * strafeForce * strafeMultiplier) * airDampFactor;
        rb.AddForce(strafeDirection, ForceMode.Force);

        if(Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.A))
        {
            rb.drag = 1;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Q))
        {
            rb.drag = 1;
        }

    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = -1;
        }
        else if(!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 1;
        }    
        else if(Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 0;
        }



    }
}
