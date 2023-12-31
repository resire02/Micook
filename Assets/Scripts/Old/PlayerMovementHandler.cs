using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementHandler : MonoBehaviour
{
    [Header("Camera")]
    public GameObject fp;
    public GameObject tp;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    private AudioHandler aux;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
    bool grounded;
    bool platformed;

    public Transform orientation;
    public KeyCode sprint;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        aux = FindObjectOfType<AudioHandler>();

        readyToJump = true;

        Physics.gravity = new Vector3(0f, -9.8f * 3f, 0f);
    }

    private void Update()
    {
        if(fp.activeSelf || tp.activeSelf){
            Vector3 boxCenter = transform.position - new Vector3(0f, playerHeight * 0.5f, 0f);
            Vector3 halfExtents = transform.localScale * .75f;
            grounded = Physics.CheckBox(boxCenter, halfExtents, transform.rotation, whatIsGround);
            platformed = Physics.CheckBox(boxCenter, halfExtents, transform.rotation, whatIsPlatform);

            // Debug.Log(grounded);

            MyInput();
            SpeedControl();

            // handle drag
            if (grounded || platformed)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
        else{
            horizontalInput = 0;
            verticalInput = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
                
        if(Input.GetKeyDown(sprint) && (grounded || platformed))
        {
            moveSpeed *= 3;
        }
        else if (Input.GetKeyUp(sprint) && (grounded || platformed)){
            moveSpeed /= 3;
        }

        // Debug.Log(readyToJump + " " + grounded);

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && (grounded || platformed))
        {
            readyToJump = false;

            Jump();

            aux.PlaySound("Jump");

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded || platformed)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded || platformed)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}