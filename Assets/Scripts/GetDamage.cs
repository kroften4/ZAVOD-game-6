using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private int _hp = 10;
    [SerializeField] private float _forceBounceValue = 10f;
    [SerializeField] private float _forceBounceValueForWeakness = 15f;
    [SerializeField] private Text _health;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private float _invincibleTime = 1;
    [SerializeField] public bool _isGodMod;
    [SerializeField] public bool _isWeakness;

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
        if(_isGodMod == false)
        {
            _isInvincible = true;
            yield return new WaitForSeconds(_invincibleTime);

            _isInvincible = false;
        }
    }

    private void LoseHealth()
    {
        if (_isGodMod == false)
        {
            if (_isInvincible) return;

            _rb.linearVelocityY = 0f;
            _hp -= 1;
            if (_isWeakness == false)
                _rb.AddForceY(_forceBounceValue, ForceMode2D.Impulse);
            else
                _rb.AddForceY(_forceBounceValueForWeakness, ForceMode2D.Impulse);

            if (_hp <= 0)
            {
                _hp = 0;
                return;
            }

            StartCoroutine(BecomeInvincible());
        }
    }
}
