using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject levelCompletedUI;
    [SerializeField] GameObject gameoverUI;

    public void ShowPauseUI()
    {
        pauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        pauseUI.SetActive(false);
    }

    public void ShowLevelCompletedUI()
    {
        levelCompletedUI.SetActive(true);
    }

    public void HideLevelCompletedUI()
    {
        levelCompletedUI.SetActive(false);
    }

    public void ShowGameoverUI()
    {
        gameoverUI.SetActive(true);
    }

    public void HideGameoverUI()
    {
        gameoverUI.SetActive(false);
    }
}
