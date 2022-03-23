using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractableToInteractibleConverter : MonoBehaviour
{
    [SerializeField] XRBaseInteractable XRinteractable;
    [SerializeField] Interactible interactible;

    public void SelectEnter()
    {
        interactible.Interact(
            XRinteractable.firstInteractorSelecting.transform.GetComponent<HandInputController>()
            .HandGrabArea.transform.parent.gameObject, true);
    }
    public void SelectExit()
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
