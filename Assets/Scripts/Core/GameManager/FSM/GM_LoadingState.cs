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
        Time.timeScale = 1f;
        SceneLoader.Instance.OnLoadingFinished += OnLoadingDone;
        SceneLoader.Instance.LoadScene(sceneName);
        GameManager.Instance.loadingUI.SetActive(true);
        GameManager.Instance.loadingCamera.SetActive(true);
    }

    public void Exit()
    {
        GameManager.Instance.loadingUI.SetActive(false);
    }

    public void Update()
    {

    }
    public void PhysicsUpdate()
    {

    }

    void OnLoadingDone()
    {
        SceneLoader.Instance.OnLoadingFinished -= OnLoadingDone;
        GameManager.Instance.loadingCamera.SetActive(false);
        GameManager.Instance.StateMachine.ChangeState(nextState);
    }
}