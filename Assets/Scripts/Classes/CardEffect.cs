using Zavod.Cards;
using UnityEngine;

namespace Zavod.Cards
{
    internal abstract class CardEffect : MonoBehaviour, ICardEffect
    {
        abstract public void Activate();
    }
}
