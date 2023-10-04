using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D frontPerascopeCollider;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        frontPerascopeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FlipFacing();
    }

    void FlipFacing()
    {
        moveSpeed *= -1;
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }
}
