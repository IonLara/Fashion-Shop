using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Item type;
    public Spawner parent;

    private SpriteRenderer _renderer;
    
    void OnEnable()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Initialize(Spawner spawner, Item item)
    {
        parent = spawner;
        type = item;
        _renderer.sprite = type.sprite;
    }

    void OnDestroy()
    {
        parent.ResourceTaken();
    }
}
