using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Died
};


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpForce;

    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator playerAnim;

    [SerializeField] private SpriteRenderer sprRndr;
    [SerializeField] private Rigidbody2D rb;

    private bool isOnGround = true;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sprRndr = GetComponent<SpriteRenderer>();
        Animator playerAnim = GetComponent<Animator>();

        sprRndr.flipX = false;
    }

    void Update()
    {
        ChooseAnimation();
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = false;
        }
    }

    private void PlayerMovement()
    {
        if (isOnGround)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                sprRndr.flipX = true;
                rb.velocity = new Vector2(-1 * playerMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.left * playerMoveSpeed);
                playerState = PlayerState.Run;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                sprRndr.flipX = false;
                rb.velocity = new Vector2(1 * playerMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.right * playerMoveSpeed);
                playerState = PlayerState.Run;
            }
            else
            {
                playerState = PlayerState.Idle;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerState = PlayerState.Jump;
                rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
                //rb.AddForce(Vector2.up * playerJumpForce);
            }
        }
        else
        {
            /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerState = PlayerState.Jump;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                //rb.AddForce(Vector2.up * playerJumpForce);
            }*/
        }
    }

    private void ChooseAnimation()
    {
        playerAnim.SetBool("IsIdle", false);
        playerAnim.SetBool("IsRun", false);
        playerAnim.SetBool("IsJump", false);
        

        switch (playerState)
        {
            case PlayerState.Idle:
                playerAnim.SetBool("IsIdle", true);
                break;
            case PlayerState.Run:
                playerAnim.SetBool("IsRun", true);
                break;
            case PlayerState.Jump:
                playerAnim.SetBool("IsJump", true);
                break;
        }
    }
}
