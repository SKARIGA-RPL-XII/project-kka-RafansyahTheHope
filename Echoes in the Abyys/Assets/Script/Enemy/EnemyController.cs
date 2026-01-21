using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;
    public EnemyHealth health;

    void Awake()
    {
        if (health == null)
            health = GetComponent<EnemyHealth>();
    }

    public void Attack()
    {
        Debug.Log($"{data.enemyName} attacks for {data.damage}");
    }
}
