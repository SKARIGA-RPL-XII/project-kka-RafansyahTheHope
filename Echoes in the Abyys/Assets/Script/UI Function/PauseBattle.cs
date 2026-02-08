using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public TurnManager turnManager;

    private CombatState stateBeforePause;
    private bool isPaused = false;

    public void Pause()
    {
        if (isPaused)
            return;

        if (turnManager.CurrentState == CombatState.BattleEnd)
            return;

        stateBeforePause = turnManager.CurrentState;

        turnManager.SetState(CombatState.Paused);

        Time.timeScale = 0f;
        pausePanel.SetActive(true);

        isPaused = true;
    }

    public void Resume()
    {
        if (!isPaused)
            return;

        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        turnManager.SetState(stateBeforePause);

        isPaused = false;
    }

    public void Surrender()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
