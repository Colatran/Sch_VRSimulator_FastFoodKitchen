using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    public Collider[] Colliders { get => colliders; }
    [SerializeField] Rigidbody rb;

    private Attachment parent;
    public Attachment Parent { get => parent; }
    public bool Attached { get => parent != null; }
    public bool NotAttached { get => parent == null; }

    private List<Attachment> children = new List<Attachment>();



    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        if (colliders.Length == 0) colliders = GetComponents<Collider>();
        if (colliders.Length == 0) colliders = GetComponentsInChildren<Collider>();
    }


    public void AttachTo(Attachment newParent)
    {
        DisableRigidbody();

        parent = newParent;
        parent.children.Add(this);

        IgnoreCollisionsWith(parent, true);

        transform.parent = parent.transform;
    }
    public void Detach()
    {
        IgnoreCollisionsWith(parent, false);

        transform.parent = null;

        parent.children.Remove(this);
        parent = null;

        EnableRigidbody();
    }
    public void SwitchToParent(Attachment newParent) 
    {
        //Deal with the old parent
        IgnoreCollisionsWith(parent, false);
        parent.children.Remove(this);

        //Set new parent
        parent = newParent;
        parent.children.Add(this);
        IgnoreCollisionsWith(parent, true);
        transform.parent = parent.transform;

    }


    private void DisableRigidbody()
    {
        Destroy(rb);
    }
    private void EnableRigidbody()
    {
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void IgnoreCollisionsWith(Attachment parent, bool ignore)
    {
        foreach (Collider m_collider in colliders)
        {
            foreach (Collider p_ollider in parent.colliders)
            {
                Physics.IgnoreCollision(m_collider, p_ollider, ignore);
            }
        }
    }
}
