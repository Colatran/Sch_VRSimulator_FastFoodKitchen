using UnityEngine;

[CreateAssetMenu(fileName = "XRSettingsData", menuName = "NewObject/XRSettingsData")]
public class XRSettingsData : ScriptableObject
{
    public delegate void Action();
    public event Action OnChanged;

    private bool continuousTurn = true;
    private bool teleportDash = true;

    public bool ContinuousTurn
    {
        get => continuousTurn;
        set
        {
            continuousTurn = value;
            if (OnChanged != null) OnChanged();
        }
    }
    public bool TeleportDash
    {
        get => teleportDash;
        set
        {
            teleportDash = value;
            if (OnChanged != null) OnChanged();
        }
    }
}
