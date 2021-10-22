using UnityEngine;

public class Bag : MonoBehaviour
{
    private readonly Vector3 _defaultBagPosition = new Vector3(4f, 0f, 0f);
    private float maxZPos = 18f;
    private float minZPos = -18f;
    private Transform _transform;
    private Renderer _renderer;

    public float speed;
    private float _weight;
    private float _maxWeight;

    private void Awake()
    {
        _weight = 0;
        _maxWeight = 30;
        speed = 50f;
        _transform = transform;
        _renderer = GetComponent<Renderer>();
    }
    
    void Start()
    {
        _transform.position = _defaultBagPosition;
    }
    
    void Update()
    {
        HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            AddItemToBag(other.GetComponent<Item>());
        }
    }

    private void HandleMovement()
    {
        if (_weight > _maxWeight) return;

        var direction = Input.GetKey(KeyCode.LeftArrow) ? Vector3.back :
            Input.GetKey(KeyCode.RightArrow) ? Vector3.forward : Vector3.zero;

        if (_transform.position.z > maxZPos) _transform.position = _defaultBagPosition + new Vector3(0, 0, maxZPos);
        if (_transform.position.z < minZPos) _transform.position = _defaultBagPosition + new Vector3(0, 0, minZPos);
        if (transform.position.z <= maxZPos && transform.position.z >= minZPos)
        {
            _transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void AddItemToBag(Item item)
    {
        UpdateWeight(item);
        UpdateSpeed();
        if (_weight > _maxWeight) DestroyBag();
        ScoreManager.Instance.UpdateScore(5, 5);
    }

    private void Close()
    {
        Debug.Log("Player wants to close current bag and open another");
    }

    private void DestroyBag()
    {
        Debug.Log("The player broke the bag");
    }

    private void UpdateWeight(Item item)
    {
        _weight += item.Weight;
        var bagCapacityPercent = (_weight * 100) / _maxWeight;

        // Editor does not like Switch here with > < signs
        if (bagCapacityPercent < 50) _renderer.material.color = Color.green;
        else if (bagCapacityPercent > 90) _renderer.material.color = Color.red;
        else _renderer.material.color = Color.yellow;
    }

    private void UpdateSpeed()
    {
        var converter = 0.5f; // Based on difficulty?
        var newSpeed = _weight * converter;

        if (speed - newSpeed < 5) return;
        speed -= newSpeed;
    }
}