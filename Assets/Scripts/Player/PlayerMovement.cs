using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 6f;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
    }

    public void HandleMove(Vector2 input)
    {
        Vector3 move = Vector3.zero;
        move.x = input.x;
        move.z = input.y;
        controller.Move(transform.TransformDirection(move) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0) playerVelocity.y = -1f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded) playerVelocity.y = Mathf.Sqrt(gravity * jumpHeight * -1f);
    }
    
}
