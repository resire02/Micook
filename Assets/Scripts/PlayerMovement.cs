using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool jumpReady;

    Vector3 moveDirection;
    Rigidbody player;

    [Header("Ground Detection")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask groundPresence;
    bool grounded;

    [Header("Key Controls")]
    public KeyCode jumpKey = KeyCode.Space;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
    }

    private void FixedUpdate() 
    {
        MovePlayer();    
    }

    private void Update() 
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundPresence);

        ReadKeyboardInput();
        CapSpeed();

        if(grounded)
            player.drag = groundDrag;
        else
            player.drag = 0;
    }

    /// registers keyboard info
    private void ReadKeyboardInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    /// update player position
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
 
        if(grounded)
            player.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            player.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
    }

    /// clamps players speed to a max set speed
    private void CapSpeed()
    {
        Vector3 flatVelocity = new Vector3(player.velocity.x, 0f, player.velocity.z);

        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            player.velocity = new Vector3(limitedVelocity.x, player.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        player.velocity = new Vector3(player.velocity.x, 0f, player.velocity.z);

        player.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }
}
