using UnityEngine;

public class UIPopUpResponsiveness : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = GameManager.PlayerMainCameraTransform;
    }





    [SerializeField] bool responsiveToPosition = false;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToPosition))]
    [SerializeField] SerializableLayerMask layerMask;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToPosition))]
    [SerializeField] float initialDistance = .75f;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToPosition))]
    [SerializeField] float hitDistanceOffset = .1f;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToPosition))]
    [SerializeField] Vector3 offset;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToPosition))]
    [SerializeField] float hight;

    private void SetInCameraPosition()
    {
        Vector3 direction = target.forward;
        Vector3 position = target.position;
        float distance = initialDistance;

        Ray ray = new Ray();
        RaycastHit hitInfo;

        ray.direction = direction;
        ray.origin = position;
        if (Physics.Raycast(ray, out hitInfo, .75f + hitDistanceOffset, layerMask.value))
        {
            distance = Vector3.Distance(position, hitInfo.point) - hitDistanceOffset;
        }

        Vector3 requestedPosition = position + direction.normalized * distance + offset;
        Vector3 finalPosition = requestedPosition;

        ray.direction = Vector3.down;
        ray.origin = requestedPosition + Vector3.up * hight;
        if (Physics.Raycast(ray, out hitInfo, hight, layerMask.value))
        {
            finalPosition = hitInfo.point;
        }

        transform.position = finalPosition;
    }





    [Header("")]
    [SerializeField] bool responsiveToAngle = false;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToAngle))]
    [SerializeField] float maxAngle = 90;

    private bool canPopOfFromAngle = false;
    private bool IsOutsideAngle()
    {
        Vector3 directionOfTarget = target.forward;
        Vector3 directionToTarget = transform.position - target.position;

        float angle = Vector3.Angle(directionOfTarget, directionToTarget);
        return angle > maxAngle;
    }
    private void AngleResponsiveness()
    {
        if (responsiveToAngle)
        {
            if (IsOutsideAngle())
            {
                if (canPopOfFromAngle) CallOnShouldPopOff();
            }
            else canPopOfFromAngle = true;
        }
    }





    [Header("")]
    [SerializeField] bool responsiveToDistance = false;
    [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(responsiveToDistance))]
    [SerializeField] float maxDistance = 5;

    private bool canPopOfFromDistance = false;
    private bool IsOutsideDistance()
    {
        return Vector3.Distance(transform.position, target.position) > maxDistance;
    }
    private void DistanceResponsiveness()
    {
        if (responsiveToDistance)
        {
            if (IsOutsideDistance())
            {
                if (canPopOfFromDistance)
                    CallOnShouldPopOff();
            }
            else canPopOfFromDistance = true;
        }
    }





    public delegate void Action();
    public event Action OnShouldPopOff;
    private void CallOnShouldPopOff()
    {
        if (OnShouldPopOff == null) return;
        OnShouldPopOff();
    }

    private bool up = false;

    public void PopUp()
    {
        up = true;

        if (responsiveToPosition)
            SetInCameraPosition();
    }
    public void PopOff()
    {
        up = false;

        canPopOfFromAngle = false;
        canPopOfFromDistance = false;
    }





    private void Update()
    {
        if (up)
        {
            AngleResponsiveness();

            DistanceResponsiveness();
        }
    }
}
