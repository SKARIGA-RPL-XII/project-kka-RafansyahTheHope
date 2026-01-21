using UnityEngine;

public class CardEffectResolver
{
    public static void Resolve(
        CardInstance card,
        EnemyHealth enemy,
        bool isChainActive)
    {
        if (card.data.cardType == CardType.Attack)
        {
            int damage = card.data.baseDamage;

            if (isChainActive)
            {
                damage = Mathf.RoundToInt(damage * card.data.chainMultiplier);
                Debug.Log("CHAIN BONUS APPLIED!");
            }

            enemy.TakeDamage(damage);
        }

        if (card.data.cardType == CardType.Heal)
        {
            Debug.Log("Heal not implemented yet");
        }
    }
}
