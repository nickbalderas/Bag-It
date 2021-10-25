using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    private GameManager _gameManager;
    private BagUI _bagUI;
    private ScoreManager _score;

    private readonly Vector3 _defaultBagPosition = new Vector3(4f, 0.4f, 0f);
    private float maxZPos = 18f;
    private float minZPos = -11f;
    private Transform _transform;

    public float speed;
    private float _weight;
    private float _maxWeight;

    public Camera camRef;
    public Image bagUI;
    public Slider weightUI;

    private float _lastItemWeight;
    private int _itemsInBag;
    private int _baseItemValue = 5;


    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _score = GetComponent<ScoreManager>();
        _bagUI = GetComponent<BagUI>();
        _transform = transform;
    }

    void Start()
    {
        NewBag();
    }

    void Update()
    {
        if (!_gameManager.GameTimer.IsActive) return;
        HandleMovement();
        HandleBagUI();
        HandleBagClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && _gameManager.GameTimer.IsActive)
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
            _transform.Translate(direction * (speed * Time.deltaTime));
        }
    }

    private void HandleBagUI()
    {
        Vector3 bagPos = camRef.WorldToScreenPoint(_transform.position);
        bagUI.transform.position = bagPos + new Vector3(0, 0, 0);
    }

    private void AddItemToBag(Item item)
    {
        if (_lastItemWeight > 0 && item.Weight > _lastItemWeight || _weight + item.Weight > _maxWeight)
        {
            BreakBag();
            return;
        }

        _itemsInBag += 1;
        _lastItemWeight = item.Weight;
        UpdateWeight(item);
        UpdateSpeed();
        UpdateScore();
        UpdateBagUI(item);
    }

    private void HandleBagClose()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        _gameManager.GameScore.UpdateScore(_score.Score);
        _bagUI.HandleNotification(Color.blue, "Bag Closed", 1.5f);
        NewBag();
    }

    private void BreakBag()
    {
        _bagUI.HandleNotification(Color.red, "The Bag Broke!", 1.5f);
        NewBag();
    }

    private void UpdateWeight(Item item)
    {
        _weight += item.Weight;
        weightUI.SetValueWithoutNotify(_weight);
    }

    private void UpdateScore()
    {
        var bagCapacityPercent = (_weight * 100) / _maxWeight;
        var multiplier = bagCapacityPercent < 25 ? 1 : bagCapacityPercent > 70 ? 3 : 2;
        var penalty = bagCapacityPercent < 25 ? -10 : bagCapacityPercent < 70 ? -5 : 0;
        if (multiplier > _score.ScoreMultiplier) _bagUI.HandleNotification(Color.green,$"{"Multiplier Increase: x" + multiplier}", 1.5f);
        {
            
        }
        _score.UpdateScoreMultiplier(multiplier);
        _score.UpdateScore(_baseItemValue);
        _score.ApplyPenalty(penalty);
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
        _lastItemWeight = 0;
        _weight = 0;
        _maxWeight = 50;
        weightUI.value = _weight;
        weightUI.minValue = 0;
        weightUI.maxValue = _maxWeight;
        speed = 50f;
        _itemsInBag = 0;
        _score.Reset();
        _score.ApplyPenalty(-10);
        UpdateBagUI(null);
    }

    private void UpdateBagUI(Item item)
    {
        _bagUI.capacity.text = $"{_weight + "/" + _maxWeight}";
        _bagUI.lastItemMass.text = item && item.Weight > 0 ? $"{item.Weight}" : "-";
        _bagUI.numberOfItems.text = $"{_itemsInBag}";
        _bagUI.itemValue.text = $"{_baseItemValue}";
        _bagUI.bagMultiplier.text = $"{_score.ScoreMultiplier}";
        _bagUI.bagPenalty.text = $"{_score.Penalty}";
        _bagUI.bagScore.text = $"{_score.Score}";
    }
}