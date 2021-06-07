using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class CameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine) 
        {
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
