using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string artifactSceneName = "ArtifactSelectionScene";

    public void OnStartPressed()
    {
        if (RunContext.Instance == null)
        {
            Debug.LogError("RunContext not found in scene.");
            return;
        }

        RunContext.Instance.StartNewRun();
        SceneManager.LoadScene(artifactSceneName);
    }

    public void OnQuitPressed()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
}
