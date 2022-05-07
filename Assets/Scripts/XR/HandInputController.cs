using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInputController : MonoBehaviour
{
    [SerializeField] MovementTeleport teleport;
    [SerializeField] HandInputManager manager;

    [Header("")]
    [SerializeField] HandInteractor handInteractor;
    [SerializeField] GameObject handCursor;
    [SerializeField] FingerTip fingerTip;

    [Header("")]
    [SerializeField] XRInteractorLineVisual interactorLineVisual;
    [SerializeField] XRInteractorLineVisual otherInteractorLineVisual;
    [SerializeField] LineRenderer lineRenderer;

    public HandInteractor HandInteractor { get => handInteractor; }



    private void OnEnable()
    {
        manager.OnGripStart += GrabStart;
        manager.OnGripCancel += GrabCancel;

        manager.OnTriggerStart += TeleportStart;
        manager.OnTriggerCancel += TeleportCancel;

        manager.OnPointStart += PointStart;
        manager.OnPointCancel += PointCancel;

        manager.OnOpenStart += OpenStart;
        manager.OnOpenCancel += OpenCancel;

        manager.OnSecondaryStart += Pause;
    }
    private void OnDisable()
    {
        manager.OnGripStart -= GrabStart;
        manager.OnGripCancel -= GrabCancel;

        manager.OnTriggerStart -= TeleportStart;
        manager.OnTriggerCancel -= TeleportCancel;

        manager.OnPointStart -= PointStart;
        manager.OnPointCancel -= PointCancel;

        manager.OnOpenStart -= OpenStart;
        manager.OnOpenCancel -= OpenCancel;

        manager.OnSecondaryStart -= Pause;
    }


    private void GrabStart()
    {
        handInteractor.GrabClosest();
    }
    private void GrabCancel()
    {
        handInteractor.ReleaseGrabbed();
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

    private void Pause()
    {
        UIPopUp pauseMenu = GameManager.PauseMenu;

        if (pauseMenu.isUp) pauseMenu.PopOff();
        else pauseMenu.PopUp();
    }
}
