using UnityEngine;

public class FingerTip : MonoBehaviour
{
    private bool pressing = false;
    public bool Pressing 
    { 
        get => pressing;
        set => pressing = value;
    }
}
