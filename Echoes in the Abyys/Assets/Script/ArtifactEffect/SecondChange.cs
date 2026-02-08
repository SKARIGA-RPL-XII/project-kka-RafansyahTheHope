using UnityEngine;

[CreateAssetMenu(menuName = "Artifact/Second Chance")]
public class SecondChanceArtifact : Artifact
{
    bool usedThisTurn = false;

    public override void OnTurnStart(ArtifactContext context)
    {
        usedThisTurn = false;
    }

    public override void OnCardPlayed(ArtifactContext context, CardInstance card)
    {
        if (!usedThisTurn)
        {
            context.handController.DrawOneCard();
            usedThisTurn = true;
        }
    }
}
 