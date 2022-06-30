using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel_Main : UIMainMenuPanel
{
    [Header("Main")]
    [SerializeField] Button button_start;
    [SerializeField] Button button_settings;
    [SerializeField] Button button_quit;



    private void OnEnable()
    {
        button_start.onClick.AddListener(Button_Start_OnClick);
        button_settings.onClick.AddListener(Button_Settings_OnClick);
        button_quit.onClick.AddListener(Button_Quit_OnClick);
    }
    private void OnDisable()
    {
        button_start.onClick.RemoveListener(Button_Start_OnClick);
        button_settings.onClick.RemoveListener(Button_Settings_OnClick);
        button_quit.onClick.RemoveListener(Button_Quit_OnClick);
    }


    
    private void Button_Start_OnClick()
    {
        mainMenu.SetPanel(1);
    }
    private void Button_Settings_OnClick()
    {
        mainMenu.SetPanel(2);
    }
    private void Button_Quit_OnClick()
    {
        mainMenu.SetPanel(3);
    }
}
