using UnityEngine;

public class EnemySelectIndicator : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, -1f, 0);
    public TurnManager turnManager;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
        {
            gameObject.SetActive(true);
            UpdatePosition();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (target != null)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        transform.position = target.position + offset;
    }

    void OnMouseDown()
    {
        if (turnManager == null)
            return;

        if (turnManager.CurrentState != CombatState.PlayerTurn)
            return;

        Debug.Log("Enemy Selected");

    }
}
