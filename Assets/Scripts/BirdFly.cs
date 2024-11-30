using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BirdFly : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private Transform _center;
    [SerializeField] private float _distToWall = 0.5f;
    [SerializeField] private LayerMask _platformsLayer;

    private Rigidbody2D _rb;
    private float _startPositionX;


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
        _startPositionX = transform.position.x;
        Debug.Log($"Начальная позиция по X: {_startPositionX}");
    }
    private void Update()
    {
        transform.position += ForwardVector * _moveSpeed * Time.deltaTime;
    }

    private bool isDistant()
    {
        if (_startPositionX > 0) return Math.Abs(transform.localPosition.x) > _startPositionX;
        return transform.localPosition.x > Math.Abs(_startPositionX);
    }

    private bool isCollisionWithWall()
    {
         return Physics2D.Raycast(_center.position, ForwardVector, _distToWall, _platformsLayer);
    }

    private bool isReadyToFlip()
    {
        return isDistant() || isCollisionWithWall();
    }

    private void Flip()
    {
        float pivotDistance = -_center.localPosition.x;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }


    private void FixedUpdate()
    {

        Debug.Log(transform.localPosition.x);
        if (isReadyToFlip())
            Flip();
    }





}
