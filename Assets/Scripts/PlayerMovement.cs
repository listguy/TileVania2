using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider2D;
    float gravity;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravity = playerRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
    }

    void ClimbLadder()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;

        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            if (playerRigidBody.gravityScale == 0) playerRigidBody.gravityScale = gravity;
            animator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, moveInput.y * climbSpeed);
        playerRigidBody.velocity = climbVelocity;
        playerRigidBody.gravityScale = 0;
        if(playerHasVerticalSpeed)
            animator.SetBool("isClimbing", true);
        else
        {
            animator.SetBool("isClimbing", false);
        }
    }
}
