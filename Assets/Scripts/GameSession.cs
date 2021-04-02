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
    // TODO: TBD: Find a nicer way to do this?
    [SerializeField] int playerRefCurrentHealth = 0; // To be used when transitioning between levels
    [SerializeField] int playerMaxHealth = 0;

    public GameObject player { get; private set; } = null;
    public PlayerController playerController { get; private set; } = null;
    public Health playerHealth { get; private set; } = null;
    public PlayerInventory playerInventory { get; private set; } = null;

    private void Awake()
    {
        if (!SetupSingleton()) { return; }

        SceneManager.sceneLoaded += OnSceneLoaded;

        playerInventory = new PlayerInventory(playerInventoryItemsParent);

        InitGame();
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
        playerHealth = player.GetComponent<Health>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void UpdatePlayer()
    {
        playerHealth.ChangeMaxHealth(playerMaxHealth);
        playerHealth.SetCurrentHealth(playerRefCurrentHealth);
    }
    #endregion

    #region Public Methdos
    public void OnLevelCompleted()
    {
        playerRefCurrentHealth = playerHealth.GetCurrentHealth();

        levelsCompleted++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayerDeath()
    {
        InitGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void InitGame()
    {
        levelsCompleted = 0;
        playerRefCurrentHealth = startingPlayerHealth;
        playerMaxHealth = startingPlayerMaxHealth;
        playerInventory.Clear();
    }

    public int GetLevel()
    {
        return levelsCompleted + 1;
    }
    #endregion
}
