using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    public LinkedList<T> Entires { get; } = new LinkedList<T>();

    public int Count { get { return Entires.Count; } }

    public T Dequeue()
    {
        if (Entires.Count > 0)
        {
            T value = Entires.First.Value;
            Entires.RemoveFirst();
            return value;
        }

        return default(T);
    }

    public void Enqueue(T value)
    {
        var newEntry = new LinkedListNode<T>(value);

        if (Entires.Count == 0)
        {
            Entires.AddFirst(newEntry);
        }
        else
        {
            var curEntry = Entires.First;

            while (curEntry.Next != null && curEntry.Value.CompareTo(value) <= 0)
            {
                curEntry = curEntry.Next;
            }

            if (curEntry.Value.CompareTo(value) <= 0)
            {
                Entires.AddAfter(curEntry, newEntry);
            }
            else
            {
                Entires.AddBefore(curEntry, newEntry);
            }
        }
    }

    public T FindDequeue(Func<T, bool> isMatch)
    {
        var curEntry = Entires.First;

        while (curEntry != null)
        {
            if (isMatch(curEntry.Value))
            {
                Entires.Remove(curEntry);
                return curEntry.Value;
            }

            curEntry = curEntry.Next;
        }

        return default(T);
    }
}
