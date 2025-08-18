using UnityEngine;
using UnityEngine.UIElements;

public class GameHUD : MonoBehaviour
{
    private UIDocument uiRoot;
    private VisualElement crosshair_dot;
    private VisualElement crosshair;
    private Label reactorHealth;
    private Label timeRemaining;
    private Label ammo;

    void Awake()
    {
        uiRoot = GetComponent<UIDocument>();
        crosshair_dot = uiRoot.rootVisualElement.Q<VisualElement>("Crosshair_Dot");
        crosshair = uiRoot.rootVisualElement.Q<VisualElement>("Crosshair");
        reactorHealth = uiRoot.rootVisualElement.Q<Label>("ReactorHealth");
        timeRemaining = uiRoot.rootVisualElement.Q<Label>("RemainingTime");
        ammo = uiRoot.rootVisualElement.Q<Label>("Ammo");
        LevelManager.Instance.reactor.OnHealthChanged += UpdateReactorHealth;
        LevelManager.Instance.player.OnAmmoChanged += UpdateAmmo;


    }

    void Update()
    {
        UpdateRemainingTime();
    }

    public void ShowDotCrosshair()
    {
        crosshair.AddToClassList("hide");
        crosshair_dot.RemoveFromClassList("hide");
    }

    public void ShowCrosshair()
    {
        crosshair_dot.AddToClassList("hide");
        crosshair.RemoveFromClassList("hide");
    }

    void UpdateReactorHealth(int health)
    {
        reactorHealth.text = $"Reactor HP: {health}";
    }

    void UpdateRemainingTime()
    {
        timeRemaining.text = $"Hold on: {Mathf.CeilToInt(LevelManager.Instance.RemainingTime)}";
    }

    void UpdateAmmo(int current, int max)
    {
        ammo.text = $"{current}/{max}";
    }
}
