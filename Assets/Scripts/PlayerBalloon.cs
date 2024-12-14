using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBalloon : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 5;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityY = Mathf.Clamp(_rb.linearVelocityY, -_fallSpeed, float.MaxValue);
    }
}
