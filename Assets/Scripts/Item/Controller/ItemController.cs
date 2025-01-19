using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float _floatingSpeed;
    protected Rigidbody2D _rb;

    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = new Vector2(Random.Range(-1f, 1), Random.Range(-1f, 1)).normalized * _floatingSpeed;
    }

    protected void FixedUpdate()
    {
        if (_rb.linearVelocity.magnitude < _floatingSpeed)
        {
            _rb.linearVelocity += _rb.linearVelocity.normalized * 0.5f * Time.fixedDeltaTime;
        }
    }
}
