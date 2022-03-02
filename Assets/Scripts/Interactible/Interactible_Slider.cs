using UnityEngine;

public class Interactible_Slider : Interactible
{
    [SerializeField] Rigidbody rb;

    private Transform targetTransform;
    private Vector3 offset;


    protected override void OnValidate()
    {
        base.OnValidate();

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public override void Interact(GameObject gObject)
    {
        Transform objTransform = gObject.transform;

        if (targetTransform == objTransform)
        {
            targetTransform = null;
        }
        else
        {
            targetTransform = objTransform;
            offset = targetTransform.position - transform.position;
        }
    }


    private void FixedUpdate()
    {
        FallowTarget();
    }

    private void FallowTarget()
    {
        if (targetTransform == null) return;

        Vector3 velocity = (targetTransform.position - transform.position - offset) / Time.fixedDeltaTime;

        rb.velocity = velocity;
    }


}