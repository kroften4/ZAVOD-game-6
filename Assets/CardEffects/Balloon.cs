using UnityEngine;
using Zavod.Cards;

namespace Zavod.Cards
{
    internal class Balloon : MonoBehaviour
    {
        [SerializeField] private CardData _cardData;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _player.AddComponent<PlayerBalloon>();
        }

        private void OnDisable()
        {
            Destroy(_player.GetComponent<PlayerBalloon>());
        }
    }
}
