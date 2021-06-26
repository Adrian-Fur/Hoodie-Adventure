using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        ClimbLadder();
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
    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
        if (Input.GetAxis("Vertical") != 0)
        {
            myAnimator.SetBool("Climbing", true);
        }
    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing")))
        {
            myAnimator.SetBool("IsJumping", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing")))
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
