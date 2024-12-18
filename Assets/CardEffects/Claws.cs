using UnityEngine;
using Zavod.Cards;

namespace Zavod.Cards
{
    internal class Claws : MonoBehaviour
    {
        [SerializeField] private CardData _cardData;
        private GameObject _player;
        private Animator _playerAnimator;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerAnimator = _player.GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _player.AddComponent<PlayerWallMechanics>();
            _playerAnimator.SetBool("ClawsEnabled", true);
        }

        private void OnDisable()
        {
            Destroy(_player.GetComponent<PlayerWallMechanics>());
            _playerAnimator.SetBool("ClawsEnabled", false);
        }
    }
}
