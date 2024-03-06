using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    private Vector3 _movement = new Vector3(0,0,0);

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal") * speed;
        _movement.y = Input.GetAxis("Vertical") * speed;
    }

    void FixedUpdate()
    {
        // gameObject.transform.position = transform.position + _movement;
        _rb.AddForce(_movement);

    }
}
