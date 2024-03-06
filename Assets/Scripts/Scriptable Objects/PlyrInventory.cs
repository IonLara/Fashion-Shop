using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Inventory", menuName = "ScriptableObjects/Inventory")]
public class PlyrInventory : ScriptableObject
{
    public Item[] items = new Item[10];
    public int numOfItems = 0;

    public int money = 0;
}
