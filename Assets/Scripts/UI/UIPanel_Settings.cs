using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanel_Settings : MonoBehaviour
{
    [SerializeField] XRSettingsData xRSettingsData;

    [SerializeField] Slider slider_teleportDash;
    [SerializeField] TMP_Text text_teleportDash;

    [SerializeField] Slider slider_rotationContinuous;
    [SerializeField] TMP_Text text_rotationContinuous;



    private void OnEnable()
    {
        slider_teleportDash.onValueChanged.AddListener(OnTeleportDashChanged);
        slider_rotationContinuous.onValueChanged.AddListener(OnRotationContinuousChanged);

        bool teleportDash = xRSettingsData.TeleportDash;
        UISetTeleportDash(teleportDash);
        if (teleportDash) slider_teleportDash.SetValueWithoutNotify(1);
        else slider_teleportDash.SetValueWithoutNotify(0);

        bool continuousTurn = xRSettingsData.ContinuousTurn;
        UISetContinuousTurn(continuousTurn);
        if (continuousTurn) slider_rotationContinuous.SetValueWithoutNotify(1);
        else slider_rotationContinuous.SetValueWithoutNotify(1);
    }
    private void OnDisable()
    {
        slider_teleportDash.onValueChanged.RemoveListener(OnTeleportDashChanged);
        slider_rotationContinuous.onValueChanged.RemoveListener(OnRotationContinuousChanged);
    }

    public void OnTeleportDashChanged(float value)
    {
        SetTeleportDash(value == 1);
    }
    public void OnRotationContinuousChanged(float value)
    {
        SetContinuousTurn(value == 1);
    }

    private void SetTeleportDash(bool val)
    {
        xRSettingsData.TeleportDash = val;
        UISetTeleportDash(val);
    }
    private void SetContinuousTurn(bool val)
    {
        xRSettingsData.ContinuousTurn = val;
        UISetContinuousTurn(val);
    }

    private void UISetTeleportDash(bool val)
    {
        if (val)
            text_teleportDash.text = "Dash";
        else
            text_teleportDash.text = "Piscar";
    }
    private void UISetContinuousTurn(bool val)
    {
        if (val)
            text_rotationContinuous.text = "Continuo";
        else
            text_rotationContinuous.text = "Estalo";
    }
}
