using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Unity.XR.CoreUtils;

public class ContinuousMovement : ContinuousMoveProviderBase
{
    [SerializeField]
    [Tooltip("The Input System Action that will be used to read Move data from the left hand controller. Must be a Value Vector2 Control.")]
    InputActionProperty m_LeftHandMoveAction;
    /// <summary>
    /// The Input System Action that Unity uses to read Move data from the left hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
    /// </summary>
    public InputActionProperty leftHandMoveAction
    {
        get => m_LeftHandMoveAction;
        set => SetInputActionProperty(ref m_LeftHandMoveAction, value);
    }

    [SerializeField]
    [Tooltip("The Input System Action that will be used to read Move data from the right hand controller. Must be a Value Vector2 Control.")]
    InputActionProperty m_RightHandMoveAction;
    /// <summary>
    /// The Input System Action that Unity uses to read Move data from the right hand controller. Must be a <see cref="InputActionType.Value"/> <see cref="Vector2Control"/> Control.
    /// </summary>
    public InputActionProperty rightHandMoveAction
    {
        get => m_RightHandMoveAction;
        set => SetInputActionProperty(ref m_RightHandMoveAction, value);
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnEnable()
    {
        m_LeftHandMoveAction.EnableDirectAction();
        m_RightHandMoveAction.EnableDirectAction();
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnDisable()
    {
        m_LeftHandMoveAction.DisableDirectAction();
        m_RightHandMoveAction.DisableDirectAction();
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }


    [SerializeField] CharacterController character;
    [SerializeField] float additionalHeight = .02f;
    [SerializeField] XROrigin origin;

    /// <inheritdoc />
    protected override Vector2 ReadInput()
    {
        //CapsuleFallowHeadset();
        CapsuleFallowHightHeadset();

        var handValue =
            m_LeftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero + 
            m_RightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

        Quaternion headYaw = Quaternion.Euler(0, origin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(handValue.x, 0, handValue.y);
        character.Move(direction * Time.fixedDeltaTime * moveSpeed);

        return Vector2.zero;
    }

    private void CapsuleFallowHeadset()
    {
        character.height = origin.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(origin.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
    private void CapsuleFallowHightHeadset()
    {
        character.height = origin.CameraInOriginSpaceHeight + additionalHeight;
        character.center = new Vector3(0, character.height / 2 + character.skinWidth, 0);
    }
}
