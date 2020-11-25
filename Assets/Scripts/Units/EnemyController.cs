using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Health health = null;
    [SerializeField] TurnBasedUnit unit = null;

    Animator animator;

    private void Start()
    {
        health.onDamage += OnDamage;
        health.onDeath += OnDeath;

        animator = GetComponent<Animator>();
    }

    private void OnDamage(int damage)
    {
        animator.SetTrigger("TakeDamage");
    }

    private void OnDeath()
    {
        unit.IsActive = false;
        Destroy(gameObject);
    }
}
