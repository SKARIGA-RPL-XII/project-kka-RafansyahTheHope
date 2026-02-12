using UnityEngine;

public static class CardEffectResolver
{
    public static void Resolve(
        CardInstance card,
        EnemyHealth targetHealth,
        bool isChain,
        ArtifactManager artifactManager,
        PlayerHealth player)
    {
        if (card == null || card.data == null)
            return;

        var data = card.data;

        float multiplier = isChain ? data.chainMultiplier : 1f;

        switch (data.cardType)
        {
            case CardType.Attack:
                ResolveAttack(data, targetHealth, multiplier, artifactManager);
                break;

            case CardType.Heal:
                ResolveHeal(data, player, multiplier, artifactManager);
                break;

            case CardType.Buff:
                Debug.Log("Buff applied (not implemented yet)");
                break;

            case CardType.Debuff:
                Debug.Log("Debuff applied (not implemented yet)");
                break;
        }

        ApplyStatusIfNeeded(data, targetHealth);
    }

    static void ResolveAttack(
        CardData data,
        EnemyHealth targetHealth,
        float multiplier,
        ArtifactManager artifactManager)
    {
        if (targetHealth == null)
            return;

        if (!targetHealth.gameObject.activeSelf)
            return;

        int finalDamage = Mathf.RoundToInt(data.baseDamage * multiplier);

        artifactManager?.ModifyDamage(ref finalDamage);

        if (finalDamage <= 0)
            return;

        Debug.Log($"FINAL DAMAGE: {finalDamage}");

        targetHealth.TakeDamage(finalDamage);
    }

    static void ResolveHeal(
        CardData data,
        PlayerHealth player,
        float multiplier,
        ArtifactManager artifactManager)
    {
        if (player == null)
            return;

        int finalHeal = Mathf.RoundToInt(data.healAmount * multiplier);

        artifactManager?.ModifyDamage(ref finalHeal);

        if (finalHeal <= 0)
            return;

        Debug.Log($"FINAL HEAL: {finalHeal}");

        player.Heal(finalHeal);
    }

    static void ApplyStatusIfNeeded(CardData data, EnemyHealth targetHealth)
    {
        if (!data.appliesStatus)
            return;

        if (targetHealth == null)
            return;

        if (!targetHealth.gameObject.activeSelf)
            return;

        var status = targetHealth.GetComponent<StatusController>();
        if (status == null)
            return;

        status.AddStatus(new StatusEffect(
            data.statusType,
            data.statusValue,
            data.statusDuration
        ));

        Debug.Log($"{targetHealth.gameObject.name} gained {data.statusType}");
    }
}
