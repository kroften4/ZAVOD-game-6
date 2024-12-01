using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _jumpForce = 12.5f;
    [SerializeField] private float _maxJumpAmount = 2;
    private Vector2 _input;
    private Rigidbody2D _rb;
    private float _borderOffsetX;
    private float _borderOffsetY;
    [SerializeField] private float _groundRaycastLength = 0.02f;
    [SerializeField] private float _wallRaycastLength = 0.02f;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private int _timesJumped = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _borderOffsetX = GetComponent<Collider2D>().bounds.extents.x;
        _borderOffsetY = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update()
    {
        bool rightArrow = Input.GetKey(KeyCode.RightArrow),
            leftArrow = Input.GetKey(KeyCode.LeftArrow);
        if (leftArrow && rightArrow)
            _input.x = 0;
        else if (leftArrow)
            _input.x = -1;
        else if (rightArrow)
            _input.x = 1;
        else
            _input.x = 0;

        bool upArrow = Input.GetKeyDown(KeyCode.UpArrow);
        if (upArrow)
            _input.y = 1;
        else
            _input.y = 0;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
            _timesJumped = 0;

        _rb.linearVelocityX = _input.x * _moveSpeed;

        if (_input.y == 1 && _timesJumped < _maxJumpAmount)
        {
            _rb.linearVelocityY = 0;
            _rb.AddForceY(_jumpForce, ForceMode2D.Impulse);
            if (!IsWallOnLeft() && !IsWallOnRight())
                _timesJumped++;
            _input.y = 0;
        }
    }

    private bool IsWallOnLeft()
    {
        return Physics2D.Raycast(
            new Vector2(transform.position.x - _borderOffsetX, transform.position.y),
            Vector2.left,
            _wallRaycastLength,
            _groundLayerMask
        );
    }

    private bool IsWallOnRight()
    {
        return Physics2D.Raycast(
            new Vector2(transform.position.x + _borderOffsetX, transform.position.y), 
            Vector2.right,
            _wallRaycastLength,
            _groundLayerMask
        );
    }

    private bool IsGrounded()
    {
        
        float _raycastOffsetY = _borderOffsetY;
        bool hitLeft = Physics2D.Raycast(
            new Vector2(transform.position.x - _borderOffsetX, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        bool hitCenter = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        bool hitRight = Physics2D.Raycast(
            new Vector2(transform.position.x + _borderOffsetX, transform.position.y - _raycastOffsetY),
            Vector2.down,
            _groundRaycastLength,
            _groundLayerMask
        );
        return hitLeft || hitCenter || hitRight;
    }
}
