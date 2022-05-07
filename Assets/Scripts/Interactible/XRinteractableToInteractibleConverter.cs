using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractableToInteractibleConverter : MonoBehaviour
{
    [SerializeField] XRBaseInteractable XRinteractable;
    [SerializeField] Interactible interactible;

    public void SelectEnter()
    {
        interactible.Grab(
            XRinteractable.firstInteractorSelecting.transform
            .GetComponent<HandInputController>().HandInteractor);
    }
    public void SelectExit()
    {
        interactible.Release(
            XRinteractable.firstInteractorSelecting.transform
            .GetComponent<HandInputController>().HandInteractor);
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
