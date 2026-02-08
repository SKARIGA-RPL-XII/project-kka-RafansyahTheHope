using UnityEngine;

public class EnemyClickTarget : MonoBehaviour
{
    public EnemyController controller;
    public EnemyManager manager;
    public TurnManager turnManager;  

    void Awake()
    {
        if (controller == null)
            controller = GetComponent<EnemyController>();

        if (turnManager == null)
            turnManager = FindObjectOfType<TurnManager>(); 
    }

    void OnMouseDown()
    {
        if (turnManager == null)
            return;

        if (turnManager.CurrentState != CombatState.PlayerTurn)
            return;

        if (manager != null)
            manager.SelectTarget(controller);
    }
}
