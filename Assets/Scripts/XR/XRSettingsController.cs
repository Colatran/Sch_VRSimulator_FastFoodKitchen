using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSettingsController : MonoBehaviour
{
    [SerializeField] XRSettingsData xRSettingsData;
    [SerializeField] ContinuousTurnProviderBase ContinuousTurnProvider;
    [SerializeField] SnapTurnProviderBase SnapTurnProvider;
    [SerializeField] MovementTeleport MovementTeleport;

    private void OnEnable()
    {
        xRSettingsData.OnChanged += SetSettings;
        SetSettings();
    }
    private void OnDisable()
    {
        xRSettingsData.OnChanged -= SetSettings;
    }

    public void SetSettings()
    {
        //ContinuousTurn
        bool ContinuousTurn = xRSettingsData.ContinuousTurn;
        ContinuousTurnProvider.enabled = ContinuousTurn;
        SnapTurnProvider.enabled = !ContinuousTurn;

        //Teleport
        bool TeleportDash = xRSettingsData.TeleportDash;
        MovementTeleport.dash = TeleportDash;
    }
}
