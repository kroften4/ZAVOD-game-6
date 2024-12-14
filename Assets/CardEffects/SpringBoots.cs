using UnityEngine;
using Zavod.Cards;

namespace Zavod.Cards
{
    internal class SpringBoots : MonoBehaviour
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
            _playerController.CurrentJumpStrength = _playerController.BoostedJumpStrength;
        }

        private void OnDisable()
        {
            _playerController.CurrentJumpStrength = _playerController.NormalJumpStrength;
        }
    }
}