using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Group")]
public class EnemyGroupData : ScriptableObject
{
    public string groupID;
    public List<EnemyData> enemies;
}
