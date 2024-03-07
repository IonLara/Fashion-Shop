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
        } else 
        {
            interactSign.SetActive(false);
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
        }
        pauseMenu.SetActive(isPaused);
    }
}
