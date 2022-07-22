using UnityEngine;

public class HandPhysicsController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform target;
    [SerializeField] Renderer targetRenderer;


    private void FixedUpdate()
    {
        FollowHandPosition();
        FollowHandRotation();
    }

    private void FollowHandPosition()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
    }
    private void FollowHandRotation()
    {
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        if (angleInDegrees > 180f)
            angleInDegrees -= 360f;

        var angularVelocity = (angleInDegrees * rotationAxis * Mathf.Deg2Rad / Time.fixedDeltaTime);

        if (!float.IsNaN(angularVelocity.x))
            rb.angularVelocity = angularVelocity;
    }



    private void Update()
    {
        SetNonPhysicalHandsVisibility();
    }

    private void SetNonPhysicalHandsVisibility()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > .1f ||
            Quaternion.Angle(transform.rotation, target.rotation) > 90)
            targetRenderer.enabled = true;
        else
            targetRenderer.enabled = false;

        if (distance > .25f) transform.position = target.position;
    }
}
