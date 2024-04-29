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

    private SpriteRenderer sprite_Boat;

    [SerializeField] private GameObject[] boat;
    [SerializeField] private int num_CurBoatPart = 0;
    [SerializeField] private Text text_BoatPart;
    
    private Color alphaBoatColor = new Color(1f, 1f, 1f, 1f);
    private float colorBoatAlpha = 0.2f;

    [SerializeField] private SpriteRenderer sprRndr;
    [SerializeField] private Rigidbody2D rb;

    private bool isPlayerInRange = false;
    private bool isPlayerInBoat = false;
    private bool isBoatFinished = false;

    [SerializeField] private Transform boatStandPosition;
    [SerializeField] private Transform boatExitPosition;

    [SerializeField] private GameObject player;
    [SerializeField] private float boatMoveSpeed;

    [SerializeField] private CollectibleManager collectibleManager;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sprRndr = GetComponent<SpriteRenderer>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sprRndr.flipX = false;

        gameObj_BoatPart.SetActive(false);
        gameObj_EnterBoat.SetActive(false);
        gameObj_ExitBoat.SetActive(false);
        spriteKeyboard_A.SetActive(false);
        spriteKeyboard_D.SetActive(false);

        colorBoatAlpha = 0.2f;
        isPlayerInRange = false;
        num_CurBoatPart = 0;
    }

    void Update()
    {
        alphaBoatColor = new Color(1f, 1f, 1f, colorBoatAlpha);
        sprRndr.color = alphaBoatColor;
        text_BoatPart.text = $"{num_CurBoatPart} / 5";

        CalculateBoatAlpha();

        if (isPlayerInRange)
        {
            E_ButtonAppear();
            BoatEnterAndExit();
            BoatMovement();
            CraftBoat();
        }

        
    }

    private void CalculateBoatAlpha()
    {
        switch (num_CurBoatPart)
        {
            case 0:
                colorBoatAlpha = 0.3f;
                break;
            case 1:
                colorBoatAlpha = 0.4f;
                break;
            case 2:
                colorBoatAlpha = 0.5f;
                break;
            case 3:
                colorBoatAlpha = 0.6f;
                break;
            case 4:
                colorBoatAlpha = 0.7f;
                break;
            case >= 5:
                colorBoatAlpha = 1f;
                break;
        }
    }

    private void BoatEnterAndExit()
    {
        if (Input.GetKeyDown(KeyCode.E) && isBoatFinished)
        {
            if (!isPlayerInBoat)
            {
                isPlayerInBoat = true;

                Debug.Log("Player is in vehicle");
                PlayerController.instance.IsPlayerInVehicle = true;
                PlayerController.instance.GetComponent<Rigidbody2D>().mass = 0.0001f;
                
            }
            else
            {
                isPlayerInBoat = false;
                //cannonState = CannonState.Shoot;

                PlayerController.instance.IsPlayerInVehicle = false;
                PlayerController.instance.GetComponent<Rigidbody2D>().mass = 1f; 

                PlayerController.instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                player.transform.position = boatExitPosition.transform.position;
                player.transform.rotation = boatExitPosition.transform.rotation;

                //StartCoroutine(CannonShootEffectCoroutine());
            }
        }
    }

    private void BoatMovement()
    {
        if(isPlayerInBoat && isBoatFinished)
        {
            player.transform.position = boatStandPosition.transform.position;
            player.transform.rotation = boatStandPosition.transform.rotation;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                sprRndr.flipX = false;
                rb.velocity = new Vector2(-1 * boatMoveSpeed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                sprRndr.flipX = true;
                rb.velocity = new Vector2(1 * boatMoveSpeed, rb.velocity.y);
            }
        }
    }

    private void E_ButtonAppear()
    {
        spriteKeyboard_E.SetActive(isPlayerInRange);

        if (isBoatFinished && isPlayerInBoat)
        {
            gameObj_BoatPart.SetActive(false);
            gameObj_EnterBoat.SetActive(false);
            gameObj_ExitBoat.SetActive(true);
            spriteKeyboard_A.SetActive(true);
            spriteKeyboard_D.SetActive(true);

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if(isBoatFinished && !isPlayerInBoat)
        {
            gameObj_BoatPart.SetActive(false);
            gameObj_EnterBoat.SetActive(true);
            gameObj_ExitBoat.SetActive(false);
            spriteKeyboard_A.SetActive(false);
            spriteKeyboard_D.SetActive(false);
        }
        else if(!isBoatFinished)
        {
            gameObj_BoatPart.SetActive(true);
            gameObj_EnterBoat.SetActive(false);
            gameObj_ExitBoat.SetActive(false);
            spriteKeyboard_A.SetActive(false);
            spriteKeyboard_D.SetActive(false);
        }
    }

    private void CraftBoat()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (collectibleManager.Num_BoatPart > 0)
            {
                SoundManager.instance.CraftItemSound();
                collectibleManager.Num_BoatPart--;
                num_CurBoatPart++;

                if(num_CurBoatPart >= 5)
                {
                    isBoatFinished = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            SoundManager.instance.WaterSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
