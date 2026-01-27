using UnityEngine;

public class CardEffectResolver
{
    public static void Resolve(CardInstance card, EnemyHealth targetHealth, bool isChain)
    {
        if (card == null || card.data == null)
            return;

        var data = card.data;

        float multiplier = isChain ? data.chainMultiplier : 1f;

        switch (data.cardType)
        {
            case CardType.Attack:
                if (targetHealth == null) return;

                int damage = Mathf.RoundToInt(data.baseDamage * multiplier);

                Debug.Log($"Attack {targetHealth.gameObject.name} for {damage}");

                targetHealth.TakeDamage(damage);
                break;

            case CardType.Heal:
                var player = Object.FindObjectOfType<PlayerHealth>();
                if (player == null) return;

                int heal = Mathf.RoundToInt(data.healAmount * multiplier);

                Debug.Log($"Heal player for {heal}");

                player.Heal(heal);
                break;

            case CardType.Buff:
                Debug.Log("Buff applied (not implemented yet)");
                break;

            case CardType.Debuff:
                Debug.Log("Debuff applied (not implemented yet)");
                break;
        }

        if (data.appliesStatus && targetHealth != null)
        {
            var status = targetHealth.GetComponent<StatusController>();
            if (status != null)
            {
                status.AddStatus(new StatusEffect(
                    data.statusType,
                    data.statusValue,
                    data.statusDuration
                ));

                Debug.Log($"{targetHealth.gameObject.name} gained {data.statusType}");
            }
        }
    }
}
