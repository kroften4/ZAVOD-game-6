using UnityEngine;

public class InstantKill : MonoBehaviour
{
    [SerializeField] private bool _enabled = false;
    private DieMobs _instantKill;
    void Start()
    {
        _instantKill = GameObject.FindGameObjectWithTag("Enemy").GetComponent<DieMobs>();
    }

    private void Update()
    {
        if (_enabled == true) 
            _instantKill._inRangeForInstantKill = true;

    }

}
