using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(GroundChecker))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 42;
    [SerializeField] private float _xAxisDumping = 10;
    private float _xInput;

    [Header("Jumping")]
    [SerializeField] private float _normalJumpStrength = 12.5f;
    [SerializeField] private float _boostedJumpStrength = 16f;
    [SerializeField] private float _maxAirJumpAmount = 1;

    AudioManager _audioManager;

    public float CurrentJumpStrength { get; private set; }
    private int _timesAirJumped = 0;
    private Rigidbody2D _rb;
    private GroundChecker _groundChecker;

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
        CurrentJumpStrength = _boostedJumpStrength;
    }
    
    public void OnDisableSpringyBoots()
    {
        CurrentJumpStrength = _normalJumpStrength;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();

        CurrentJumpStrength = _normalJumpStrength;
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnMovement(InputValue value)
    {
        _xInput = value.Get<float>();
        if (_audioManager._walkSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, _audioManager._walkSounds.Length);
            _audioManager.SFXSource.PlayOneShot(_audioManager._walkSounds[randomIndex]);
        }
    }
    
    private void OnJump()
    {
        if (_groundChecker.IsGrounded())
        {
            if (_audioManager._jumpScream.Length > 0)
            {
                int randomIndex = Random.Range(0, _audioManager._jumpScream.Length);
                _audioManager.SFXSource.PlayOneShot(_audioManager._jumpScream[randomIndex]);
            }
            if (_audioManager._jumpSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, _audioManager._jumpSounds.Length);
                _audioManager.SFXSource.PlayOneShot(_audioManager._jumpSounds[randomIndex]);
            }
            _rb.linearVelocityY = 0;
            _rb.AddForceY(CurrentJumpStrength, ForceMode2D.Impulse);
        }
        else if (_groundChecker.GetWallDirection() == 0 && _timesAirJumped < _maxAirJumpAmount)
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
