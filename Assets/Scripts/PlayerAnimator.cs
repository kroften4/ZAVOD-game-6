using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(GroundChecker))]
[RequireComponent(typeof(PlayerInput), typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private GroundChecker _groundChecker;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _linearVelocity;
    private bool _isHuggingWall;
    private int _wallDirection;
    private float _xInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundChecker = GetComponent<GroundChecker>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMovement(InputValue value)
    {
        _xInput = value.Get<float>();
    }

    private void Update()
    {
        _animator.SetFloat("VelocityY", _linearVelocity.y);
        _animator.SetBool("IsWalled", _isHuggingWall);
        switch (_xInput)
        {
            case -1:
                _animator.SetBool("LeftKey", true);
                _spriteRenderer.flipX = true;
                break;
            case 1:
                _animator.SetBool("RightKey", true);
                _spriteRenderer.flipX = false;
                break;
            case 0:
                _animator.SetBool("LeftKey", false);
                _animator.SetBool("RightKey", false);
                break;
        }
        if (_isHuggingWall)
        {
            switch (_wallDirection)
            {
                case -1:
                    _spriteRenderer.flipX = true;
                    break;
                case 1:
                    _spriteRenderer.flipX = false;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        _wallDirection = _groundChecker.GetWallDirection();
        _linearVelocity = _rb.linearVelocity;
        _isHuggingWall = _wallDirection != 0 && !_groundChecker.IsGrounded();
    }
}
