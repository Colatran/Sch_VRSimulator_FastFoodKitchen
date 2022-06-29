using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanel_Settings : MonoBehaviour
{
    [SerializeField] Slider slider_teleportDash;
    [SerializeField] TMP_Text text_teleportDash;

    [SerializeField] Slider slider_rotationContinuous;
    [SerializeField] TMP_Text text_rotationContinuous;

    XRSettingsController settingsController;



    private void OnEnable()
    {
        slider_teleportDash.onValueChanged.AddListener(OnTeleportDashChanged);
        slider_rotationContinuous.onValueChanged.AddListener(OnRotationContinuousChanged);

        bool teleportDash = GameManager.PlayerSettingsController.TeleportDash;
        if (teleportDash) slider_teleportDash.SetValueWithoutNotify(1);
        SetTeleportDash(teleportDash);

        bool continuousTurn = GameManager.PlayerSettingsController.ContinuousTurn;
        if (continuousTurn) slider_rotationContinuous.SetValueWithoutNotify(1);
        SetContinuousTurn(continuousTurn);
    }
    private void OnDisable()
    {
        slider_teleportDash.onValueChanged.RemoveListener(OnTeleportDashChanged);
        slider_rotationContinuous.onValueChanged.RemoveListener(OnRotationContinuousChanged);
    }

    private void Start()
    {
        settingsController = GameManager.PlayerSettingsController;
    }


    public void OnTeleportDashChanged(float value)
    {
        if (value == 1) GameManager.PlayerSettingsController.TeleportDash = true;
        else GameManager.PlayerSettingsController.TeleportDash = false;
    }
    public void OnRotationContinuousChanged(float value)
    {
        if (value == 1) GameManager.PlayerSettingsController.ContinuousTurn = true;
        else GameManager.PlayerSettingsController.ContinuousTurn = false;
    }

    private void SetTeleportDash(bool val)
    {
        settingsController.TeleportDash = val;
        settingsController.SetSettings();

        if(val)
            text_teleportDash.text = "Dash";
        else
            text_teleportDash.text = "Piscar";
    }
    private void SetContinuousTurn(bool val)
    {
        settingsController.ContinuousTurn = val;
        settingsController.SetSettings();

        if (val)
            text_rotationContinuous.text = "Continuo";
        else
            text_rotationContinuous.text = "Estalo";
    }
    
}
