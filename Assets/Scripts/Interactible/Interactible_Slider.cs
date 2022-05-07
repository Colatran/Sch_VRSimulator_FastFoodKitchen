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


    public override void Grab(HandInteractor sender)
    {
        targetTransform = sender.transform;
        offset = targetTransform.position - transform.position;
    }
    public override void Release(HandInteractor sender)
    {
        targetTransform = null;
    }


    private void FixedUpdate()
    {
        FallowTargetPosition();
    }

    private void FallowTargetPosition()
    {
        if (targetTransform == null) return;

        Vector3 velocity = (targetTransform.position - transform.position - offset) / Time.fixedDeltaTime;

        rb.velocity = velocity;
    }
}
