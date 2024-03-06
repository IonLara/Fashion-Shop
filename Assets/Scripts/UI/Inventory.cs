using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Item[] items = new Item[10];

    public GameObject[] slots = new GameObject[10];
    public GameObject[] icons = new GameObject[10];

    public Color baseColor;
    public Color highlightColor;

    private int indexSelected = 0;

    void OnEnable()
    {
        indexSelected = 0;
        slots[0].GetComponent<Image>().color = highlightColor;
        for (int i = 0; i < 10; i++)
        {
            if(items[i] != null)
            {
                icons[i].SetActive(true);
                var image = icons[i].GetComponent<Image>();
                image.sprite = items[i].sprite;
                image.enabled = true;
                var rect = icons[i].GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + (int)items[i].type);
            } else 
            {
                icons[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItems(Item[] list) 
    {
        items = list;
    }
    
}
