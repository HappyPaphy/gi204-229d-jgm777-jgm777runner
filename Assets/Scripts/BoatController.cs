using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    [SerializeField] private GameObject spriteKeyboard_A;
    [SerializeField] private GameObject spriteKeyboard_D;
    [SerializeField] private GameObject spriteKeyboard_E;

    [SerializeField] private GameObject gameObj_EnterBoat;
    [SerializeField] private GameObject gameObj_ExitBoat;
    [SerializeField] private GameObject gameObj_BoatPart;

    [SerializeField] private SpriteRenderer sprite_Boat;

    [SerializeField] private int num_BoatPart;
    [SerializeField] private Text text_BoatPart;
    

    private Color alphaBoatColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private float colorBoatAlpha;

    [SerializeField] private SpriteRenderer sprRndr;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool isPlayerInRange = false;
    [SerializeField] private bool isPlayerInBoat = false;

    [SerializeField] private Transform boatStandPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private float boatMoveSpeed;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sprRndr = GetComponent<SpriteRenderer>();

        sprRndr.flipX = false;

        colorBoatAlpha = 0.3f;
        E_ButtonAppear(false);
    }

    void Update()
    {
        alphaBoatColor = new Color(1f, 1f, 1f, colorBoatAlpha);
        sprite_Boat.color = alphaBoatColor;
        text_BoatPart.text = $"{num_BoatPart} / 5";

        if(num_BoatPart == 5)
        {
            colorBoatAlpha = 1f;
        }

        BoatEnterAndExit();
        BoatMovement();
    }

    private void BoatEnterAndExit()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && num_BoatPart == 5)
        {
            if (!isPlayerInBoat)
            {
                isPlayerInBoat = true;
                E_ButtonAppear(true);
            }
            else
            {
                //cannonState = CannonState.Shoot;

                PlayerController.instance.ShootCannon();
                PlayerController.instance.IsPlayerInVehicle = false;
                //StartCoroutine(CannonShootEffectCoroutine());
                E_ButtonAppear(true);

                isPlayerInBoat = false;
            }
        }
    }

    private void BoatMovement()
    {
        if(isPlayerInBoat)
        {
            player.transform.position = boatStandPosition.transform.position;
            player.transform.rotation = boatStandPosition.transform.rotation;

            PlayerController.instance.IsPlayerInVehicle = true;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                sprRndr.flipX = false;
                rb.velocity = new Vector2(-1 * boatMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.left * playerMoveSpeed);
                //playerState = PlayerState.Run;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                sprRndr.flipX = true;
                rb.velocity = new Vector2(1 * boatMoveSpeed, rb.velocity.y);
                //rb.AddForce(Vector2.right * playerMoveSpeed);
                //playerState = PlayerState.Run;
            }
        }
    }

    private void E_ButtonAppear(bool b)
    {
        spriteKeyboard_E.SetActive(b);

        if (num_BoatPart == 5 && isPlayerInBoat)
        {
            gameObj_BoatPart.SetActive(false);
            gameObj_EnterBoat.SetActive(false);
            gameObj_ExitBoat.SetActive(true);
            spriteKeyboard_A.SetActive(true);
            spriteKeyboard_D.SetActive(true);
        }
        else if(num_BoatPart == 5 && !isPlayerInBoat)
        {
            gameObj_BoatPart.SetActive(false);
            gameObj_EnterBoat.SetActive(true);
            gameObj_ExitBoat.SetActive(false);
            spriteKeyboard_A.SetActive(false);
            spriteKeyboard_D.SetActive(false);
        }
        else if(num_BoatPart < 5 && !isPlayerInBoat)
        {
            gameObj_BoatPart.SetActive(true);
            gameObj_EnterBoat.SetActive(false);
            gameObj_ExitBoat.SetActive(false);
            spriteKeyboard_A.SetActive(false);
            spriteKeyboard_D.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            E_ButtonAppear(true);
            Debug.Log("Player Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            E_ButtonAppear(false);
            Debug.Log("Player Exit");
        }
    }
}
