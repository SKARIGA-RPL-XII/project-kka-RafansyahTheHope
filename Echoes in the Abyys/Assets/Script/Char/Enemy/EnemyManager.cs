using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyManager : MonoBehaviour
{
    public List<EnemyController> enemies = new();
    public event Action<EnemyController> OnTargetChanged;
    public EnemySelectIndicator selectIndicator;
    public event Action OnAllEnemiesDead;



    EnemyController selectedTarget;

    void Start()
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health != null)
            {
                e.health.OnDeath += () => OnEnemyDeath(e);
            }
        }
    }

    // === TARGETING ===
    public void SelectTarget(EnemyController enemy)
    {
        if (selectIndicator != null && enemy == null)
        {
            selectIndicator.SetTarget(null);
            return;
        }

        if (enemy == null || enemy.health.currentHP <= 0)
            return;

        selectedTarget = enemy;

        Debug.Log("Target selected: " + enemy.data.enemyName);

        if (selectIndicator != null)
            selectIndicator.SetTarget(enemy.transform);

        OnTargetChanged?.Invoke(selectedTarget);
    }



    public EnemyController GetTarget()
    {
        if (selectedTarget != null && selectedTarget.health.currentHP > 0)
            return selectedTarget;

        return GetFirstAliveEnemy();
    }

    // === HELPERS ===
    public EnemyController GetFirstAliveEnemy()
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health.currentHP > 0)
                return e;
        }
        return null;
    }

    // === ENEMY TURN ===
    public void EnemyTurn(PlayerHealth player)
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health.currentHP > 0)
            {
                e.Attack(player);
            }
        }
    }

    public bool AreAllEnemiesDead()
    {
        foreach (var e in enemies)
        {
            if (e != null && e.health.currentHP > 0)
                return false;
        }
        return true;
    }
    void CheckEnemiesDead()
    {
        if (AreAllEnemiesDead())
        {
            Debug.Log("=== ALL ENEMIES DEFEATED ===");
            OnAllEnemiesDead?.Invoke();
        }
    }
    void OnEnemyDeath(EnemyController deadEnemy)
    {
        Debug.Log("EnemyManager: " + deadEnemy.data.enemyName + " died");

        if (deadEnemy == selectedTarget)
        {
            var next = GetFirstAliveEnemy();
            SelectTarget(next);
        }

        if (AreAllEnemiesDead())
        {
            Debug.Log("=== ALL ENEMIES DEFEATED ===");
            OnAllEnemiesDead?.Invoke();
        }
    }


}
