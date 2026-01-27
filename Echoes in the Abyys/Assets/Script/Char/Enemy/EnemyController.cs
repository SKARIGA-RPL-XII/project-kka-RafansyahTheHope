using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    public EnemyHealth health;
    public StatusController status;

    void Awake()
    {
        if (health == null)
            health = GetComponent<EnemyHealth>();

        if (status == null)
            status = GetComponent<StatusController>();

        if (data != null && health != null)
        {
            health.maxHP = data.maxHP;
        }
    }

    public void Attack(PlayerHealth player)
    {
        if (player == null || data == null) return;
        if (health == null || health.currentHP <= 0) return;

        Debug.Log($"{data.enemyName} attacks for {data.damage}");
        player.TakeDamage(data.damage);
    }
}
