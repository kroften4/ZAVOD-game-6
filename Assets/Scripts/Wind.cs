using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{
    public float _windStrength = 5f;
    private bool _isWinding = false;

    private Vector2 _windDirection;
    private Rigidbody2D _rb;

    AudioManager _audioManager;

    void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        StartCoroutine(WindCoroutine());
    }

    private IEnumerator WindCoroutine()
    {
        while (true)
        {
            float windDuration = Random.Range(3f, 30f);
            float angle = Random.Range(0f, 360f);
            _windDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Random.value > 0.5f ? Vector2.up.y : Vector2.down.y).normalized;
            _isWinding = true;
            _audioManager.PlaySFX(_audioManager._wind);

            yield return new WaitForSeconds(windDuration);

            _isWinding = false;
            float calmDuration = Random.Range(3f, 30f);
            yield return new WaitForSeconds(calmDuration);
        }
    }

    private void FixedUpdate()
    {
        if (_isWinding)
        {
            _rb.AddForce(_windDirection * _windStrength);
        }
    }
}
