using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] public float _windStrength = 5f;
    [SerializeField] public float _changeInterval = 3f;


    private Vector2 _windDirection;
    private Rigidbody2D _rb;

    AudioManager _audioManager;

    void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeWindDirection", 0f, _changeInterval);
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void ChangeWindDirection()
    {
        float angle = Random.Range(0f, 360f);
        _windDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        ApplyWindForce();
        _audioManager.PlaySFX(_audioManager._wind);
    }

    void ApplyWindForce()
    {
        _rb.AddForce(_windDirection * _windStrength, ForceMode2D.Force);
    }
}
