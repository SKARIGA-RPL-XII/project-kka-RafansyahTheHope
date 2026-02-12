using UnityEngine;
using TMPro;

public class StageWaveTurnView : MonoBehaviour
{
    [Header("Managers")]
    public RoundManager roundManager;
    public WaveManager waveManager;
    public TurnManager turnManager;

    [Header("UI")]
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI turnText;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        if (roundManager != null && stageText != null)
        {
            stageText.text = $"Stage: {roundManager.CurrentRound}";
        }

        if (waveManager != null && waveText != null)
        {
            waveText.text =
                $"Wave: {waveManager.CurrentWaveNumber} / {waveManager.TotalWaves}";
        }

        if (turnManager != null && turnText != null)
        {
            int currentTurn = turnManager.CurrentTurn;
            int maxTurns = turnManager.MaxTurns;

            turnText.text = $"Turn: {currentTurn} / {maxTurns}";
        }
    }
}
