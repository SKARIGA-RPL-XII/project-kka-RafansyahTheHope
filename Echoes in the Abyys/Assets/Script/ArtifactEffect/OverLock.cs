using UnityEngine;

[CreateAssetMenu(menuName = "Artifact/Overclock")]
public class OverclockArtifact : Artifact
{
    public override int ModifyMaxCardsPerTurn(ArtifactContext context, int baseValue)
    {
        return baseValue + 1;
    }
}
