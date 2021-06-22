using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        myAnimator.SetBool("Running", false);


    }
    private void FlipSprite()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            myAnimator.SetBool("Running", true);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            myAnimator.SetBool("Running", true);
        }
    }
}
