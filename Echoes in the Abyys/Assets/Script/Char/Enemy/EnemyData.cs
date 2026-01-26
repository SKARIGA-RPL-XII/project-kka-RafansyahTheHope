using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public int damage;

}
