using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData data;

    public void Attack()
    {
        Debug.Log($"{data.enemyName} attacks for {data.damage}");
    }
}
