using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRinteractableToInteractibleConverter : MonoBehaviour
{
    [SerializeField] XRBaseInteractable XRinteractable;
    [SerializeField] Interactible_Pickup interactible;

    public void InteractGrab()
    {
        interactible.Interact(
            XRinteractable.firstInteractorSelecting.transform.GetComponent<HandInputController>()
            .HandGrabArea.transform.parent.gameObject, true);
    }
    public void InteractRelease()
    {
        interactible.Interact(
            XRinteractable.firstInteractorSelecting.transform.GetComponent<HandInputController>()
            .HandGrabArea.transform.parent.gameObject, false);
    }

    public void HoverEnter()
    {
        interactible.AddHilight();
    }
    public void HoverExit()
    {
        interactible.RemoveHilight();
    }
}
