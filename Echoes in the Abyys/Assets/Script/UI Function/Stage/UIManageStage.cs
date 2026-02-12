using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
