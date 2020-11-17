using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one Level script instances");
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void OnPlayerReachExit()
    {
        print("EXIT!");
    }
}
