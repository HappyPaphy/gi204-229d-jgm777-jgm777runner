using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private enum CamIndex { frontCam, thirdPersonCam }
    [SerializeField] CamIndex camIndex;

    [SerializeField] private Transform player;
    [SerializeField] private Transform position_FrontCam, position_ThirdPersonCam;
    [SerializeField] private KeyCode switchCam = KeyCode.Q; // KeyCode.U
    [SerializeField] private KeyCode moveForwardKey = KeyCode.W; // KeyCode.I
    //[SerializeField] private float camMoveSpeed = 10f;
    //[SerializeField] private float offsetZ_ThirdPersonCam_ZoomNormal, offsetZ_ThirdPersonCam_ZoomOut;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (Input.GetKey(moveForwardKey))
        {
            if(position_ThirdPersonCam.transform.position.z > (player.transform.position.z + offsetZ_ThirdPersonCam_ZoomOut))
            {
                position_ThirdPersonCam.transform.Translate(Vector3.back * camMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (position_ThirdPersonCam.transform.position.z != (player.transform.position.z + offsetZ_ThirdPersonCam_ZoomNormal))
            {
                position_ThirdPersonCam.
            }
        }*/

        if (Input.GetKeyDown(switchCam))
        {
            if(camIndex == CamIndex.frontCam)
            {
                camIndex = CamIndex.thirdPersonCam;
            }
            else
            {
                camIndex = CamIndex.frontCam;
            }
        }
    }

    void LateUpdate()
    {
        if(camIndex == CamIndex.thirdPersonCam)
        {
            transform.position = position_ThirdPersonCam.transform.position;
        }
        else
        {
            transform.position = position_FrontCam.transform.position;
            //transform.position = player.transform.position + offset_FrontCam;
        }
    }
}
