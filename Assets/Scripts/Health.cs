using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnDamage(int amount);
    public event OnDamage onDamage;

    public delegate void OnHeal(int amount);
    public event OnHeal onHeal;

    [Header("Config")]
    [SerializeField] int maxHealth = 1;

    [Header("Debug View")]
    [SerializeField] int currentHealth = 0;

    private void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void ChangeMaxHealth(int health)
    {
        if (health > maxHealth)
        {
            currentHealth += maxHealth - health;
        }
        else if (health < maxHealth)
        {
            currentHealth = Mathf.Min(health, currentHealth);
        }

        maxHealth = health;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = Mathf.Min(health, maxHealth);
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

    public void TakeHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        onHeal?.Invoke(amount);
    }
}