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
        [SerializeField] private string _name;
        [SerializeField] private CardType type;
        [SerializeField] private Sprite sprite;
        [SerializeField] [TextArea]
        private string _desription;
    }
}
