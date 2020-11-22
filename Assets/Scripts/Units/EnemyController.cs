using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Health health = null;

    private void Start()
    {
        health.onDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
