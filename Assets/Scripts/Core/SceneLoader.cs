using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : Singleton<SceneLoader>
{
    public AsyncOperation op { get; private set; }
    public void LoadScene(string sceneName)
    {
        op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        op.allowSceneActivation = false;
    }
}