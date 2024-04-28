using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float sawBladeForce;
    [SerializeField] private bool isFallBackRightDirection;

    private float fallBackRightDirection;

    void Start()
    {

    }

    void Update()
    {
        if (isFallBackRightDirection)
        {
            fallBackRightDirection = 1;
        }
        else
        {
            fallBackRightDirection = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.instance.CurHP -= 20;
            PlayerController.instance.GetComponent<Rigidbody2D>().AddForce(transform.up * sawBladeForce, ForceMode2D.Impulse);
            PlayerController.instance.GetComponent<Rigidbody2D>().AddForce(transform.right * fallBackRightDirection * sawBladeForce, ForceMode2D.Impulse);
        }
    }
}

