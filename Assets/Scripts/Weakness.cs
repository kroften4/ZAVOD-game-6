using UnityEngine;

public class Weakness : MonoBehaviour
{
    private GetDamage _getDamage;
    [SerializeField] private bool _enabled = false;
    void Start()
    {
        _getDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<GetDamage>();
        
    }

    private void Update()
    {
        if (_enabled == true)
            _getDamage._isWeakness = true;
        else
            _getDamage._isWeakness = false;
    }
}
