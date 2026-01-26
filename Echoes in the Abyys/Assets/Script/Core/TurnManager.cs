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

    void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("=== PLAYER TURN ===");

        if (handController != null)
            handController.SetInputLock(false);
    }

    public void EndPlayerTurn()
    {
        if (currentState != CombatState.PlayerTurn) return;

        StartCoroutine(EnemyTurnRoutine());
    }

    IEnumerator EnemyTurnRoutine()
    {
        currentState = CombatState.EnemyTurn;
        Debug.Log("=== ENEMY TURN ===");

        if (handController != null)
            handController.SetInputLock(true);

        yield return new WaitForSeconds(turnTransitionDelay);

        // 👾 SEMUA MUSUH HIDUP MENYERANG
        if (enemyManager != null && player != null)
        {
            enemyManager.EnemyTurn(player);
        }
        else
        {
            Debug.LogError("TurnManager: EnemyManager or Player not assigned!");
        }

        yield return new WaitForSeconds(enemyAttackDelay);

        StartPlayerTurn();
    }
}
