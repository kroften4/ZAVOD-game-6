using UnityEngine;
using UnityEngine.Events;
using Zavod.Cards;

public class EventTesting : MonoBehaviour
{
    [SerializeField] private bool _isDoubleJumpActive = false;
    [SerializeField] private UnityEvent _onEnableDoubleJump;
    [SerializeField] private UnityEvent _onDisableDoubleJump;
    
    [SerializeField] private bool _isClawsActive = false;
    [SerializeField] private UnityEvent _onEnableClaws;
    [SerializeField] private UnityEvent _onDisableClaws;
    
    [SerializeField] private bool _isSpringyBootsActive = false;
    [SerializeField] private UnityEvent _onEnableSpringyBoots;
    [SerializeField] private UnityEvent _onDisableSpringyBoots;    

    private void Update()
    {
        if (_isDoubleJumpActive)
        {
            _onEnableDoubleJump.Invoke();
        }
        else
        {
            _onDisableDoubleJump.Invoke();
        }

        if (_isClawsActive)
        {
            _onEnableClaws.Invoke();
        }
        else
        {
            _onDisableClaws.Invoke();
        }

        if (_isSpringyBootsActive)
        {
            _onEnableSpringyBoots.Invoke();
        }
        else
        {
            _onDisableSpringyBoots.Invoke();
        }
    }
}
