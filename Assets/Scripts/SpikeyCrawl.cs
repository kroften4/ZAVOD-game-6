using TMPro.EditorUtilities;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SpikeyCrawl : MonoBehaviour
{
    [SerializeField] private bool _onlyMoveLeftRight = false;
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private Transform _center;
    [SerializeField] private float _distToGround = 0.5f;
    [SerializeField] private float _distToWall = 0.5f;
    [SerializeField] private LayerMask _platformsLayer;

    private Rigidbody2D _rb;

    AudioManager _audioManager;

    private Vector3 ForwardVector
    {
        get => (-transform.right * transform.lossyScale.x).normalized;
    }
    
    private Vector3 DownVector
    {
        get => (-transform.up.normalized * transform.lossyScale.y).normalized;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        transform.position += ForwardVector * _moveSpeed * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(_center.position, DownVector, _distToGround, _platformsLayer);
    }

    private bool IsHittingWall()
    {
        return Physics2D.Raycast(_center.position, ForwardVector, _distToWall, _platformsLayer);
    }

    private void Flip()
    {
        float pivotDistance = -_center.localPosition.x;
        transform.position += ForwardVector * pivotDistance;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position += -ForwardVector * (pivotDistance - 0.1f);
    }

    private void Rotate()
    {
        float pivotDistance = -_center.localPosition.x;
        transform.position += ForwardVector * pivotDistance;
        transform.Rotate(0, 0, transform.lossyScale.x * 90);
        transform.position += ForwardVector * 0.1f;
        transform.position += -ForwardVector * pivotDistance;
    }

    private void WallRotate()
    {
        float pivotDistance = -_center.localPosition.x;
        transform.position += ForwardVector * (pivotDistance + _distToWall);
        transform.Rotate(0, 0, transform.lossyScale.x * -90);
        transform.position += ForwardVector * 0.1f;
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            if (_onlyMoveLeftRight)
            {
                Flip();
            }
            else
            {
                Rotate();
                // _audioManager.PlaySFX(_audioManager._mobs); // некоторые мобы слишком часто поворачивают это звучит не очень-то хорошо
            }
        }

        if (IsHittingWall())
        {
            _audioManager.PlaySFX(_audioManager._mobs);
            if (_onlyMoveLeftRight)
                Flip();
            else
            {
                WallRotate();
            }
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_center.position, DownVector * _distToGround);
        Gizmos.DrawRay(_center.position, ForwardVector * _distToWall);
    }
}
