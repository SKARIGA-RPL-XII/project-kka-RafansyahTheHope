using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    public EnemyHealth health;
    public StatusController status;

    void Awake()
    {
        if (!health)
            health = GetComponent<EnemyHealth>();

        if (!status)
            status = GetComponent<StatusController>();
    }

    // DIPANGGIL OLEH WAVEMANAGER
    public void Init(EnemyData enemyData)
    {
        data = enemyData;

        if (health != null && data != null)
        {
            health.maxHP = data.maxHP;
            health.currentHP = data.maxHP;
        }

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && data.sprite != null)
        {
            sr.sprite = data.sprite;
        }
    }

    public void Attack(PlayerHealth player)
    {
        if (!player || data == null) return;

        Debug.Log($"{data.enemyName} attacks for {data.damage}");
        player.TakeDamage(data.damage);
    }
}
