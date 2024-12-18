using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zavod.Cards;

public class ShopCardGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _options;
    [SerializeField] private List<CardData> _cards;

    public void GenerateCards()
    {
        List<CardData> selectedCards = _cards.OrderBy(x => Random.Range(0, _cards.Count)).Take(_options.Count).ToList();
        for (int i = 0; i < _options.Count; i++)
        {
            _options[i].GetComponent<ShopCardDisplay>().CardData = selectedCards[i];
        }
    }
}
