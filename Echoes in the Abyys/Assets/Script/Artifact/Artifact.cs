using UnityEngine;

public abstract class Artifact : ScriptableObject
{
    [Header("Info")]
    public string artifactName;
    [TextArea] public string description;

    public virtual void OnBattleStart(ArtifactContext context) { }

    public virtual void OnTurnStart(ArtifactContext context) { }

    public virtual void OnCardPlayed(ArtifactContext context, CardInstance card) { }

    public virtual void ModifyDamage(ArtifactContext context, ref int damage) { }

    public virtual int ModifyMaxCardsPerTurn(ArtifactContext context, int baseValue)
    {
        return baseValue;
    }

    public virtual void OnBattleEnd(ArtifactContext context) { }
}
