using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _platformsLayer;
    [SerializeField] private float _jumpForce = 5f;
    private Rigidbody2D _rb;
    private bool _isGround;
    private bool _doubleJump = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_isGround)
            {
                Jump();
            }
            else if (!_doubleJump)
            {
                _doubleJump = true;
                Jump();
            }
        }
    }

    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _platformsLayer) != 0)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    _isGround = true;
                    _doubleJump = false;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _platformsLayer) != 0)
        {
            _isGround = false;
        }
    }
}
