using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    // private Item[] items = new Item[10];
    public PlyrInventory inventory;

    public GameObject[] slots = new GameObject[10];
    public GameObject[] icons = new GameObject[10];

    public Image hat;
    public Image hair;
    public Image shirt;
    public Image pants;
    public Image shoes;

    public Color baseColor;
    public Color highlightColor;

    private int indexSelected = 0;

    public float uiCooldown = 0.3f;
    private float _timer = 0f;
    private bool cd = false;

    private bool spaceCD = false;

    public TextMeshProUGUI description;

    public Player player;

    void OnEnable()
    {
        if (player.hasHat)
        {
            hat.enabled = true;
            hat.sprite = player.hat.sprite;
            hat.color = player.hat.color;
        }
        if (player.hasHair)
        {
            hair.enabled = true;
            hair.sprite = player.hair.sprite;
            hair.color = player.hair.color;
        }
        if (player.hasShirt)
        {
            shirt.enabled = true;
            shirt.sprite = player.shirt.sprite;
            shirt.color = player.shirt.color;
        }
        if (player.hasPants)
        {
            pants.enabled = true;
            pants.sprite = player.pants.sprite;
            pants.color = player.pants.color;
        }
        if (player.hasShoes)
        {
            shoes.enabled = true;
            shoes.sprite = player.shoes.sprite;
            shoes.color = player.shoes.color;
        }
        
        indexSelected = 0;

        for (int i = 0; i < 10; i++)
        {
            slots[i].GetComponent<Image>().color = baseColor;
            var item = inventory.items[i];
            if(item != null)
            {
                icons[i].SetActive(true);
                var image = icons[i].GetComponent<Image>();
                image.sprite = item.sprite;
                image.enabled = true;
                image.color = item.color;
                var rect = icons[i].GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (int)item.type);
            } else 
            {
                icons[i].SetActive(false);
            }
        }
        if (inventory.items[indexSelected]!= null)
        {
            description.text = inventory.items[indexSelected].itemName;
        } else
        {
            description.text = "";
        }
        slots[0].GetComponent<Image>().color = highlightColor;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if(cd == false && hor > 0)
        {
            cd = true;
            slots[indexSelected].GetComponent<Image>().color = baseColor;
            if (indexSelected < 9)
            {
                indexSelected++;
            } else 
            {
                indexSelected = 0;
            }
            slots[indexSelected].GetComponent<Image>().color = highlightColor;
            if (inventory.items[indexSelected]!= null)
            {
                description.text = inventory.items[indexSelected].itemName;
            }else
            {
                description.text = "";
            }
        } else if(cd == false && hor < 0)
        {
            cd = true;
            slots[indexSelected].GetComponent<Image>().color = baseColor;
            if (indexSelected > 0)
            {
                indexSelected--;
            } else 
            {
                indexSelected = 9;
            }
            slots[indexSelected].GetComponent<Image>().color = highlightColor;
            if (inventory.items[indexSelected]!= null)
            {
                description.text = inventory.items[indexSelected].itemName;
            } else
            {
                description.text = "";
            }
        } else if(cd == false && ver > 0)
        {
            cd = true;
            slots[indexSelected].GetComponent<Image>().color = baseColor;
            if (indexSelected < 5)
            {
                indexSelected+=5;
            } else 
            {
                indexSelected-=5;
            }
            slots[indexSelected].GetComponent<Image>().color = highlightColor;
            if (inventory.items[indexSelected]!= null)
            {
                description.text = inventory.items[indexSelected].itemName;
            } else
            {
                description.text = "";
            }
        } else if(cd == false && ver < 0)
        {
            cd = true;
            slots[indexSelected].GetComponent<Image>().color = baseColor;
            if (indexSelected < 5)
            {
                indexSelected+=5;
            } else 
            {
                indexSelected-=5;
            }
            slots[indexSelected].GetComponent<Image>().color = highlightColor;
            if (inventory.items[indexSelected]!= null)
            {
                description.text = inventory.items[indexSelected].itemName;
            } else
            {
                description.text = "";
            }
        }

        if (cd == true)
        {
            _timer += Time.deltaTime;
            if (_timer >= uiCooldown)
            {
                cd = false;
                _timer = 0;
            }
        }

        if (spaceCD == false && Input.GetKeyDown(KeyCode.Space))
        {
            EquipItem();
            spaceCD = true;
        }
        if (spaceCD == true && Input.GetKeyDown(KeyCode.Space) == false)
        {
            spaceCD = false;
        }
    }

    private void EquipItem()
    {
        if (inventory.items[indexSelected] != null)
        {
            Item item = inventory.items[indexSelected];

            if (item.isEquipped)
            {
                item.isEquipped = false;

                switch (item.type)
                {
                    case Item.ItemType.hat:
                        player.hasHat = false;
                        player.hat = null;
                        player.hatRend.sprite = null;

                        hat.enabled = false;
                        break;
                    case Item.ItemType.hair:
                        player.hasHair = false;
                        player.hair = null;
                        player.hairRend.sprite = null;

                        hair.enabled = false;
                        break;
                    case Item.ItemType.shirt:
                        player.hasShirt = false;
                        player.shirt = null;
                        player.shirtRend.sprite = null;

                        shirt.enabled = false;
                        break;
                    case Item.ItemType.pants:
                        player.hasPants = false;
                        player.pants = null;
                        player.pantsRend.sprite = null;

                        pants.enabled = false;
                        break;
                    case Item.ItemType.shoes:
                        player.hasShoes = false;
                        player.shoes = null;
                        player.shoesRend.sprite = null;

                        shoes.enabled = false;
                        break;
                    default:
                        break;
                }
            } else 
            {
                switch (item.type)
                {
                    case Item.ItemType.hat:
                        if (player.hasHat)
                        {
                            CheckIfEquipped(Item.ItemType.hat);
                        }
                        player.hasHat = true;
                        player.hat = item;
                        player.hatRend.sprite = item.sprite;
                        player.hatRend.color = item.color;

                        hat.enabled = true;
                        hat.sprite = item.sprite;
                        hat.color = item.color;
                        break;
                    case Item.ItemType.hair:
                        if (player.hasHair)
                        {
                            CheckIfEquipped(Item.ItemType.hair);
                        }
                        player.hasHair = true;
                        player.hair = item;
                        player.hairRend.sprite = item.sprite;
                        player.hairRend.color = item.color;

                        hair.enabled = true;
                        hair.sprite = item.sprite;
                        hair.color = item.color;
                        break;
                    case Item.ItemType.shirt:
                        if (player.hasShirt)
                        {
                            CheckIfEquipped(Item.ItemType.shirt);
                        }
                        player.hasShirt = true;
                        player.shirt = item;
                        player.shirtRend.sprite = item.sprite;
                        player.shirtRend.color = item.color;

                        shirt.enabled = true;
                        shirt.sprite = item.sprite;
                        shirt.color = item.color;
                        break;
                    case Item.ItemType.pants:
                        if (player.hasPants)
                        {
                            CheckIfEquipped(Item.ItemType.pants);
                        }
                        player.hasPants = true;
                        player.pants = item;
                        player.pantsRend.sprite = item.sprite;
                        player.pantsRend.color = item.color;

                        pants.enabled = true;
                        pants.sprite = item.sprite;
                        pants.color = item.color;
                        break;
                    case Item.ItemType.shoes:
                        if (player.hasShoes)
                        {
                            CheckIfEquipped(Item.ItemType.shoes);
                        }
                        player.hasShoes = true;
                        player.shoes = item;
                        player.shoesRend.sprite = item.sprite;
                        player.shoesRend.color = item.color;

                        shoes.enabled = true;
                        shoes.sprite = item.sprite;
                        shoes.color = item.color;
                        break;
                    default:
                        break;
                }
                item.isEquipped = true;
            }
        }
        
    }

    private void CheckIfEquipped(Item.ItemType type)
    {
        for (int i = 0; i < inventory.numOfItems; i++)
        {
            if (inventory.items[i].isEquipped && inventory.items[i].type == type)
            {
                inventory.items[i].isEquipped = false;
            }
        }
    }
}
