using UnityEngine;

public class PhysicsHandController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform target;
    [SerializeField] Renderer targetRenderer;


    private void FixedUpdate()
    {
        FollowHands();
    }

    private void FollowHands()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;


        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }


    private void Update()
    {
        SetNonPhysicalHandsVisibility();
    }

    private void SetNonPhysicalHandsVisibility()
    {
        if (Vector3.Distance(transform.position, target.position) > .1f ||
            Quaternion.Angle(transform.rotation, target.rotation) > 90)
            targetRenderer.enabled = true;
        else
            targetRenderer.enabled = false;
    }
}
