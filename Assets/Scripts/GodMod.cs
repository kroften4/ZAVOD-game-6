using Unity.VisualScripting;
using UnityEngine;

public class GodMod : MonoBehaviour
{
    private GetDamage _getDamage;
    private GodMod _godMod;
    [SerializeField] private bool _enabled = false;
    void Start()
    {
        _getDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<GetDamage>();
    }

    private void Update()
    {
        if(_enabled == true)
            _getDamage._isGodMod = true;
        else
            _getDamage._isGodMod = false;
    


        
    }
    
}
