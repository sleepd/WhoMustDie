using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameManagerStateMachine StateMachine { get; private set; }
    public GameObject loadingUI { get; private set; }
    public Player player { get; private set; }

    public override void Awake()
    {
        base.Awake();

        StateMachine = new GameManagerStateMachine();
        loadingUI = FindFirstObjectByType<LoadingUI>().gameObject;

    }

    private void Start()
    {
        StateMachine.loadingState.SetSceneNameToLoad("MainMenu", StateMachine.mainmenuState);
        StateMachine.ChangeState(StateMachine.loadingState);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    
}
