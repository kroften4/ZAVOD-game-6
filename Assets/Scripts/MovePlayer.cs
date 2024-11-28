using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody2D _rb;
    private float _speed = 4f;
    private int k = 0, movex = 0;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movex = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movex = -1;
        }
        else
        {
            movex = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && k < 2)
        {
            k++;
            _rb.linearVelocityY = _speed;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && k > 2)
        {
            Input.GetKeyDown(KeyCode.UpArrow).Equals(false);
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _speed * movex;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        k = 0;
    }

}
