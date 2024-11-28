using UnityEngine;

public class JumpPlayer : MonoBehaviour
{

    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _platformsLayer;
    [SerializeField] private float _jumpForce = 5f;
    private bool _isGround;
    [SerializeField] private float _rayDistance = 0.6f;
    private bool _doubleJump = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
        {
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        else if(!_doubleJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _doubleJump = true;
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }       
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, _rayDistance, _platformsLayer);
        if (hit.collider != null)
        {
            _isGround = true;
            _doubleJump = false;
        }
        else
            _isGround = false;
    }
}
