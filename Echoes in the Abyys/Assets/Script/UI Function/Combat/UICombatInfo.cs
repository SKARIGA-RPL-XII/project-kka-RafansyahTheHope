using UnityEngine;
using TMPro;

public class UICombatInfo : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private TextMeshProUGUI infoText;

    void Update()
    {
        if (waveManager == null || turnManager == null) return;

        string stageName = "Unknown";

        if (RunContext.Instance != null && RunContext.Instance.selectedStage != null)
            stageName = RunContext.Instance.selectedStage.stageName;

        infoText.text =
            $"Wave: {waveManager.CurrentWaveNumber}/{waveManager.TotalWaves}   " +
            $"Turn: {turnManager.CurrentTurn}/{turnManager.MaxTurns}   " +
            $"Stage: {stageName}";
    }
}
