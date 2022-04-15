using UnityEngine;

public class SaltCanister : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] OrientationChecker orientationChecker;
    [SerializeField] GameObjectPool pool;
    [SerializeField] Transform origin;
    [SerializeField] float targetYVelocity = 0;
    [SerializeField] float minYVelocity = 0;
    [SerializeField] Collider colliderInteractible;
    [SerializeField] Collider colliderIgnoreRaycast;

    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponent<Attachment>();
        if (orientationChecker == null) orientationChecker = GetComponent<OrientationChecker>();
        if (pool == null) pool = GetComponent<GameObjectPool>();
    }



    private Rigidbody rb;
    private bool goodVelocity = false;
    private Vector3 lastVelocity = Vector3.zero;
    private bool PointingDown { get => orientationChecker.Check(null); }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    private void OnEnable()
    {
        attachment.OnAttach += OnAttach;
        attachment.OnDetach += OnDetach;
    }
    private void OnDisable()
    {
        attachment.OnAttach -= OnAttach;
        attachment.OnDetach -= OnDetach;
    }

    private void OnDetach()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnAttach()
    {
        rb = attachment.EndParent.GetComponent<Rigidbody>();
    }



    void Update()
    {
        GetSwing();
    }

    private void GetSwing()
    {
        if (rb == null) return;

        if (rb.velocity.y < targetYVelocity)
        {
            goodVelocity = true;
            lastVelocity = rb.velocity;
        }
        else if (goodVelocity && rb.velocity.y > minYVelocity)
        {
            goodVelocity = false;

            if (PointingDown)
            {
                ThrowSalt();
            }
        }
    }



    private void ThrowSalt()
    {
        GameObject pojectile = pool.GetObject();
        pojectile.transform.position = origin.position;
        pojectile.SetActive(true);

        pojectile.GetComponent<Rigidbody>().velocity = lastVelocity;
    }

    public Collider ColliderInteractible { get => colliderInteractible; }
    public Collider ColliderIgnoreRaycast { get => colliderIgnoreRaycast; }
}
