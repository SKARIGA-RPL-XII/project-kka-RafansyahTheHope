using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{

    [Header("State")]
    [SerializeField] private CombatState currentState = CombatState.PlayerTurn;
    public CombatState CurrentState => currentState;

    [Header("References")]
    public EnemyManager enemyManager;
    public PlayerHealth player;
    public HandController handController;
    public WaveManager waveManager;
    public ArtifactManager artifactManager;


    [Header("Timing")]
    public float turnTransitionDelay = 0.75f;
    public float enemyAttackDelay = 0.75f;


    [Header("Turn Rules")]
    [SerializeField] private int currentTurn = 0;

    public int CurrentTurn => currentTurn;

    public int MaxTurns
    {
        get
        {
            if (waveManager == null || waveManager.CurrentWave == null)
                return 0;

            return waveManager.CurrentWave.maxTurns;
        }
    }

    private bool isProcessingTurn = false;
    private bool battleEnded = false;

    void Start()
    {
        if (artifactManager != null)
        {
            artifactManager.Initialize(this, player, enemyManager, handController);
            artifactManager.OnBattleStart();
        }

        if (player != null)
            player.OnDeath += OnPlayerDeath;

        StartPlayerTurn();
    }

    void OnDestroy()
    {
        if (player != null)
            player.OnDeath -= OnPlayerDeath;
    }

    public void StartPlayerTurn()
    {
        if (battleEnded) return;
        if (currentState == CombatState.Paused) return;

        currentState = CombatState.PlayerTurn;
        currentTurn++;

        Debug.Log($"=== PLAYER TURN {currentTurn}/{MaxTurns} ===");

        handController?.SetInputLock(false);

        artifactManager?.OnTurnStart();

        CheckTurnLimit();
    }

    public void EndPlayerTurn()
    {
        if (battleEnded) return;
        if (currentState != CombatState.PlayerTurn) return;
        if (isProcessingTurn) return;

        StartCoroutine(EnemyTurnRoutine());
    }

    IEnumerator EnemyTurnRoutine()
    {
        isProcessingTurn = true;
        currentState = CombatState.EnemyTurn;

        handController?.SetInputLock(true);

        yield return new WaitForSeconds(turnTransitionDelay);

        enemyManager?.EnemyTurn(player);

        yield return new WaitForSeconds(enemyAttackDelay);

        isProcessingTurn = false;

        StartPlayerTurn();
    }

    void CheckTurnLimit()
    {
        if (MaxTurns <= 0) return;

        if (currentTurn > MaxTurns)
            OnTurnLimitReached();
    }

    void OnTurnLimitReached()
    {
        battleEnded = true;
        currentState = CombatState.BattleEnd;

        handController?.SetInputLock(true);

        Debug.Log("=== PLAYER FAILED: TURN LIMIT ===");

        StopAllCoroutines();

        artifactManager?.OnBattleEnd();
    }

    public void ResetTurnCounter()
    {
        currentTurn = 0;
        Debug.Log("=== TURN COUNTER RESET ===");
    }

    void OnPlayerDeath()
    {
        battleEnded = true;
        currentState = CombatState.BattleEnd;

        handController?.SetInputLock(true);

        Debug.Log("=== GAME OVER ===");

        StopAllCoroutines();

        artifactManager?.OnBattleEnd();
    }

    public void SetState(CombatState newState)
    {
        currentState = newState;
    }
}
