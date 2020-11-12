using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCinemacineSizeAccordingToCamera : MonoBehaviour
{
    [SerializeField] Camera referenceCamera = null;

    int updateAttempts = 5;

    private void Update()
    {
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = referenceCamera.orthographicSize;

        updateAttempts--;

        if (updateAttempts == 0)
        {
            Destroy(this);
        }
    }
}
