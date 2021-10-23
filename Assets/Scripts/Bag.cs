using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    private GameManager _gameManager;
    private readonly Vector3 _defaultBagPosition = new Vector3(4f, 1.2f, 0f);
    private float maxZPos = 18f;
    private float minZPos = -11f;
    private Transform _transform;

    public float speed;
    private float _weight;
    private float _maxWeight;

    public Camera camRef;
    public Image bagUI;
    public Slider weightUI;
    
    private ScoreManager _score;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _score = GetComponent<ScoreManager>();
        _transform = transform;
    }
    
    void Start()
    {
        NewBag();
    }
    
    void Update()
    {
        HandleMovement();
        HandleBagUI();
        HandleBagClose();
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

    private void HandleBagUI()
    {
        Vector3 bagPos = camRef.WorldToScreenPoint(_transform.position);
        bagUI.transform.position = bagPos + new Vector3(0, 29, 0);
    }

    private void AddItemToBag(Item item)
    {
        if (_weight + item.Weight > _maxWeight)
        {
            BreakBag();
            return;
        }
        UpdateWeight(item);
        UpdateSpeed();
        _score.UpdateScore(5);
    }

    private void HandleBagClose()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        _gameManager.gameScore.UpdateScore(_score.Score);
        NewBag();
    }

    private void BreakBag()
    {
        NewBag();
    }

    private void UpdateWeight(Item item)
    {
        _weight += item.Weight;
        weightUI.SetValueWithoutNotify(_weight);
        
        // Editor does not like Switch here with > < signs
        var bagCapacityPercent = (_weight * 100) / _maxWeight;
        if (bagCapacityPercent < 25) _score.UpdateScoreMultiplier(1);
        else if (bagCapacityPercent > 70) _score.UpdateScoreMultiplier(3);
        else _score.UpdateScoreMultiplier(2);
    }

    private void UpdateSpeed()
    {
        var converter = 0.5f; // Based on difficulty?
        var newSpeed = _weight * converter;

        if (speed - newSpeed < 5) return;
        speed -= newSpeed;
    }

    private void NewBag()
    {
        _weight = 0;
        _maxWeight = 30;
        _score.Reset();
        _transform.position = _defaultBagPosition;
        weightUI.value = _weight;
        weightUI.minValue = 0;
        weightUI.maxValue = _maxWeight;
        speed = 50f;
    }
}