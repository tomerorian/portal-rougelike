using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnDamage(int amount);
    public event OnDamage onDamage;

    [Header("Config")]
    [SerializeField] int maxHealth = 1;

    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        onDamage?.Invoke(damage);

        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }
}