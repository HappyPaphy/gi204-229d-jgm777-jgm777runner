using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float length, startPositionX, startPositionY;
    public GameObject cam1;
    public float parallaxEffect;

    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = (cam1.transform.position.x * (1 - parallaxEffect));
        float distX = (cam1.transform.position.x * parallaxEffect);
        float tempY = (cam1.transform.position.y * (parallaxEffect -1));
        float distY = (cam1.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPositionX + distX, startPositionY - distY, transform.position.z);

        if (tempX > startPositionX * length)
        {
            startPositionX += length;
        }
        else if (tempX < startPositionX - length)
        {
            startPositionX -= length;
        }

        if (tempY > startPositionY * length)
        {
            startPositionY += length;
        }
        else if (tempY < startPositionY - length)
        {
            startPositionY -= length;
        }
    }
}
