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
    [SerializeField] FingerTip fingerTip;
    [SerializeField] GameObject handCursor;

    public HandGrabArea HandGrabArea { get => handGrabArea; }

    private float selectActionValue;
    public float SelectActionValue { get => selectActionValue; }

    private float activateActionValue;
    public float ActivateActionValue { get => activateActionValue; }

    private bool selectActionPressing = false;
    private bool activateActionPressing = false;
    private bool notPointing = false;
    private bool openHand = false;


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


        if(notPointing)
        {
            if (selectAction && !activateAction) 
            {
                notPointing = false;
                Point();
            }
        }
        else
        {
            if (!selectAction || activateAction)
            {
                notPointing = true;
                NotPoint();
            }
        }

        if(openHand)
        {
            if (selectActionPressing || activateActionPressing)
            {
                openHand = false;
                OnCloseHand();
            }
        }
        else
        {
            if (!(selectActionPressing || activateActionPressing))
            {
                openHand = true;
                OnOpenHand();
            }
        }
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

    private void Point()
    {
        fingerTip.Pressing = true;
    }
    private void NotPoint()
    {
        fingerTip.Pressing = false;
    }

    private void OnOpenHand()
    {
        handCursor.SetActive(true);
    }
    private void OnCloseHand()
    {
        handCursor.SetActive(false);
    }
}
