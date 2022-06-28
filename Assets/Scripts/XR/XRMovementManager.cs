using UnityEngine;

public class XRMovementManager : MonoBehaviour
{
    [SerializeField] MovementContinuous movementContinuous;
    [SerializeField] MovementTeleport movementTeleport;

    public void Lock()
    {
        movementContinuous.enabled = false;
        movementTeleport.enabled = false;
    }
    public void Unlock()
    {
        movementContinuous.enabled = true;
        movementTeleport.enabled = true;
    }
}
