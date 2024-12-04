using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private int _hp = 10;
    [SerializeField] private float _forceBounceValue = 10f;
    [SerializeField] private Text _health;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _health.text = _hp.ToString();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LoseHealth();
        }
    }



    private IEnumerator BecomeInvincible()
    {         
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleTime);

        _isInvincible = false;        
    }

    private void LoseHealth()
    {
        if (_isInvincible) return;
        
        _rb.linearVelocityY = 0f;
        _hp -= 1;       
        _rb.AddForceY(_forceBounceValue, ForceMode2D.Impulse);

        if (_hp <= 0)
        {
            _hp = 0;
            return;
        }

        StartCoroutine(BecomeInvincible());
    }
}
