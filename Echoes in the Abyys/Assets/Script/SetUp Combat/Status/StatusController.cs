using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    List<StatusEffect> activeStatuses = new();

    public void AddStatus(StatusEffect newStatus)
    {
        Debug.Log($"{gameObject.name} gained {newStatus.type}");
        foreach (var s in activeStatuses)
        {
            if (s.type == newStatus.type)
            {
                s.duration = Mathf.Max(s.duration, newStatus.duration);
                return;
            }
        }

        activeStatuses.Add(newStatus);
    }

    public void Tick(EnemyHealth enemyHealth)
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            var s = activeStatuses[i];

            Debug.Log($"{gameObject.name} status tick: {s.type}");

            switch (s.type)
            {
                case StatusType.Burn:
                case StatusType.Poison:
                    enemyHealth.TakeDamage(s.value);
                    break;

                case StatusType.Regen:
                    break;
            }

            s.duration--;

            if (s.duration <= 0)
            {
                Debug.Log($"{s.type} expired");
                activeStatuses.RemoveAt(i);
            }
        }
    }

    public void TickPlayer(PlayerHealth playerHealth)
    {
        for (int i = activeStatuses.Count - 1; i >= 0; i--)
        {
            var s = activeStatuses[i];

            switch (s.type)
            {
                case StatusType.Burn:
                case StatusType.Poison:
                    playerHealth.TakeDamage(s.value);
                    break;

                case StatusType.Regen:
                    playerHealth.Heal(s.value);
                    break;
            }

            s.duration--;

            if (s.duration <= 0)
                activeStatuses.RemoveAt(i);
        }
    }
}
