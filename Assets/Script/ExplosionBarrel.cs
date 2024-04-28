using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosionBarrel : MonoBehaviour
{
    //[SerializeField] private GameObject explosionEffect;
    [SerializeField] private MeshRenderer[] meshRenderer;
    [SerializeField] private Collider[] collider;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioSource explosionSound;


    [SerializeField] private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            StartCoroutine(DestroyGameObjectOnTime(4f));
            Explode();
        }
    }

    public void Explode()
    {
        if (explosionSound)
        {
            explosionSound.Play();
        }

        if (explosionEffect)
        {
            //Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosionEffect.Play();
        }
    }

    IEnumerator DestroyGameObjectOnTime(float time)
    {
        Debug.Log("Destroying Object");

        foreach(MeshRenderer meshRndr in meshRenderer)
        {
            meshRndr.enabled = false;
        }

        foreach (Collider col in collider)
        {
            col.enabled = false;
        }

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
