using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCache : MonoBehaviour
{
    #region Singleton
    public static PrefabCache Instance { get; private set; }

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Enemies")]
    public MazeUnit Enemy;
    public MazeUnit Slime;

    [Header("Items")]
    public Item Sword;
    public Item HealthPotion;
}
