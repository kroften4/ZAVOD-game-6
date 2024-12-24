using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{
    public float _windStrength = 5f;
    private bool _isWinding = false;

    private Vector2 _windDirection;
    private Rigidbody2D _rb;

    AudioManager _audioManager;

    public GameObject _windPrefab;
    public GameObject _windStraightPrefab;

    private GameObject _wind;
    private GameObject _windStraight;

    private ParticleSystem _windParticleSystem;
    private ParticleSystem _windStraightParticleSystem;

    private Vector2 offset = new Vector2(1f, 0f);

    void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        _wind = Instantiate(_windPrefab, transform.position, Quaternion.identity);
        _windStraight = Instantiate(_windStraightPrefab, transform.position, Quaternion.identity);

        _windParticleSystem = _wind.GetComponent<ParticleSystem>();
        _windStraightParticleSystem = _windStraight.GetComponent<ParticleSystem>();

        StartCoroutine(WindCoroutine());
    }

    private IEnumerator WindCoroutine()
    {
        while (true)
        {
            float _windDuration = Random.Range(3f, 30f);
            float angle = Random.Range(0f, 360f);
            _windDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Random.value > 0.5f ? Vector2.up.y : Vector2.down.y).normalized;
            _isWinding = true;

            if (_windParticleSystem != null)
            {
                _windParticleSystem.Play();
                SetParticleVisibility(_windParticleSystem, true);
            }
            if (_windStraightParticleSystem != null)
            {
                _windStraightParticleSystem.Play();
                SetParticleVisibility(_windStraightParticleSystem, true);
            }

            UpdateWindParticles();

            if (_audioManager._wind.Length > 0)
            {
                int randomIndex = Random.Range(0, _audioManager._wind.Length);
                _audioManager.SFXSource.PlayOneShot(_audioManager._wind[randomIndex]);
            }

            yield return new WaitForSeconds(_windDuration);

            if (_windParticleSystem != null)
            {
                _windParticleSystem.Stop();
                SetParticleVisibility(_windParticleSystem, false);
            }
            if (_windStraightParticleSystem != null)
            {
                _windStraightParticleSystem.Stop();
                SetParticleVisibility(_windStraightParticleSystem, false);
            }

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

    private void UpdateWindParticles()
    {
        if (_wind != null)
        {
            _wind.transform.position = (Vector2)_rb.position + offset;
            _wind.transform.up = _windDirection;
        }

        if (_windStraight != null)
        {
            _windStraight.transform.position = (Vector2)_rb.position + offset;
            _windStraight.transform.up = _windDirection;
        }
    }

    private void SetParticleVisibility(ParticleSystem _ps, bool isVisible)
    {
        _ps.gameObject.SetActive(isVisible);
    }
}
