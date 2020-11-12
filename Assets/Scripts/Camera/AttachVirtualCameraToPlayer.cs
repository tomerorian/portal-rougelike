using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachVirtualCameraToPlayer : MonoBehaviour
{
    void Start()
    {
        GetComponent<ICinemachineCamera>().Follow = FindObjectOfType<PlayerController>().transform;
    }
}
