using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    private Vector3 _movement = new Vector3(0,0,0);
    private Vector3 _lastMove = Vector3.zero;
    private Vector2 _direction = Vector2.down;

    private Rigidbody2D _rb;

    public PlyrInventory inventory;

    [HideInInspector]
    public bool hasHair = false;
    [HideInInspector]
    public Item hair;
    public SpriteRenderer hairRend;
    [HideInInspector]
    public bool hasHat = false;
    [HideInInspector]
    public Item hat;
    public SpriteRenderer hatRend;
    [HideInInspector]
    public bool hasShirt = false;
    [HideInInspector]
    public Item shirt;
    public SpriteRenderer shirtRend;
    [HideInInspector]
    public bool hasPants = false;
    [HideInInspector]
    public Item pants;
    public SpriteRenderer pantsRend;
    [HideInInspector]
    public bool hasShoes = false;
    [HideInInspector]
    public Item shoes;
    public SpriteRenderer shoesRend;

    private bool isPaused = false;
    public GameObject pauseMenu;

    public Transform rotator;
    public float rotSpeed = 10;

    public Transform lookTransform;
    public float interactArea = 1;

    public ContactFilter2D contactFilter2D = new ContactFilter2D();

    public GameObject interactSign;

    private GameObject _interactable;
    private bool _spaceCD = false;

    private bool _storeOpen = false;
    private Shop _openShop;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] != null && inventory.items[i].isEquipped)
            {
                Item item = inventory.items[i];
                switch (item.type)
                {
                    case Item.ItemType.hat:
                        hasHat = true;
                        hat = item;
                        hatRend.sprite = item.sprite;
                        break;
                    case Item.ItemType.hair:
                        hasHair = true;
                        hair = item;
                        hairRend.sprite = item.sprite;
                        break;
                    case Item.ItemType.shirt:
                        hasShirt = true;
                        shirt = item;
                        shirtRend.sprite = item.sprite;
                        break;
                    case Item.ItemType.pants:
                        hasPants = true;
                        pants = item;
                        pantsRend.sprite = item.sprite;
                        break;
                    case Item.ItemType.shoes:
                        hasShoes = true;
                        shoes = item;
                        shoesRend.sprite = item.sprite;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            ToggleInventory();
        }

        if(isPaused == false)
        {
            if (_spaceCD == false && _interactable != null && Input.GetKeyUp(KeyCode.Space))
            {
                if (_interactable.TryGetComponent<Shop>(out Shop shop))
                {
                    ToggleStore(shop);
                    _openShop = shop;
                }
            }

            _movement.x = Input.GetAxisRaw("Horizontal") * speed;
            _movement.y = Input.GetAxisRaw("Vertical") * speed;

            if (_movement != Vector3.zero)
            {
                _lastMove.x = _movement.normalized.x;
                _lastMove.y = _movement.normalized.y;

                _direction = (transform.position + _lastMove) - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _movement.normalized);
                rotator.rotation = Quaternion.RotateTowards(rotator.rotation, toRotation, rotSpeed);
            }
        }
        
        if (_spaceCD && Input.GetKey(KeyCode.Space) == false)
        {
            _spaceCD = false;
        }
        if (_storeOpen && Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleStore(_openShop);
            _openShop = null;
        }
    }


    void FixedUpdate()
    {
        if (isPaused == false)
        {
            _rb.AddForce(_movement);
        }

        Collider2D[] colliders = new Collider2D[5]; 

        int overlapped = Physics2D.OverlapCircle(new Vector2(lookTransform.position.x, lookTransform.position.y), interactArea, contactFilter2D, colliders);

        if (overlapped > 0)
        {
            interactSign.SetActive(true);
            _interactable = colliders[0].gameObject;
        } else 
        {
            interactSign.SetActive(false);
            _interactable = null;
        }
    }

    public void ToggleStore(Shop shop)
    {
        if (_storeOpen)
        {
            _storeOpen = false;
            isPaused = false;
            _spaceCD = true;
        } else if(_spaceCD == false)
        {
            _storeOpen = true;
            isPaused = true;
        }
        shop.ToggleStore(_storeOpen);
    }

    public void ToggleInventory()
    {
        if(isPaused) 
        {
            isPaused = false;
        } else
        {
            isPaused = true;
        }
        pauseMenu.SetActive(isPaused);
    }
}
