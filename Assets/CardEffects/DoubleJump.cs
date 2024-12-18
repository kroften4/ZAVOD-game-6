using UnityEngine;
using Zavod.Cards;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private CardData _cardData;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerController.MaxAirJumpAmount = 1;
    }

    private void OnDisable()
    {
        _playerController.MaxAirJumpAmount = 0;
    }
}
