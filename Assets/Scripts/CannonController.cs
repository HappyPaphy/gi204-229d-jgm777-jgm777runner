using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;


public enum CannonState
{
    Idle,
    Shoot
};

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject spriteKeyboard_E;
    [SerializeField] private GameObject text_EnterCannon;
    [SerializeField] private GameObject text_ShootCannon;
    [SerializeField] private GameObject shootEffect;

    [SerializeField] private CannonState cannonState;
    [SerializeField] private Animator cannonAnim;

    [SerializeField] private Transform cannonBarrelPosition;

    [SerializeField] private bool isPlayerInRange = false;
    [SerializeField] private bool isPlayerInCannon = false;

    [SerializeField] private float cannonRotateSpeed = 1f;


    [SerializeField] private GameObject player;

    void Start()
    {
        Animator cannonAnim = GetComponent<Animator>();

        spriteKeyboard_E.SetActive(false);
        text_EnterCannon.SetActive(false);
        text_ShootCannon.SetActive(false);
        shootEffect.SetActive(false);
    }

    void Update()
    {
        ChooseAnimation();



        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlayerInCannon)
            {
                isPlayerInCannon = true;

                text_EnterCannon.SetActive(false);
                text_ShootCannon.SetActive(true);
            }
            else
            {
                cannonState = CannonState.Shoot;

                PlayerController.instance.ShootCannon();
                PlayerController.instance.IsPlayerInCannon = false;

                StartCoroutine(CannonShootEffectCoroutine());
                text_EnterCannon.SetActive(true);
                text_ShootCannon.SetActive(false);

                isPlayerInCannon = false;
            }
        }
        else
        { 
            cannonState = CannonState.Idle;
        }

        if (isPlayerInCannon)
        {
            PlayerController.instance.IsPlayerInCannon = true;

            player.transform.position = cannonBarrelPosition.transform.position;
            player.transform.rotation = cannonBarrelPosition.transform.rotation;

            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.Rotate(Vector3.forward * cannonRotateSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.Rotate(Vector3.forward * -cannonRotateSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            spriteKeyboard_E.SetActive(true);
            text_EnterCannon.SetActive(true);
            Debug.Log("Player Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            spriteKeyboard_E.SetActive(false);
            text_EnterCannon.SetActive(false);
            Debug.Log("Player Exit");
        }
    }

    private void ChooseAnimation()
    {
        cannonAnim.SetBool("IsIdle", false);
        cannonAnim.SetBool("IsShoot", false);


        switch (cannonState)
        {
            case CannonState.Idle:
                cannonAnim.SetBool("IsIdle", true);
                break;
            case CannonState.Shoot:
                cannonAnim.SetBool("IsShoot", true);
                break;
        }
    }

    private IEnumerator CannonShootEffectCoroutine()
    {
        shootEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        shootEffect.SetActive(false);
    }
}
