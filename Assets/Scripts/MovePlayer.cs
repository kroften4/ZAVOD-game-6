using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 4f;
    public float _pushForce = 10f;
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
    }

    // отталкивание от стенок не работает
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y)) //определяем, что сталкиваемся именно "боком" игрока с платформой
            {
                Vector2 pushDirection = -normal.normalized;
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);
                // Debug.Log("Толчок игрока в направлении: " + pushDirection);
            }
        }
    }
}
