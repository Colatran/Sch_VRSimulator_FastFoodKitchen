using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInputController : MonoBehaviour
{
    [SerializeField] ActionBasedController controller;
    [SerializeField] HandGrabArea handGrabArea;
    [SerializeField] TeleportMovement teleportMovement;
    [SerializeField] XRInteractorLineVisual myInteractorLineVisual;
    [SerializeField] XRInteractorLineVisual otherInteractorLineVisual;
    [SerializeField] LineRenderer lineRenderer;

    private float selectActionValue;
    public float SelectActionValue { get => selectActionValue; }

    private float activateActionValue;
    public float ActivateActionValue { get => activateActionValue; }

    private bool selectActionPressing = false;
    private bool activateActionPressing = false;


    private void Update()
    {
        selectActionValue = controller.selectActionValue.action.ReadValue<float>();
        activateActionValue = controller.activateActionValue.action.ReadValue<float>();

        var selectAction = controller.selectAction.action.ReadValue<float>() == 1;
        if (selectActionPressing)
        {
            if (!selectAction) SelectRelease();
        }
        else
        {
            if(selectAction) SelectPress();
        }
        selectActionPressing = selectAction;


        var activateAction = controller.activateAction.action.ReadValue<float>() == 1;
        if (activateActionPressing)
        {
            if (!activateAction) ActivateRelease();
        }
        else
        {
            if (activateAction) ActivatePress();
        }
        activateActionPressing = activateAction;
    }


    private void SelectPress()
    {
        handGrabArea.GrabClosest();
    }
    private void SelectRelease()
    {
        handGrabArea.ReleaseGrabbed();
    }

    private void ActivatePress()
    {
        myInteractorLineVisual.enabled = true;
        otherInteractorLineVisual.enabled = false;
        teleportMovement.StartTeleport(lineRenderer);
    }
    private void ActivateRelease()
    {
        otherInteractorLineVisual.enabled = true;
        teleportMovement.EndTeleport();
    }
}
