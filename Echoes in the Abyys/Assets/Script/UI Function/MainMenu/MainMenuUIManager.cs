using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;

    [Header("Scene")]
    [SerializeField] private string artifactSceneName = "ArtifactSelectionScene";

    private GameObject currentPanel;

    private void Start()
    {
        ShowMainPanel();
    }

    public void StartGame()
    {
        if (RunContext.Instance != null)
        {
            RunContext.Instance.StartNewRun();
        }

        SceneManager.LoadScene(artifactSceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenSetting()
    {
        SwitchPanel(settingPanel);
    }

    public void OpenCredit()
    {
        SwitchPanel(creditPanel);
    }

    public void BackToMain()
    {
        ShowMainPanel();
    }

    private void ShowMainPanel()
    {
        SwitchPanel(mainPanel);
    }

    private void SwitchPanel(GameObject targetPanel)
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        targetPanel.SetActive(true);
        currentPanel = targetPanel;
    }
}
