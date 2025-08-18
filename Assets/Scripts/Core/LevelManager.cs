using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelTime = 180f;
    [SerializeField] InGameUI inGameUI;
    public EnemyTarget reactor;
    public float RemainingTime { get; private set; }

    public static LevelManager Instance { get; private set; }
    public PlayerController player { get; private set; }

    void Awake()
    {
        Instance = this;
        RemainingTime = levelTime;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        reactor.OnHealthChanged += CheckReactorHealth;
    }

    void Start()
    {
        player.input.Player.Pause.performed += GamePause;
    }

    private void GamePause(InputAction.CallbackContext context)
    {
        PauseGame();
        inGameUI.ShowPauseUI();
    }

    public void ResumeGame()
    {
        inGameUI.HidePauseUI();
        Resume();
    }

    void Update()
    {
        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
        {
            LevelCompleted();
        }
    }

    void LevelCompleted()
    {
        PauseGame();
        inGameUI.ShowLevelCompletedUI();
    }

    void Gameover()
    {
        PauseGame();
        inGameUI.ShowGameoverUI();
    }

    void CheckReactorHealth(int health)
    {
        if (health <= 0)
        {
            Gameover();
        }
    }

    public void BackToMainMenu()
    {
        Resume();
        Debug.Log("BackToMainMenu called, timeScale = " + Time.timeScale);
        GameManager.Instance.ToMainmenu();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        player.input.Player.Disable();
    }

    void Resume()
    {
        Time.timeScale = 1f;
        player.input.Player.Enable();
    }
}
