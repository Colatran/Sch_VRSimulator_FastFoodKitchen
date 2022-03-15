using UnityEngine;
using TMPro;

public class XrSettingsCanvas : MonoBehaviour
{
    [SerializeField] XRSettingsController settingsController;
    [SerializeField] TMP_Text text_rot;
    [SerializeField] TMP_Text text_tele;


    public void Rotacao()
    {
        if(settingsController.ContinuousTurn)
        {
            settingsController.ContinuousTurn = false;
            text_rot.text = "Rotação Estalido";
        }
        else
        {
            settingsController.ContinuousTurn = true;
            text_rot.text = "Rotação Continua";
        }

        settingsController.SetSettings();
    }

    public void Teleport()
    {
        if (settingsController.TeleportDash)
        {
            settingsController.TeleportDash = false;
            text_tele.text = "Teletransporte Piscar";
        }
        else
        {
            settingsController.TeleportDash = true;
            text_tele.text = "Teletransporte Dash";
        }

        settingsController.SetSettings();
    }
}
