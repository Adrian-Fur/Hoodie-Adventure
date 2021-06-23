using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        myAnimator.SetBool("Running", false);
        if (Input.GetAxis("Horizontal") != 0)
        {
            myAnimator.SetBool("Running", true);
        }
    }
    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsJumping", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsJumping", false);
            return;
        }
    }
    private void FlipSprite()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }
}
