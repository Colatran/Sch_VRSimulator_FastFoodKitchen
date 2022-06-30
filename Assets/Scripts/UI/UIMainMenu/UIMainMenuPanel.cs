using UnityEngine;

public class UIMainMenuPanel : MonoBehaviour
{
    [SerializeField] protected UIMainMenu mainMenu;

    protected void ResetToMainMenu()
    {
        mainMenu.SetPanel(0);
    }
}
