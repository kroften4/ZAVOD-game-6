using TMPro.EditorUtilities;
using UnityEngine;

public class SpikeyCrawl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private Transform _center;
    [SerializeField] private float _distToGround = 0.5f;
    [SerializeField] private float _distToWall = 0.5f;
    [SerializeField] private LayerMask _platformsLayer;

    private Rigidbody2D _rb;

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

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            transform.position += -ForwardVector * _center.localPosition.x;
            transform.Rotate(0, 0, transform.lossyScale.x * 90);
            transform.position += ForwardVector * (_center.localPosition.x + 0.1f);
        }

        if (IsHittingWall())
        {

        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_center.position, DownVector * _distToGround);
        Gizmos.DrawRay(_center.position, ForwardVector * _distToWall);
    }
}
