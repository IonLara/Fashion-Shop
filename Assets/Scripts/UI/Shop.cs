using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject window;
    public GameObject firstButton;

    public Image[] buySlots = new Image[10];
    public Image[] sellSlots = new Image[10];

    public PlyrInventory storeInventory;
    public PlyrInventory plyrInventory;

    public TextMeshProUGUI description;

    [Header("Sound")]
    public AudioClip menuSound;
    public AudioClip moveSound;
    public AudioClip coinSound;
    public AudioClip cancelSound;
    private AudioSource _audioSource;

    void OnEnable()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        for (int i = 0; i < storeInventory.numOfItems; i++)
        {
            UpdateBuySlot(i);
        }

        for (int i = 0; i < plyrInventory.numOfItems; i++)
        {
            UpdateSellSlot(i);
        }
    }

    private void UpdateBuySlot(int i)
    {
        buySlots[i].sprite = storeInventory.items[i].sprite;
        buySlots[i].color = storeInventory.items[i].color;
        buySlots[i].enabled = true;
        buySlots[i].gameObject.SetActive(true);
        var rect = buySlots[i].gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (int)storeInventory.items[i].type);
    }

    private void UpdateSellSlot(int i)
    {
        sellSlots[i].sprite = plyrInventory.items[i].sprite;
        sellSlots[i].color = plyrInventory.items[i].color;
        sellSlots[i].enabled = true;
        sellSlots[i].gameObject.SetActive(true);
        var rect = sellSlots[i].gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (int)plyrInventory.items[i].type);
        if (plyrInventory.items[i].type == Item.ItemType.misc)
        {
            rect.localScale = new Vector3(0.3f,0.3f,0.3f);
        } else
        {
            rect.localScale = Vector3.one;
        }
    }
    private void UpdateAllSellSlots()
    {
        for (int i = 0; i < 10; i++)
        {
            if (plyrInventory.items[i] == null)
            {
                sellSlots[i].sprite = null;
                sellSlots[i].color = Color.white;
                sellSlots[i].enabled = false;
                sellSlots[i].gameObject.SetActive(false);
                return;
            }
            sellSlots[i].sprite = plyrInventory.items[i].sprite;
            sellSlots[i].color = plyrInventory.items[i].color;
            sellSlots[i].enabled = true;
            sellSlots[i].gameObject.SetActive(true);
            var rect = sellSlots[i].gameObject.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (int)plyrInventory.items[i].type);
            if (plyrInventory.items[i].type == Item.ItemType.misc)
            {
                rect.localScale = new Vector3(0.3f,0.3f,0.3f);
            } else
            {
                rect.localScale = Vector3.one;
            }
        }
    }

    public void BuyOrSell(int index)
    {
        if (index < 10)
        {
            if (plyrInventory.numOfItems < 10)
            {
                if (storeInventory.items[index] != null)
                {
                    Item item = storeInventory.items[index];
                    if (plyrInventory.money >= item.price)
                    {
                        plyrInventory.ChangeMoney(-item.price);
                        plyrInventory.AddItem(item);
                        UpdateSellSlot(plyrInventory.numOfItems - 1);
                        _audioSource.PlayOneShot(coinSound);
                    } else //Not enough money.
                    {
                        Debug.Log("Not enough money!!");
                        _audioSource.PlayOneShot(cancelSound);
                    }
                }
            } else //Not enough space in inventory.
            {
                Debug.Log("Not enough space in inventory!!");
                _audioSource.PlayOneShot(cancelSound);
            }
            
        } else 
        {
            if(plyrInventory.items[index - 10] != null)
            {
                int ind = index - 10;
                Item item = plyrInventory.items[ind];
                plyrInventory.ChangeMoney(item.price);
                plyrInventory.RemoveItem(ind);
                UpdateAllSellSlots();
                UpdateDescription(index);
                _audioSource.PlayOneShot(coinSound);
            } 
        }
    }

    public void UpdateDescription(int index)
    {
        if (index < 10)
        {
            if (storeInventory.items[index] != null)
            {
                Item item = storeInventory.items[index];
                description.text = item.itemName + ": -$" + item.price;
            } else
            {
                description.text = "";
            }
        } else 
        {
            if(plyrInventory.items[index - 10] != null)
            {
                int ind = index - 10;
                Item item = plyrInventory.items[ind];
                description.text = item.itemName + ": +$" + item.price;
            } else
            {
                description.text = "";
            }
            
        }
    }

    public void PlayMoveSound()
    {
        _audioSource.PlayOneShot(moveSound);
    }

    public void ToggleStore(bool on) 
    {
        _audioSource.PlayOneShot(menuSound);
        window.SetActive(on);
        EventSystem.current.SetSelectedGameObject(firstButton);
        UpdateAllSellSlots();
        UpdateDescription(0);
    }
}
