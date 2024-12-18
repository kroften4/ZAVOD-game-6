using UnityEngine;

namespace Zavod.Cards
{
    public enum CardType
    {
        Active, Passive
    }

    [CreateAssetMenu(fileName = "NewCard", menuName = "Scriptable Objects/CardData")]
    public class CardData : ScriptableObject
    {
        public string Name;
        public CardType Type;
        public Sprite Sprite;
        [TextArea] public string Desription;
    }
}
