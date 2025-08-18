using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameManagerStateMachine StateMachine { get; private set; }
    public GameObject loadingUI { get; private set; }
    public Player player { get; private set; }
    public GameObject loadingCamera;

    public override void Awake()
    {
        base.Awake();

        StateMachine = new GameManagerStateMachine();
        loadingUI = FindFirstObjectByType<LoadingUI>().gameObject;

    }

    private void Start()
    {
        ToMainmenu();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void StartGame()
    {
        StateMachine.loadingState.SetSceneNameToLoad("Level01", StateMachine.playingState);
        StateMachine.ChangeState(StateMachine.loadingState);
    }

    public void ToMainmenu()
    {
        StateMachine.loadingState.SetSceneNameToLoad("MainMenu", StateMachine.mainmenuState);
        StateMachine.ChangeState(StateMachine.loadingState);
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
