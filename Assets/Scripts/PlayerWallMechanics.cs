using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController), typeof(PlayerInput), typeof(GroundChecker))]
public class PlayerWallMechanics : MonoBehaviour
{
    [SerializeField] private float _wallJumpPushStrength = 5;
    [SerializeField] private float _wallSlideSpeed = 3;
    private int _wallJumpDirection;
    private Rigidbody2D _rb;
    private GroundChecker _groundChecker;
    private PlayerController _playerController;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        _playerController = GetComponent<PlayerController>();
    }

    private void OnJump()
    {
        int wallDirection = _groundChecker.GetWallDirection();
        if (wallDirection != 0 && !_groundChecker.IsGrounded())
        {
            _wallJumpDirection = -wallDirection;
            _rb.linearVelocityY = 0;
            _rb.AddForceY(_playerController.CurrentJumpStrength, ForceMode2D.Impulse);
            _rb.AddForceX(_wallJumpDirection * _wallJumpPushStrength, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        bool isGrounded = _groundChecker.IsGrounded();
        int wallDirection = _groundChecker.GetWallDirection();

        if (wallDirection != 0 && !isGrounded)
        {
            _rb.linearVelocityY = Mathf.Clamp(_rb.linearVelocityY, -_wallSlideSpeed, float.MaxValue);
        }
    }
}
