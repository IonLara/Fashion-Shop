using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour
{
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    private float _timer = 0f;

    private bool _hasResource = false;

    public GameObject prefab;
    public Item type;

    private Collider2D _collider;

    void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0 && _hasResource == false)
        {
            _timer = Random.Range(minSpawnTime, maxSpawnTime);
            _hasResource = true;
            float x = Random.Range(_collider.bounds.min.x, _collider.bounds.max.x);
            float y = Random.Range(_collider.bounds.min.y, _collider.bounds.max.y);
            Vector2 point = new Vector2(x,y);

            GameObject resource = Instantiate(prefab, point, Quaternion.identity);
            resource.GetComponent<Resource>().Initialize(this, type);
        }
        if (_timer > 0 && _hasResource == false)
        {
            _timer -= Time.deltaTime;
        }
    }

    public void ResourceTaken()
    {
        _hasResource = false;
    }
}
