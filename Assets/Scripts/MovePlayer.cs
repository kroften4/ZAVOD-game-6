using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody2D _rb;
    private float _speed = 4f;
    private int k = 0;
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
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            k++;
            _rb.linearVelocityY = _speed;
            if(k > 2)
            {
                _rb.linearVelocityY = 0;
            }
        }
        else
        {
            _rb.linearVelocityX = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        k = 0;
    }

}
