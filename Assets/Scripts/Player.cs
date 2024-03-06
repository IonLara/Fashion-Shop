using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    private Vector3 _movement = new Vector3(0,0,0);

    private Rigidbody2D _rb;

    public Item[] items = new Item[10];
    public int numOfItems = 0;

    public bool hasHair = false;
    public Item hair;
    public SpriteRenderer hairRend;
    public bool hasHat = false;
    public Item hat;
    public SpriteRenderer hatRend;
    public bool hasShirt = false;
    public Item shirt;
    public SpriteRenderer shirtRend;
    public bool hasPants = false;
    public Item pants;
    public SpriteRenderer pantsRend;
    public bool hasShoes = false;
    public Item shoes;
    public SpriteRenderer shoesRend;

    private bool isPaused = false;
    public GameObject pauseMenu;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            TogglePause();
        }

        if(isPaused == false)
        {
            _movement.x = Input.GetAxis("Horizontal") * speed;
            _movement.y = Input.GetAxis("Vertical") * speed;
        }
    }

    void FixedUpdate()
    {
        if (isPaused == false)
        {
            _rb.AddForce(_movement);
        }
    }

    public void TogglePause()
    {
        if(isPaused) 
        {
            isPaused = false;
        } else
        {
            isPaused = true;
            pauseMenu.GetComponent<Inventory>().SetItems(items);
        }
        pauseMenu.SetActive(isPaused);
    }
}
