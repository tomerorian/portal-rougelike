using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] Image[] items = null;

    private void Update()
    {
        if (PlayerInventory.PLAYER_INVENTORY_SLOTS != items.Length)
        {
            Debug.LogError("Player inventory slot count does not match displayed items");
        }

        for (int i = 0; i < PlayerInventory.PLAYER_INVENTORY_SLOTS; i++)
        {
            Item item = GameSession.Instance.playerInventory.GetItemAt(i);

            if (item == null)
            {
                items[i].enabled = false;
            }
            else
            {
                items[i].enabled = true;
                items[i].sprite = item.GetSpriteRenderer().sprite;
            }
        }
    }
}
