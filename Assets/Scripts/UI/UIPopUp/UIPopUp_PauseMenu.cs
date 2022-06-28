using UnityEngine;
using UnityEngine.UI;

public class UIPopUp_PauseMenu : UIPopUp
{
    [Header("")]
    [SerializeField] GameObject Panel_Confirmation;

    [Header("Buttons")]
    [SerializeField] Button button_Close;
    [SerializeField] Button button_QuitRequest;
    [SerializeField] Button button_QuitCancel;
    [SerializeField] Button button_QuitConfirm;

    private void OnEnable()
    {
        button_Close.onClick.AddListener(CallClose);
        button_QuitRequest.onClick.AddListener(OpenQuitConfirmation);
        button_QuitCancel.onClick.AddListener(CloseQuitConfirmation);
        button_QuitConfirm.onClick.AddListener(Quit);
    }
    private void OnDisable()
    {
        button_Close.onClick.RemoveListener(CallClose);
        button_QuitRequest.onClick.RemoveListener(OpenQuitConfirmation);
        button_QuitCancel.onClick.RemoveListener(CloseQuitConfirmation);
        button_QuitConfirm.onClick.RemoveListener(Quit);
    }


    public void OpenQuitConfirmation()
    {
        Panel_Confirmation.SetActive(true);
    }
    public void CloseQuitConfirmation()
    {
        Panel_Confirmation.SetActive(false);
    }
    public void Quit()
    {
        GameManager.QuitToMainMenu();
    }
}
