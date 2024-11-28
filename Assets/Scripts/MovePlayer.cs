using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody2D _rb;
    private float _speed = 4f;
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
            _rb.linearVelocityX = 0f;
        }
    }
}
