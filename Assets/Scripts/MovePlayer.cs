using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody2D _rb;
    private float _speed = 4f;
    private float jumpForce = 5f;
    private bool isGround;
    private float rayDistance = 0.6f;
    private bool doubleJump = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        { 
            isGround = true;
            doubleJump = false;
        }
        else
            isGround = false;

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
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if(!doubleJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            doubleJump = true;
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        
    }

    
}
