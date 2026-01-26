using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    public EnemyHealth health;

    void Awake()
    {
        if (health == null)
            health = GetComponent<EnemyHealth>();

        if (data != null && health != null)
        {
            health.maxHP = data.maxHP;
            health.currentHP = data.maxHP;
        }
    }

    public void Attack(PlayerHealth player)
    {
        if (player == null || data == null) return;

        Debug.Log($"{data.enemyName} attacks for {data.damage}");
        player.TakeDamage(data.damage);
    }
    
}
