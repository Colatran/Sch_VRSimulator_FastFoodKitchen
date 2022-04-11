using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInputManager : MonoBehaviour
{
    [SerializeField] ActionBasedController controller;


    public delegate void Action();
    public event Action OnSecondaryStart;
    public event Action OnSecondaryCancel;

    public event Action OnGripStart;
    public event Action OnGripCancel;
    public event Action OnTriggerStart;
    public event Action OnTriggerCancel;
    public event Action OnPointStart;
    public event Action OnPointCancel;
    public event Action OnPinchStart;
    public event Action OnPinchCancel;
    public event Action OnFistStart;
    public event Action OnFistCancel;
    public event Action OnOpenStart;
    public event Action OnOpenCancel;

    private void Call(Action On)
    {
        if (On == null) return;
        On();
    }

    private bool grip = false;
    private bool trigger = false;

    private bool isPoint { get => !trigger && grip; }
    private bool point = false;

    private bool isPinch { get => trigger && !grip; }
    private bool pinch = false;

    private bool isFist { get => trigger && grip; }
    private bool fist = false;

    private bool isOpen { get => !trigger && !grip; }
    private bool open = true;



    private void OnEnable()
    {
        controller.selectAction.action.started += SelectStart;
        controller.selectAction.action.canceled += SelectCancel;

        controller.activateAction.action.started += ActivateStart;
        controller.activateAction.action.canceled += ActivateCancel;

        //controller.secondaryAction.action.started += SecondaryStart;
        //controller.secondaryAction.action.canceled += SecondaryCancel;
    }
    private void OnDisable()
    {
        controller.selectAction.action.started -= SelectStart;
        controller.selectAction.action.canceled -= SelectCancel;

        controller.activateAction.action.started -= ActivateStart;
        controller.activateAction.action.canceled -= ActivateCancel;

        //controller.secondaryAction.action.started -= SecondaryStart;
        //controller.secondaryAction.action.canceled -= SecondaryCancel;
    }


    private void SelectStart(InputAction.CallbackContext obj)
    {
        grip = true;
        Call(OnGripStart);

        UncheckPinch();
        UncheckOpen();

        if (CheckPoint())
            CheckFist();
    }
    private void SelectCancel(InputAction.CallbackContext obj)
    {
        grip = false;
        Call(OnGripCancel);

        if (UncheckPoint())
            UncheckFist();

        if(CheckPinch())
            CheckOpen();
    }

    private void ActivateStart(InputAction.CallbackContext obj)
    {
        trigger = true;
        Call(OnTriggerStart);

        UncheckPoint();
        UncheckOpen();

        if (CheckPinch())
            CheckFist();
    }
    private void ActivateCancel(InputAction.CallbackContext obj)
    {
        trigger = false;
        Call(OnTriggerCancel);

        if (UncheckPinch())
            UncheckFist();

        if(CheckPoint())
            CheckOpen();
    }

    private void SecondaryStart(InputAction.CallbackContext obj)
    {
        Call(OnSecondaryStart);
    }
    private void SecondaryCancel(InputAction.CallbackContext obj)
    {
        Call(OnSecondaryCancel);
    }



    private bool CheckPoint()
    {
        if (point) return true;
        if (isPoint)
        {
            point = true;
            Call(OnPointStart);
            return false;
        }
        return true;
    }
    private bool UncheckPoint()
    {
        if (!point) return true;
        if (!isPoint)
        {
            point = false;
            Call(OnPointCancel);
            return false;
        }
        return true;
    }

    private bool CheckPinch()
    {
        if (pinch) return true;
        if (isPinch)
        {
            pinch = true;
            Call(OnPinchStart);
            return false;
        }
        return true;
    }
    private bool UncheckPinch()
    {
        if (!pinch) return true;
        if (!isPinch)
        {
            pinch = false;
            Call(OnPinchCancel);
            return false;
        }
        return true;
    }

    private bool CheckFist()
    {
        if (fist) return true;
        if (isFist)
        {
            fist = true;
            Call(OnFistStart);
            return false;
        }
        return true;
    }
    private bool UncheckFist()
    {
        if (!fist) return true;
        if (!isFist)
        {
            fist = false;
            Call(OnFistCancel);
            return false;
        }
        return true;
    }

    private bool CheckOpen()
    {
        if (open) return true;
        if (isOpen)
        {
            open = true;
            Call(OnOpenStart);
            return false;
        }
        return true;
    }
    private bool UncheckOpen()
    {
        if (!open) return true;
        if (!isOpen)
        {
            open = false;
            Call(OnOpenCancel);
            return false;
        }
        return true;
    }





    private float selectActionValue;
    public float SelectActionValue { get => selectActionValue; }

    private float activateActionValue;
    public float ActivateActionValue { get => activateActionValue; }

    private void Update()
    {
        selectActionValue = controller.selectActionValue.action.ReadValue<float>();
        activateActionValue = controller.activateActionValue.action.ReadValue<float>();
    }
}
