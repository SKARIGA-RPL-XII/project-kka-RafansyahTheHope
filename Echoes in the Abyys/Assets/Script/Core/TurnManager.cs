using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public CombatState currentState;

    public EnemyManager enemyManager;
    public PlayerHealth player;
    public HandController handController;

    public float turnTransitionDelay = 0.75f;
    public float enemyAttackDelay = 0.75f;

    bool isProcessingTurn = false;

    void Start()
    {
        StartPlayerTurn();

        if (player != null)
            player.OnDeath += OnPlayerDeath;
    }

    void OnDestroy()
    {
        if (player != null)
            player.OnDeath -= OnPlayerDeath;
    }

    public void StartPlayerTurn()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("=== PLAYER TURN ===");

        if (handController != null)
            handController.SetInputLock(false);

        isProcessingTurn = false;
    }

    public void EndPlayerTurn()
    {
        if (currentState != CombatState.PlayerTurn) return;
        if (isProcessingTurn) return;

        StartCoroutine(EnemyTurnRoutine());
    }

    IEnumerator EnemyTurnRoutine()
    {
        isProcessingTurn = true;
        currentState = CombatState.EnemyTurn;
        Debug.Log("=== ENEMY TURN ===");

        if (handController != null)
            handController.SetInputLock(true);

        yield return new WaitForSeconds(turnTransitionDelay);

        if (enemyManager != null && player != null)
        {
            enemyManager.EnemyTurn(player);
        }
        else
        {
            Debug.LogError("TurnManager: EnemyManager or Player not assigned!");
        }

        yield return new WaitForSeconds(enemyAttackDelay);

        if (currentState != CombatState.BattleEnd)
        {
            StartPlayerTurn();
        }
    }

    void OnPlayerDeath()
    {
        currentState = CombatState.BattleEnd;
        Debug.Log("=== GAME OVER ===");

        if (handController != null)
            handController.SetInputLock(true);

        StopAllCoroutines();
    }
}
