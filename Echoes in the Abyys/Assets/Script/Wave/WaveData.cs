using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Wave/Wave Data")]
public class WaveData : ScriptableObject
{
    [Header("Enemies")]
    public List<EnemyData> enemies;

    [Header("Rules")]
    public int maxTurns = 20;
}
