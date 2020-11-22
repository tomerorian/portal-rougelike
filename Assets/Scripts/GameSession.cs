using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] GameObject playerInventoryItemsParent = null;

    [Header("Config")]
    [SerializeField] int startingPlayerHealth = 5;
    [SerializeField] int startingPlayerMaxHealth = 5;

    [Header("Debug View")]
    [SerializeField] int levelsCompleted = 0;
    [SerializeField] int playerCurrentHealth = 0;
    [SerializeField] int playerMaxHealth = 0;

    GameObject player = null;

    public PlayerInventory playerInventory { get; private set; } = null;

    private void Awake()
    {
        if (!SetupSingleton()) { return; }

        SceneManager.sceneLoaded += OnSceneLoaded;

        playerCurrentHealth = startingPlayerHealth;
        playerMaxHealth = startingPlayerMaxHealth;

        playerInventory = new PlayerInventory(playerInventoryItemsParent);
        playerInventoryItemsParent.SetActive(false);
    }

    #region Singleton
    private bool SetupSingleton()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return false;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        return true;
    }
    #endregion

    #region SceneManagement
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateReferences();
        UpdatePlayer();
    }

    private void UpdateReferences()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void UpdatePlayer()
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.onDamage += OnPlayerTakenDamage;
        playerHealth.ChangeMaxHealth(playerMaxHealth);
        playerHealth.SetCurrentHealth(playerCurrentHealth);
    }

    private void OnPlayerTakenDamage(int amount)
    {
        playerCurrentHealth -= amount;
    }
    #endregion

    #region Public Methdos
    public void OnLevelCompleted()
    {
        levelsCompleted++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayerDeath()
    {
        levelsCompleted = 0;
        playerCurrentHealth = startingPlayerHealth;
        playerMaxHealth = startingPlayerMaxHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetLevel()
    {
        return levelsCompleted + 1;
    }
    #endregion
}
