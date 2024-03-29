﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    bool isPlayerTurn = false;
    bool isPaused = false;
    List<TurnBasedUnit> units = new List<TurnBasedUnit>();
    List<TurnBasedUnit> removedUnits = new List<TurnBasedUnit>();

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one Level script instances");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitGame();
    }

    private void InitGame()
    {
        isPlayerTurn = true;
        isPaused = false;
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

    public void PauseGame()
    {
        isPaused = true;
    }

    public void UnpauseGame()
    {
        isPaused = false;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn && !isPaused;
    }

    public void OnPlayerReachExit()
    {
        // Can add conditions for finishing the level here
        GameSession.Instance.OnLevelCompleted();
    }

    public void OnPlayerTookAction()
    {
        isPlayerTurn = false;

        RemoveUnits();
        StartCoroutine(DoUnitsTurn());
    }

    private IEnumerator DoUnitsTurn()
    {
        yield return null;

        foreach (TurnBasedUnit unit in units)
        {
            StartCoroutine(unit.TakeTurn());
        }

        bool didAllUnitsFinish;

        do
        {
            didAllUnitsFinish = true;
            foreach (TurnBasedUnit unit in units)
            {
                didAllUnitsFinish = didAllUnitsFinish && unit.FinishedTurn;
            }

            if (!didAllUnitsFinish)
            {
                yield return null;
            }
        } while (!didAllUnitsFinish);

        RemoveUnits();

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
