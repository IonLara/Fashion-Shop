using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    public PlyrInventory inventory;
    private int _money;

    void OnEnable()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        _money = inventory.money;
        inventory.OnMoneyChange += MoneyChanged;
        _textMesh.text = "" + _money;
    }

    void OnDisable()
    {
        inventory.OnMoneyChange -= MoneyChanged;
    }

    private void MoneyChanged(int amount)
    {
        _money += amount;
        _textMesh.text = "" + _money;
    }
}
