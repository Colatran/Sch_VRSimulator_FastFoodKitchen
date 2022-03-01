using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSettingsController : MonoBehaviour
{
    [SerializeField] ContinuousTurnProviderBase ContinuousTurnProvider;
    [SerializeField] SnapTurnProviderBase SnapTurnProvider;


    public bool ContinuousTurn = true;
    public bool TeleportDash = true;



    public void SetSettings()
    {
        //ContinuousTurn
        ContinuousTurnProvider.enabled = ContinuousTurn;
        SnapTurnProvider.enabled = !ContinuousTurn;


    }


    private void Start()
    {
        SetSettings();
    }
}
