using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] Toggle setting1;
    [SerializeField] Toggle setting2;
    [SerializeField] Toggle setting3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Quit();
    }

    public void ShowSettingPanel()
    {
        LoadSettings();
        settingPanel.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void SaveSettings()
    {
        SaveData data = new SaveData();
        data.setting1 = setting1.isOn;
        data.setting2 = setting2.isOn;
        data.setting3 = setting3.isOn;
        SaveSystem.Save(data);
    }

    void LoadSettings()
    {
        SaveData loaded = SaveSystem.Load();
        setting1.isOn = loaded.setting1;
        setting2.isOn = loaded.setting2;
        setting3.isOn = loaded.setting3;
    }

    public void DeleteSettingFile()
    {
        SaveSystem.DeleteSave();
    }
}
