using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public string enemyName;

    [Header("Visual")]
    public Sprite sprite;  

    [Header("Combat")]
    public int maxHP;
    public int damage;
}
