using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyController> enemies = new();

    public event Action<EnemyController> OnTargetChanged;
    public event Action OnAllEnemiesDead;

    public EnemySelectIndicator selectIndicator;
    public WaveManager waveManager;

    EnemyController selectedTarget;

    // === REGISTRATION ===
    public void RegisterEnemy(EnemyController enemy)
    {
        if (enemy == null) return;

        enemies.Add(enemy);

        if (enemy.health != null)
        {
            enemy.health.OnDeath += () => OnEnemyDeath(enemy);
        }
    }

    public void ClearEnemies()
    {
        enemies.Clear();
        SelectTarget(null);
    }

    // === TARGETING ===
    public void SelectTarget(EnemyController enemy)
    {
        if (selectIndicator != null && enemy == null)
        {
            selectIndicator.SetTarget(null);
            selectedTarget = null;
            return;
        }

        if (enemy == null || enemy.health == null || enemy.health.currentHP <= 0)
            return;

        selectedTarget = enemy;

        Debug.Log("Target selected: " + enemy.data.enemyName);

        if (selectIndicator != null)
            selectIndicator.SetTarget(enemy.transform);

        OnTargetChanged?.Invoke(selectedTarget);
    }

    public EnemyController GetTarget()
    {
        if (selectedTarget != null &&
            selectedTarget.health != null &&
            selectedTarget.health.currentHP > 0)
        {
            return selectedTarget;
        }

        return GetFirstAliveEnemy();
    }

    // === HELPERS ===
    public EnemyController GetFirstAliveEnemy()
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health != null && e.health.currentHP > 0)
                return e;
        }
        return null;
    }

    // === ENEMY TURN ===
    public void EnemyTurn(PlayerHealth player)
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health != null && e.health.currentHP > 0)
            {
                e.Attack(player);
            }
        }
    }

    public bool AreAllEnemiesDead()
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health != null && e.health.currentHP > 0)
                return false;
        }
        return true;
    }

    // === DEATH HANDLING ===
    void OnEnemyDeath(EnemyController deadEnemy)
    {
        Debug.Log("EnemyManager: " + deadEnemy.data.enemyName + " died");

        if (deadEnemy == selectedTarget)
        {
            SelectTarget(GetFirstAliveEnemy());
        }

        if (AreAllEnemiesDead())
        {
            Debug.Log("=== ALL ENEMIES DEFEATED ===");

            OnAllEnemiesDead?.Invoke();

            if (waveManager != null)
                waveManager.NotifyWaveCleared();
        }
    }
}
