using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] float _pushForce = 10f;
    private Rigidbody2D _rb;
    private Vector2 _pushDirection;
    private bool _isTouchingWall;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.linearVelocityX = _speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.linearVelocityX = -_speed;
        }
        else
        {
            _rb.linearVelocityX = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow) && _isTouchingWall)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.AddForce(_pushDirection * _pushForce, ForceMode2D.Impulse);
        }
        _isTouchingWall = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                _pushDirection = normal.normalized;
                _isTouchingWall = true;
            }
        }
    }
}
