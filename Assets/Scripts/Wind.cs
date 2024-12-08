using UnityEngine;

public class Wind : MonoBehaviour
{
    public float _windStrength = 5f;
    public float _windInterval = 3f;
    private bool _isWinding = false;


    private Vector2 _windDirection;
    private Rigidbody2D _rb;

    AudioManager _audioManager;

    void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(ChangeWindDirection), 0f, _windInterval);
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void ChangeWindDirection()
    {
        float angle = Random.Range(0f, 360f);
        _windDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        _audioManager.PlaySFX(_audioManager._wind);
        _isWinding = !_isWinding;
    }

    private void FixedUpdate()
    {
        if (_isWinding)
            _rb.AddForce(_windDirection * _windStrength);
    }
}
