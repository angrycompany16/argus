// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    GameObject player;
    [SerializeField] CinemachineVirtualCamera vCam;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
        if (player && vCam)
            vCam.Follow = player.transform;
    }
}
