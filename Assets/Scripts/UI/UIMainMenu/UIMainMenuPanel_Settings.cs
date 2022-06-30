using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel_Settings : UIMainMenuPanel
{
    [Header("Settings")]
    [SerializeField] Button button_quit;



    private void OnEnable()
    {
        button_quit.onClick.AddListener(button_quit_onClick);
    }
    private void OnDisable()
    {
        button_quit.onClick.RemoveListener(button_quit_onClick);
    }

    private void button_quit_onClick()
    {
        ResetToMainMenu();
    }
}
