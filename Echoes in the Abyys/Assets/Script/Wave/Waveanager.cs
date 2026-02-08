using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    List<WaveData> activeWaves = new();

    [Header("Spawn Setup")]
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public EnemyManager enemyManager;

    [Header("Timing")]
    public float nextWaveDelay = 1.5f;

    int currentWaveIndex = -1;

    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveCleared;
    public event Action OnChapterCleared;

    // === ROUND SYSTEM HOOK ===
    public void SetWaves(List<WaveData> waves)
    {
        activeWaves = waves;
        currentWaveIndex = -1;
    }

    public void StartWaves()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        currentWaveIndex++;

        if (currentWaveIndex >= activeWaves.Count)
        {
            Debug.Log("=== STAGE CLEARED ===");
            OnChapterCleared?.Invoke();
            return;
        }

        StartCoroutine(SpawnWaveRoutine());
    }

    IEnumerator SpawnWaveRoutine()
    {
        yield return new WaitForSeconds(nextWaveDelay);

        Debug.Log("=== SPAWNING WAVE " + (currentWaveIndex + 1) + " ===");

        var wave = activeWaves[currentWaveIndex];

        if (enemyManager != null)
            enemyManager.ClearEnemies();

        for (int i = 0; i < wave.enemies.Count; i++)
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("WaveManager: No spawn points assigned!");
                break;
            }

            var spawnPoint = spawnPoints[i % spawnPoints.Length];

            var obj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            var controller = obj.GetComponent<EnemyController>();
            controller.Init(wave.enemies[i]);

            enemyManager.RegisterEnemy(controller);
        }

        OnWaveStarted?.Invoke(currentWaveIndex + 1);
    }

    public void NotifyWaveCleared()
    {
        Debug.Log("=== WAVE CLEARED ===");
        OnWaveCleared?.Invoke(currentWaveIndex + 1);

        StartNextWave();
    }

    // === UI HELPERS ===
    public int CurrentWaveNumber => currentWaveIndex + 1;
    public int TotalWaves => activeWaves.Count;
    public WaveData CurrentWave =>
        currentWaveIndex >= 0 && currentWaveIndex < activeWaves.Count
        ? activeWaves[currentWaveIndex]
        : null;
}
