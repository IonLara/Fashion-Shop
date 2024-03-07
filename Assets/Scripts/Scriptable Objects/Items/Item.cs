using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Color color = Color.white;
    public Sprite sprite;
    public Sprite backSpr;
    public Sprite rightSpr;
    public int price = 5;

    public bool isEquipped = false;

    public enum ItemType
    {
        hair = -35,
        hat = -45,
        shirt = 0,
        pants = 11,
        shoes = 25,
        misc = 1
    }
    public ItemType type = ItemType.misc;
}
