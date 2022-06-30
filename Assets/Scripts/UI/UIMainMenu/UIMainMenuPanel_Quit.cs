using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuPanel_Quit : UIMainMenuPanel
{
    [Header("Quit")]
    [SerializeField] Button button_yes;
    [SerializeField] Button button_no;



    private void OnEnable()
    {
        button_yes.onClick.AddListener(button_yes_onClick);
        button_no.onClick.AddListener(button_no_onClick);
    }
    private void OnDisable()
    {
        button_yes.onClick.RemoveListener(button_yes_onClick);
        button_no.onClick.RemoveListener(button_no_onClick);
    }



    private void button_yes_onClick()
    {
        Application.Quit();
    }
    private void button_no_onClick()
    {
        ResetToMainMenu();
    }
}
