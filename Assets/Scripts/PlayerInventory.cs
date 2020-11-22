using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IEnumerable
{
    const int PLAYER_INVENTORY_SLOTS = 5;

    Item[] items = new Item[PLAYER_INVENTORY_SLOTS];

    public PlayerInventory()
    {
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
                items[i] = item;
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

    public void RemoveItemAt(int index)
    {
        if (index >= items.Length) { return; }

        items[index] = null;
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
