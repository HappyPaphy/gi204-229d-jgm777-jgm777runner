using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.LightAnchor;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    ShootByCannon,
    Died
};


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private float cannonShootForce;

    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator playerAnim;

    [SerializeField] private SpriteRenderer sprRndr;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float time;

    public bool IsPlayerInVehicle { get { return isPlayerInVehicle; } set { isPlayerInVehicle = value; } }

    public static PlayerController instance;

    [SerializeField] private int curHP;
    public int CurHP { get { return curHP; } set { curHP = value; } }

    [SerializeField] public int maxHP = 100;
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }

    private bool isOnGround = true;
    private bool isFootStepCooldown = false;
    private bool isPlayerRunning = false;
    private bool isPlayerJumping = false;
    private bool isPlayerInVehicle = false;
    private bool isPlayerDied = false;
    private bool isPlayerWin = false;


    void Start()
    {
        instance = this;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sprRndr = GetComponent<SpriteRenderer>();
        Animator playerAnim = GetComponent<Animator>();

        sprRndr.flipX = false;
    }

    void Update()
    {
        ChooseAnimation();
        PlayerMovement();
        CheckPlayerDied();

        if (!isFootStepCooldown && isPlayerRunning 
             && !isPlayerInVehicle && !isPlayerDied && !isPlayerWin)
        {
            StartCoroutine(FootStepCoroutine());
        }

        /*if(playerState == PlayerState.Run)
        {
            time += Time.deltaTime;
            if(time >= 30f)
            {
                SoundManager.instance.PlayerRunSound();
                time = 0f;
            }
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Vehicle")
        {
            isPlayerJumping = false;
            rb.rotation = 0f;
            isOnGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water") && !isPlayerInVehicle)
        {
            curHP -= 1;
            rb.mass = 5f;
            Debug.Log("Player Dying");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("House"))
        {
            isPlayerWin = true;
            WinLoseUI.instance.YouWin();
            SoundManager.instance.WinSound();
        }

        if (other.gameObject.CompareTag("Water"))
        {
            SoundManager.instance.WaterSound();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Vehicle")
        {
            isOnGround = false;
        }
    }

    private void PlayerMovement()
    {
        if (isOnGround && !isPlayerInVehicle && !isPlayerDied)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                isPlayerRunning = true;
                sprRndr.flipX = true;
                rb.velocity = new Vector2(-1 * playerMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.left * playerMoveSpeed);
                playerState = PlayerState.Run;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                isPlayerRunning = true;
                sprRndr.flipX = false;
                rb.velocity = new Vector2(1 * playerMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.right * playerMoveSpeed);
                playerState = PlayerState.Run;
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isPlayerRunning = false;
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                isPlayerRunning = false;
            }

            if(!isPlayerRunning && !isPlayerJumping && !isPlayerDied && !isPlayerWin)
            {
                playerState = PlayerState.Idle;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isPlayerJumping = true;
                SoundManager.instance.JumpSound();
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

    public void ShootCannon()
    {
        playerState = PlayerState.ShootByCannon;
        rb.AddForce(transform.up * cannonShootForce, ForceMode2D.Impulse);
    }

    private void ChooseAnimation()
    {
        playerAnim.SetBool("IsIdle", false);
        playerAnim.SetBool("IsRun", false);
        playerAnim.SetBool("IsJump", false);
        playerAnim.SetBool("IsShootByCannon", false);
        playerAnim.SetBool("IsDied", false);


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
            case PlayerState.ShootByCannon:
                playerAnim.SetBool("IsShootByCannon", true);
                break;
            case PlayerState.Died:
                playerAnim.SetBool("IsDied", true);
                break;
        }
    }

    private IEnumerator FootStepCoroutine()
    {
        isFootStepCooldown = true;
        SoundManager.instance.PlayerRunSound();
        yield return new WaitForSeconds(0.2f);
        isFootStepCooldown = false;
    }
    
    private void CheckPlayerDied()
    {
        if(curHP <= 0)
        {
            SoundManager.instance.PlayerDiedSound();
            SoundManager.instance.LoseSound();
            WinLoseUI.instance.YouLost();
            playerState = PlayerState.Died;
            isPlayerDied = true;
        }
    }                 
}
