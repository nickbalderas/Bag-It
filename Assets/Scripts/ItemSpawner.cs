using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> availableItems = new List<GameObject>();
    
    private Vector3 _initialPosition = new Vector3(1f, 35f, 0f);
    private float _maxZPos = 17f;
    private float _minZPos = -11f;

    public void SpawnItem()
    {
        var randomPosition = _initialPosition + new Vector3(0, 0, Random.Range(_minZPos, _maxZPos + 1));
        var randomItem = availableItems[Random.Range(0, availableItems.Count)];
        Instantiate(randomItem, randomPosition, transform.rotation, transform);
    }
}
