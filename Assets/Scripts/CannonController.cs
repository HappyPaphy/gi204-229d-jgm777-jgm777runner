using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject spriteKeyboard_E;
    [SerializeField] private Transform cannonBarrelPosition;

    [SerializeField] private bool isPlayerInRange = false;
    [SerializeField] private bool isPlayerInCannon = false;

    [SerializeField] private float cannonRotateSpeed = 1f;


    [SerializeField] private GameObject player;

    void Start()
    {
        spriteKeyboard_E.SetActive(false);
    }

    void Update()
    {
        if(isPlayerInCannon)
        {
            player.transform.position = cannonBarrelPosition.transform.position;
            player.transform.rotation = cannonBarrelPosition.transform.rotation;
        }

        if(isPlayerInRange && Input.GetKey(KeyCode.E))
        {
            if (!isPlayerInCannon)
            {
                isPlayerInCannon = true;
                
            }
            else
            {
                isPlayerInCannon = false;
            }
        }

        if (isPlayerInCannon)
        {
            PlayerController.instance.IsPlayerInCannon = true;

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
            Debug.Log("Player Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            spriteKeyboard_E.SetActive(false);
            Debug.Log("Player Exit");
        }
    }
}
