using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealthPotion : Item
{
    [Header("Config")]
    [SerializeField] int healAmount = 2;

    private void Update()
    {
        Heal();
        // TODO: Remove item
        Used();
    }

    private void Heal()
    {
        GameSession.Instance.playerHealth.TakeHeal(healAmount);
    }
}
