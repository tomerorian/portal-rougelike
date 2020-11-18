using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    bool isPlayerTurn = false;
    bool hasUnitsRoutineRunning = false;
    List<TurnBasedUnit> units = new List<TurnBasedUnit>();
    List<TurnBasedUnit> removedUnits = new List<TurnBasedUnit>();

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one Level script instances");
            Destroy(gameObject);
        }

        Instance = this;
        InitGame();
    }

    private void Update()
    {
        if (IsPlayerTurn() || hasUnitsRoutineRunning) { return; }

        StartCoroutine(DoUnitsTurn());
    }

    private void InitGame()
    {
        isPlayerTurn = true;
        hasUnitsRoutineRunning = false;
        units.Clear();
        removedUnits.Clear();
    }

    public void AddUnit(TurnBasedUnit unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(TurnBasedUnit unit)
    {
        removedUnits.Add(unit);
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
    }

    private IEnumerator DoUnitsTurn()
    {
        hasUnitsRoutineRunning = true;

        yield return new WaitForFixedUpdate();

        List<Coroutine> turnRoutines = new List<Coroutine>();

        foreach (TurnBasedUnit unit in units)
        {
            turnRoutines.Add(StartCoroutine(unit.TakeTurn()));
        }

        foreach (Coroutine turnRoutine in turnRoutines)
        {
            yield return turnRoutine;
        }

        RemoveUnits();

        hasUnitsRoutineRunning = false;
        isPlayerTurn = true;
    }

    private void RemoveUnits()
    {
        foreach (TurnBasedUnit unit in removedUnits)
        {
            units.Remove(unit);
        }

        removedUnits.Clear();
    }
}
