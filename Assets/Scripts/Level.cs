using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    bool isPlayerTurn = false;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one Level script instances");
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        isPlayerTurn = true;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public void OnPlayerReachExit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayerTookAction()
    {
        isPlayerTurn = false;

        StartCoroutine(DoEnemyTurn());
    }

    private IEnumerator DoEnemyTurn()
    {
        isPlayerTurn = true;
        yield return null;
    }
}
