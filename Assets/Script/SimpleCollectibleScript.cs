using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, HealthItem, BoatPart}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType;

    [SerializeField] private int num_BoatPart;
    public int Num_BoatPart { get { return num_BoatPart; } set { num_BoatPart = value; } }

    [SerializeField] private bool rotate;
	[SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject spriteKeyboard_E;
    [SerializeField] private GameObject text_CollectItem;

    [SerializeField] private AudioClip collectSound;
    [SerializeField] private GameObject collectEffect;

    [SerializeField] private bool isPlayerInRange = false;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CollectibleManager collectibleManager;



    void Start () 
	{
        spriteKeyboard_E.SetActive(false);
        text_CollectItem.SetActive(false);
    }
	
	void Update () 
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            SoundManager.instance.CollectItemSound();
            Collect();
        }

        if (rotate)
		{
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }
	}

	public void Collect()
	{
		/*if(collectSound)
		{
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
			
		if(collectEffect)
		{
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }*/
			
		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.NoType) 
		{
			Debug.Log("This Object hasn't set type yet.");
		}
		else if (CollectibleType == CollectibleTypes.HealthItem)
		{
			//playerController.CollectEfect(0); // 0 = Coin
            //playerController.CollectSound(0); // 0 = Coin
            //coin += 300;
            Destroy(gameObject);
        }
		else if (CollectibleType == CollectibleTypes.BoatPart)
		{
			CollectibleManager.instance.Num_BoatPart++;
            Debug.Log(CollectibleManager.instance.Num_BoatPart);
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            spriteKeyboard_E.SetActive(true);
            text_CollectItem.SetActive(true);
            Debug.Log("Player Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            spriteKeyboard_E.SetActive(false);
            text_CollectItem.SetActive(false);
            Debug.Log("Player Exit");
        }
    }
}
