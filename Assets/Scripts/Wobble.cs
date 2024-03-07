using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public float speed = 0.5f;
    public float intensity = 0.2f;

    private float _y = 1;

    private bool _up = true;

    void Update()
    {
        if (_up)
        {
            _y = transform.localScale.y + (Time.deltaTime*(intensity/speed));
            if (_y >= 1 + intensity)
            {
                _up = false;
            }
        } else 
        {
            _y = transform.localScale.y - (Time.deltaTime*(intensity/speed));
            if (_y <= 1 - intensity)
            {
                _up = true;
            }
        }

        Vector2 scale = new Vector2(_y,_y);
        transform.localScale = scale;
    }
}
