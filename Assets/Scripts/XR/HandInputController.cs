using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInputController : MonoBehaviour
{
    [SerializeField] MovementTeleport teleport;
    [SerializeField] HandInputManager manager;

    [Header("")]
    [SerializeField] HandGrabArea handGrabArea;
    public HandGrabArea HandGrabArea { get => handGrabArea; }
    [SerializeField] GameObject handCursor;
    [SerializeField] FingerTip fingerTip;

    [Header("")]
    [SerializeField] XRInteractorLineVisual interactorLineVisual;
    [SerializeField] XRInteractorLineVisual otherInteractorLineVisual;
    [SerializeField] LineRenderer lineRenderer;




    private void Awake()
    {
        manager.OnGripStart += GrabStart;
        manager.OnGripCancel += GrabCancel;

        manager.OnTriggerStart += TeleportStart;
        manager.OnTriggerCancel += TeleportCancel;

        manager.OnPointStart += PointStart;
        manager.OnPointCancel += PointCancel;

        manager.OnOpenStart += OpenStart;
        manager.OnOpenCancel += OpenCancel;
    }


    private void GrabStart()
    {
        handGrabArea.GrabClosest();
    }
    private void GrabCancel()
    {
        handGrabArea.ReleaseGrabbed();
    }

    private void TeleportStart()
    {
        interactorLineVisual.enabled = true;
        otherInteractorLineVisual.enabled = false;
        teleport.StartTeleport(lineRenderer);
    }
    private void TeleportCancel()
    {
        otherInteractorLineVisual.enabled = true;
        teleport.EndTeleport();
    }

    private void PointStart()
    {
        fingerTip.Pressing = true;
    }
    private void PointCancel()
    {
        fingerTip.Pressing = false;
    }

    private void OpenStart()
    {
        handCursor.SetActive(true);
    }
    private void OpenCancel()
    {
        handCursor.SetActive(false);
    }
}
