using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zavod.Cards;

public class ShopCardDisplay : MonoBehaviour
{
    public CardData CardData;
    [SerializeField] private Image _cardCoverImage;
    [SerializeField] private TextMeshProUGUI _cardNameText;

    private void Update()
    {
        // i am sorry
        if (CardData == null) return;
        _cardCoverImage.sprite = CardData.Sprite;
        _cardNameText.text = CardData.Name;
    }
}
