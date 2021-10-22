using UnityEngine;

public class Item : MonoBehaviour
{
    public float Weight { get; private set; }
    private Rigidbody _rb;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
        Weight = _rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (_transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
