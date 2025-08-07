using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingState : IState
{
    string sceneName;
    IState nextState;

    public void SetSceneNameToLoad(string sceneName, IState nextState)
    {
        this.sceneName = sceneName;
        this.nextState = nextState;
    }
    public void Enter()
    {
        SceneLoader.Instance.LoadScene(sceneName);
        GameManager.Instance.loadingUI.SetActive(true);
    }

    public void Exit()
    {
        GameManager.Instance.loadingUI.SetActive(false);
    }

    public void Update()
    {
        if (SceneLoader.Instance.op.progress >= 0.9f)
        {
            SceneLoader.Instance.op.allowSceneActivation = true;
            GameManager.Instance.StateMachine.ChangeState(nextState);
        }
    }
    public void PhysicsUpdate()
    {

    }
}