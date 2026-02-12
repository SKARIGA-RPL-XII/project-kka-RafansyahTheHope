using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectionManager : MonoBehaviour
{
    [SerializeField] private StageData[] availableStages;
    [SerializeField] private string combatSceneName = "CombatScene";

    private StageData selected;

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; 
    private int lastClickedIndex = -1;

    public void SelectStage(int index)
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (lastClickedIndex == index && timeSinceLastClick <= doubleClickThreshold)
        {
            StartStage(index);
        }
        else
        {
            selected = availableStages[index];
        }

        lastClickTime = Time.time;
        lastClickedIndex = index;
    }

    private void StartStage(int index)
    {
        selected = availableStages[index];

        if (selected == null) return;

        RunContext.Instance.SetStage(selected);
        SceneManager.LoadScene(combatSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
