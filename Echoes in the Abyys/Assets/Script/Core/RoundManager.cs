using UnityEngine;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    [Header("Stage Setup")]
    public List<WaveData> stageWaves = new();
    public WaveManager waveManager;
    public TurnManager turnManager;

    [Header("Progress")]
    [SerializeField] int _currentRound = 1;   

    public int CurrentRound => _currentRound; 

    bool stageRunning = false;

    void Start()
    {
        if (waveManager == null || turnManager == null)
        {
            Debug.LogError("RoundManager: Missing WaveManager or TurnManager reference!");
            return;
        }

        waveManager.OnChapterCleared += OnStageCleared;

        StartStage();
    }

    void OnDestroy()
    {
        if (waveManager != null)
            waveManager.OnChapterCleared -= OnStageCleared;
    }

    // === STAGE FLOW ===
    public void StartStage()
    {
        if (stageRunning)
            return;

        stageRunning = true;

        Debug.Log($"=== START ROUND {_currentRound} ===");

        waveManager.SetWaves(stageWaves);
        waveManager.StartWaves();

        turnManager.ResetTurnCounter();
    }

    void OnStageCleared()
    {
        Debug.Log($"=== ROUND {_currentRound} CLEARED ===");

        stageRunning = false;
        _currentRound++;

        StartStage();
    }
}
