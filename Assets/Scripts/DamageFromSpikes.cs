using UnityEngine;
using UnityEngine.UI;


public class DamageFromSpikes : MonoBehaviour

{

    [SerializeField] private int _hpForHero = 100;
    private Rigidbody2D _rb;
    [SerializeField] private Text _health;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _health.text = _hpForHero.ToString();
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spike")
        {
            _hpForHero -= 1;
        
        }
    }



}
