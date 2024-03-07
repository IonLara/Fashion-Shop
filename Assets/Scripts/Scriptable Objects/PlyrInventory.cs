using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Inventory", menuName = "ScriptableObjects/Inventory")]
public class PlyrInventory : ScriptableObject
{
    public Item[] items = new Item[10];
    public int numOfItems = 0;

    public int money = 0;

    public event Action<int> OnMoneyChange;

    public void AddItem(Item item)
    {
        items[numOfItems] = item;
        numOfItems ++;
    }
    public void RemoveItem(int index)
    {
        items[index] = null;
        numOfItems--;
        if (index < numOfItems)
        {
            bool lastEmpty = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (lastEmpty && items[i] != null)
                {
                    items[i-1] = items[i];
                    items[i] = null;
                } else if(items[i] == null)
                {
                    lastEmpty = true;
                } else if(lastEmpty && items[i] == null)
                {
                    items[i] = null;
                    return;
                }
            }
        }
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
        if (OnMoneyChange != null)
        {
            OnMoneyChange.Invoke(amount);
        }
    }
}
