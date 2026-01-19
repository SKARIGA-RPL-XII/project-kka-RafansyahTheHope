using UnityEngine;

public class CardEffectResolver
{
    public static void Resolve(CardInstance card)
    {
        switch (card.data.cardType)
        {
            case CardType.Attack:
                Debug.Log($"Attack for {card.data.value}");
                break;

            case CardType.Heal:
                Debug.Log($"Heal for {card.data.value}");
                break;

            case CardType.Buff:
                Debug.Log("Apply Buff");
                break;

            case CardType.Debuff:
                Debug.Log("Apply Debuff");
                break;
        }
    }
}
