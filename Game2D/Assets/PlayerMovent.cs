using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovent : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float jumpHeight = 5.0f;

    private float horizontalInput = 0f;
    private enum MovementState {idle,running,jump,falling }

    Rigidbody2D rbody;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rbody=GetComponent<Rigidbody2D>();
        capsuleCollider=GetComponent<CapsuleCollider2D>();
        animator=GetComponent<Animator>();
        sprite=GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Rigibody
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 movementVector = new Vector2(horizontalInput * movementSpeed * 100 * Time.deltaTime, rbody.velocity.y);
        rbody.velocity = movementVector;
        Debug.Log(Time.deltaTime);

        //UpdateAnimationUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAnimationUpdate();
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }


    public void UpdateAnimationUpdate()
    {
        MovementState state;
        if (horizontalInput > 0.0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (horizontalInput < 0.0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rbody.velocity.y > .1f)
        {
            state = MovementState.jump;
        } 
        else if (rbody.velocity.y < -.1f) 
        {
            state= MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }
}
