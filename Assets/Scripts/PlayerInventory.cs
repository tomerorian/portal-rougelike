﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IEnumerable
{
    public const int PLAYER_INVENTORY_SLOTS = 5;

    Item[] items = new Item[PLAYER_INVENTORY_SLOTS];
    GameObject inventoryItemsParent;

    public PlayerInventory(GameObject inventoryItemsParent)
    {
        this.inventoryItemsParent = inventoryItemsParent;

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
    }

    public bool AttemptAddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                item.SetInventoryIndex(i);
                items[i] = item;
                item.transform.parent = inventoryItemsParent.transform;
                item.transform.localPosition = Vector3.zero;
                return true;
            }
        }

        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                return false;
            }
        }

        return true;
    }

    public Item GetItemAt(int index)
    {
        if (index >= items.Length) { return null; }

        return items[index];
    }

    public Item RemoveItemAt(int index)
    {
        if (index >= items.Length) { return null; }

        Item item = items[index];
        items[index] = null;

        return item;
    }

    public void Clear()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item item = RemoveItemAt(i);

            if (item)
            {
                GameObject.Destroy(item.gameObject);
            }
        }
    }

    #region IEnumerable
    public IEnumerator GetEnumerator()
    {
        return new InventoryEnumerator(this);
    }

    private class InventoryEnumerator : IEnumerator<Item>
    {
        private PlayerInventory inventory;
        int curI = -1;

        public InventoryEnumerator(PlayerInventory inventory)
        {
            this.inventory = inventory;
        }

        object IEnumerator.Current => Current;

        public Item Current
        {
            get
            {
                try
                {
                    return inventory.GetItemAt(curI);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            do
            {
                curI++;
            } while (curI < PLAYER_INVENTORY_SLOTS && inventory.GetItemAt(curI) == null);

            return inventory.GetItemAt(curI) != null;
        }

        public void Reset()
        {
            curI = -1;
        }
    }
    #endregion
}
