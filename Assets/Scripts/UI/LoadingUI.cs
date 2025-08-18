using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] float anglePerStep = 15f;
    [SerializeField] float interval = 0.1f;
    [SerializeField] Image loadingImage;

    private float timer = 0f;

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer >= interval)
        {
            loadingImage.transform.Rotate(0f, 0f, anglePerStep);
            timer -= interval;
        }
    }
}