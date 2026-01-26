using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyController> enemies = new();

    EnemyController selectedTarget;

    // === TARGETING ===
    public void SelectTarget(EnemyController enemy)
    {
        if (enemy == null || enemy.health.currentHP <= 0)
            return;

        selectedTarget = enemy;
        Debug.Log($"Target selected: {enemy.data.enemyName}");
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
}
