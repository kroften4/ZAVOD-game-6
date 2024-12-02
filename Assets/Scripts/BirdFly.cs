using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BirdFly : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _distToWall = 1.0f;
    [SerializeField] private LayerMask _platformsLayer;
    [SerializeField] private float _flyingDistance = 20f;

    private Rigidbody2D _rb;
    private float _startPositionX;


    private Vector3 ForwardVector
    {
        get => (-transform.right * transform.lossyScale.x).normalized;
    }



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();
        _startPositionX = transform.position.x;
    }
    private void Update()
    {
        transform.position += ForwardVector * _moveSpeed * Time.deltaTime;
    }

    private bool IsDistant()
    {
        return Math.Abs(transform.position.x - _startPositionX) > _flyingDistance;
    }

    private bool IsCollisionWithWall()
    {
        return Physics2D.Raycast(transform.position, ForwardVector, _distToWall, _platformsLayer);
    }

    

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }


    private void FixedUpdate()
    {
        if (IsDistant() || IsCollisionWithWall())
        {
            Flip();
            _startPositionX = transform.position.x;
        }
    }





}