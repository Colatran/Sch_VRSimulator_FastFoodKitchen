using UnityEngine;
using TMPro;

public class TEST_XrSettingsCanvas : MonoBehaviour
{
    [SerializeField] XRSettingsData xRSettingsData;
    [SerializeField] TMP_Text text_rot;
    [SerializeField] TMP_Text text_tele;


    public void Rotacao()
    {
        if(xRSettingsData.ContinuousTurn)
        {
            xRSettingsData.ContinuousTurn = false;
            text_rot.text = "Rotação Estalido";
        }
        else
        {
            xRSettingsData.ContinuousTurn = true;
            text_rot.text = "Rotação Continua";
        }
    }

    public void Teleport()
    {
        if (xRSettingsData.TeleportDash)
        {
            xRSettingsData.TeleportDash = false;
            text_tele.text = "Teletransporte Piscar";
        }
        else
        {
            xRSettingsData.TeleportDash = true;
            text_tele.text = "Teletransporte Dash";
        }
    }
}
