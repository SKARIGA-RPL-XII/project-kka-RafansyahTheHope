using UnityEngine;

public class RunContext : MonoBehaviour
{
    public static RunContext Instance;

    [Header("Run State")]
    public bool runActive;

    [Header("Stage Data")]
    public StageData selectedStage;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewRun()
    {
        runActive = true;
        selectedStage = null;
    }

    public void EndRun()
    {
        runActive = false;
        selectedStage = null;
    }

    public void SetStage(StageData stage)
    {
        selectedStage = stage;
    }
}
