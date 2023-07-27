using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public HUD boost;
    public bool isInGameplay;
    public float boostSpeed;
    public bool isBoosting;
    public float horizontalInput;
    public float verticalInput;
    public float jumpForce;
    float airDampFactor;
    public Transform orientation;
    Vector3 moveDirection;
    Vector3 strafeDirection;
    public int jumpTimeOut;
    public float turnTimer;
    public bool canJump;
    public float postJumpTimeOut; // checks if player can jump after already jumping
    public float postJumpTimer;
    public bool isJumping;
    public float camTurnSpeed;
    public float strafeForce;
    public int strafeMultiplier;
    public Vector3 speed;

    public Camera playerCamFOV;
    public float viewClamper;
    public float camTurnLimiter;

    public GameObject modelObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
        speed = rb.velocity;
        speed.x = moveDirection.x;
    }

    // Update is called once per frame
    void Update()
    {


        playerCamFOV.fieldOfView = 80 * (1 + rb.velocity.magnitude/viewClamper);


        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit))
        {
            rayDidHit = true;
        }

        if (Physics.Raycast(transform.position, Vector3.down, playerSittingHeight * .5f + groundCheckDistance))
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
            if (hitBody != null)
            {
                otherVelocity = hitBody.velocity;
            }

            float rayDirVel = Vector3.Dot(rayDirection, vel);
            float otherDirVel = Vector3.Dot(rayDirection, otherVelocity);

            float relVel = rayDirVel - otherDirVel;

            float x = rayHit.distance - playerRideHeight;

            float springForce = (x * springStrength) - (relVel * springDamper);


            if (!isJumping)
            {
                rb.AddForce(rayDirection * springForce);
            }


        }
        GetInput();

        if (isInGameplay)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                boost.canBoost = true;
                isBoosting = true;
            }
        }




        
        
        if (Input.GetKey(KeyCode.A) && ((modelObject.transform.eulerAngles.z <= 52f || modelObject.transform.eulerAngles.z >= 307f)))
        {
                modelObject.transform.Rotate(0, 0, 1 * camTurnLimiter * Time.deltaTime);

        }



        if (Input.GetKey(KeyCode.D) && ((modelObject.transform.eulerAngles.z <= 52f || modelObject.transform.eulerAngles.z >= 307f)))
        {
            modelObject.transform.Rotate(0, 0, -1 * camTurnLimiter * Time.deltaTime);

        }

        if (!(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) && modelObject.transform.eulerAngles.z != 0f)
        {
            modelObject.transform.Rotate(0, 0, -modelObject.transform.rotation.z * camTurnLimiter * 2 * Time.deltaTime);
        }

        if (!(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) && modelObject.transform.eulerAngles.z == 0f)
        {
            modelObject.transform.eulerAngles.Set(0, 0, 0);
        }

    }

    private void FixedUpdate()
    {
        DoMovement();
        DoBoost();
    }

    public void DoMovement()
    {
        moveDirection = (orientation.forward * verticalInput) * airDampFactor;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        if (verticalInput == 0 && horizontalInput == 0)
        {
            rb.drag = dragSpeed * 1.2f;
        }
        else
        {
            rb.drag = dragSpeed;
            airDampFactor = 1f;
        }

        transform.Rotate(0, horizontalInput * camTurnSpeed, 0);




        strafeDirection = (orientation.right * strafeForce * strafeMultiplier) * airDampFactor;
        rb.AddForce(strafeDirection, ForceMode.Force);

        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.A))
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

        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = -1;
        }
        else if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 1;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 0;
        }
        else if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 0;
        }
        else if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            strafeMultiplier = 0;
        }



    }

    void DoBoost()
    {
        if (isBoosting)
        {
            if (boost.boostChagesRemaining >= 0)
            {
                    rb.AddForce(moveDirection * boostSpeed, ForceMode.Force);

            }

        }

        if(boost.canBoost == false)
        {
            isBoosting = false;
        }


    }
}
