using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    public Artifact activeArtifact;

    private ArtifactContext context;

    void Awake()
    {
        context = new ArtifactContext();
    }

    public void Initialize(
        TurnManager turn,
        PlayerHealth player,
        EnemyManager enemy,
        HandController hand)
    {
        context.turnManager = turn;
        context.player = player;
        context.enemyManager = enemy;
        context.handController = hand;
    }

    public void OnBattleStart()
    {
        activeArtifact?.OnBattleStart(context);
    }

    public void OnTurnStart()
    {
        activeArtifact?.OnTurnStart(context);
    }

    public void OnCardPlayed(CardInstance card)
    {
        activeArtifact?.OnCardPlayed(context, card);
    }

    public void ModifyDamage(ref int damage)
    {
        activeArtifact?.ModifyDamage(context, ref damage);
    }

    public int ModifyMaxCardsPerTurn(int baseValue)
    {
        if (activeArtifact == null)
            return baseValue;

        return activeArtifact.ModifyMaxCardsPerTurn(context, baseValue);
    }

    public void OnBattleEnd()
    {
        activeArtifact?.OnBattleEnd(context);
    }
}
