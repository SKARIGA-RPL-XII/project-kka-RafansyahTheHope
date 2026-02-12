using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stage")]
public class StageData : ScriptableObject
{
    public string stageID;
    public string stageName;

    [Header("Waves in this Stage")]
    public List<WaveData> waves;
}
