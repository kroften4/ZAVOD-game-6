using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;

    [Header("Jumping")]
    [SerializeField] private float _normalJumpStrength = 12.5f;
    [SerializeField] private float _boostedJumpStrength = 16f;
    [SerializeField] private float _maxAirJumpAmount = 1;
    private float _currentJumpStrength;

    [Header("Wall Jump")]
    [SerializeField] private bool _wallJumpMechanics = false;
    [SerializeField] private float _wallJumpStrength = 5;
    [SerializeField] private float _wallJumpTime = 0.4f;
    [SerializeField] private float _wallSlideSpeed = 3;
    private float _wallJumpTimer = 0;
    private bool _isWallJumping = false;
    private int _wallJumpDirection;

    private Vector2 _input;
    private bool _leftBtn = false;
    private bool _rightBtn = false;
    private bool _jumpBtn = false;

    [Header("Raycast checks")]
    [SerializeField] private float _groundRaycastLength = 0.02f;
    [SerializeField] private float _wallRaycastLength = 0.02f;
    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rb;
    private float _borderOffsetX;
    private float _borderOffsetY;
    private int _timesAirJumped = 0;

    public void OnEnableDoubleJump()
    {
        _maxAirJumpAmount = 1;
    }
    
    public void OnDisableDoubleJump()
    {
        _maxAirJumpAmount = 0;
    }

    public void OnEnableSpringyBoots()
    {
        _currentJumpStrength = _boostedJumpStrength;
    }
    
    public void OnDisableSpringyBoots()
    {
        _currentJumpStrength = _normalJumpStrength;
    }
    
    public void OnEnableClaws()
    {
        _wallJumpMechanics = true;
    }
    
    public void OnDisableClaws()
    {
        _wallJumpMechanics = false;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _borderOffsetX = GetComponent<Collider2D>().bounds.extents.x;
        _borderOffsetY = GetComponent<Collider2D>().bounds.extents.y;

        _currentJumpStrength = _normalJumpStrength;
    }

    public void LeftBtnDown()
    {
        _leftBtn = true;
    }

    public void LeftBtnUp()
    {
        _leftBtn = false;
    }

    public void RightBtnDown()
    {
        _rightBtn = true;
    }
    
    public void RightBtnUp()
    {
        _rightBtn = false;
    }
    
    public void JumpBtnClick()
    {
        _jumpBtn = true;
    }

    void Update()
    {
        bool rightArrow = Input.GetKey(KeyCode.RightArrow) || _rightBtn,
            leftArrow = Input.GetKey(KeyCode.LeftArrow) || _leftBtn;
        if (leftArrow && rightArrow)
            _input.x = 0;
        else if (leftArrow)
            _input.x = -1;
        else if (rightArrow)
            _input.x = 1;

        bool upArrow = Input.GetKeyDown(KeyCode.UpArrow) || _jumpBtn;
        _jumpBtn = false;
        if (upArrow)
            _input.y = 1;            
    }

    private void FixedUpdate()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded)
            _timesAirJumped = 0;

        _rb.linearVelocityX = _input.x * _moveSpeed;

        int wallDirection = GetWallDirection();
        if (_input.y == 1)
        {
            if (_wallJumpMechanics && wallDirection != 0 && !isGrounded)
            {
                _wallJumpTimer = 0;
                _wallJumpDirection = -wallDirection;
                _isWallJumping = true;
                _rb.linearVelocityY = 0;
                _rb.AddForceY(_currentJumpStrength, ForceMode2D.Impulse);
            }
            else if (isGrounded)
            {
                _rb.linearVelocityY = 0;
                _rb.AddForceY(_currentJumpStrength, ForceMode2D.Impulse);
            }
            else if (_timesAirJumped < _maxAirJumpAmount)
            {
                _timesAirJumped++;
                _rb.linearVelocityY = 0;
                _rb.AddForceY(_currentJumpStrength, ForceMode2D.Impulse);
            }

            _input.y = 0;
        }
        else if (_wallJumpMechanics && wallDirection != 0 && !isGrounded)
        {
            _rb.linearVelocityY = Mathf.Clamp(_rb.linearVelocityY, -_wallSlideSpeed, float.MaxValue);
        }

        if (_isWallJumping)
        {
            _rb.linearVelocityX = _wallJumpDirection * _wallJumpStrength;
            _wallJumpTimer += Time.fixedDeltaTime;
            if (_wallJumpTimer >= _wallJumpTime) 
            { 
                _isWallJumping = false;
                _wallJumpTimer = 0;
            }
        }

        _input.x = 0;
        _input.y = 0;
    }

    private int GetWallDirection()
    {
        bool leftWall = Physics2D.Raycast(
            new Vector2(transform.position.x - _borderOffsetX, transform.position.y),
            Vector2.left,
            _wallRaycastLength,
            _groundLayerMask
        );
        bool rightWall = Physics2D.Raycast(
          new Vector2(transform.position.x + _borderOffsetX, transform.position.y),
          Vector2.right,
          _wallRaycastLength,
          _groundLayerMask
        );


        if (leftWall)
            return -1;
        if (rightWall)
            return 1;
        return 0;
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
