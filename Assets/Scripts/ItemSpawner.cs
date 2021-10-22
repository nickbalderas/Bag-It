using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    private float _spawnRate = 2f;
    public List<GameObject> availableItems = new List<GameObject>();
    
    private Vector3 _initialPosition = new Vector3(1f, 35f, 0f);
    private float _maxZPos = 17f;
    private float _minZPos = -17f;

    private void Start()
    {
        InvokeRepeating("SpawnItem", 0, _spawnRate);
    }

    [ContextMenu("Spawn Random Item")]
    public void SpawnItem()
    {
        var randomPosition = _initialPosition + new Vector3(0, 0, Random.Range(_minZPos, _maxZPos + 1));
        var randomItem = availableItems[Random.Range(0, availableItems.Count)];
        Instantiate(randomItem, randomPosition, transform.rotation, transform);
    }
}
