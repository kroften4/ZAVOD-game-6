using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private int _hp = 10;
    [SerializeField] private Vector2 _forceDigit = new Vector2(0f, 5f);
    [SerializeField] private Text _health;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _health.text = _hp.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(ToDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ToDamage()
    {
        while (_hp > 0)
        {
            _hp -= 1;
            _rb.AddForce(_forceDigit, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
