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
    float horizontalInput;
    float verticalInput;
    public float jumpForce;
    public Transform orientation;
    Vector3 moveDirection;

    [Header("Camera")]
    public GameObject cameraGrab;
    public float horizontalCamInput;
    public float verticalCamInput;
    public float camTurnSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        
            rb.AddForce(rayDirection * springForce);
       
        }
        GetInput();
    }

    public void DoMovement()
    {

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
}
