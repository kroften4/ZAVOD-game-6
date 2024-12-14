using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(GroundChecker))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 42;
    [SerializeField] private float _xAxisDumping = 10;
    private float _xInput;

    [Header("Jumping")]
    public float NormalJumpStrength = 12.5f ;
    public float BoostedJumpStrength = 16f;
    public int MaxAirJumpAmount = 1;

    public float CurrentJumpStrength;
    private int _timesAirJumped = 0;
    private Rigidbody2D _rb;
    private GroundChecker _groundChecker;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();

        CurrentJumpStrength = NormalJumpStrength;
    }

    private void OnMovement(InputValue value)
    {
        _xInput = value.Get<float>();
    }
    
    private void OnJump()
    {
        if (_groundChecker.IsGrounded())
        {
            _rb.linearVelocityY = 0;
            _rb.AddForceY(CurrentJumpStrength, ForceMode2D.Impulse);
        }
        else if (_groundChecker.GetWallDirection() == 0 && _timesAirJumped < MaxAirJumpAmount)
        {
            _timesAirJumped++;
            _rb.linearVelocityY = 0;
            _rb.AddForceY(CurrentJumpStrength, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (_groundChecker.IsGrounded())
            _timesAirJumped = 0;

        _rb.AddForceX(_xInput * _moveSpeed);
        _rb.AddForceX(-_rb.linearVelocityX * _xAxisDumping);
    }
}
