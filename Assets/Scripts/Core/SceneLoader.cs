using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneLoader : Singleton<SceneLoader>
{
    public AsyncOperation op { get; private set; }
    [SerializeField] float simulateLoadingTime = 0f;
    private float remainingTime;
    public string currentScene { get; private set; }
    public event Action OnLoadingFinished;
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        remainingTime = simulateLoadingTime;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (!string.IsNullOrEmpty(currentScene))
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(currentScene);
            while (!unloadOp.isDone)
                yield return null;
        }


        op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        op.allowSceneActivation = false;
        currentScene = sceneName;
        while (op.progress < 0.9f || remainingTime > 0f)
        {
            remainingTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        StartCoroutine(AllowSceneActivation());
    }

    IEnumerator AllowSceneActivation()
    {
        if (op != null)
        {
            op.allowSceneActivation = true;
        }

        while (!op.isDone)
        {
            yield return null;
        }
        Scene loadedScene = SceneManager.GetSceneByName(currentScene);
        SceneManager.SetActiveScene(loadedScene);
        OnLoadingFinished?.Invoke();
    }
}