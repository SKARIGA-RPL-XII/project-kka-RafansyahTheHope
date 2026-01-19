using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public CombatState currentState;

    void Start()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("Player Turn Start");
    }

    public void EndPlayerTurn()
    {
        currentState = CombatState.EnemyTurn;
        Debug.Log("Enemy Turn Start");
    }

    public void EndEnemyTurn()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("Player Turn Start");
    }
}
