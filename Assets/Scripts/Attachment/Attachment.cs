using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    [SerializeField] Rigidbody rb;
    [SerializeField] OrientationChecker orientation;

    private Attachment parent;
    private List<Attachment> children = new List<Attachment>();

    public Collider[] Colliders { get => colliders; }
    public Attachment Parent { get => parent; }
    public bool HasProperOrientation { get => orientation.Check(null); }

    public bool Attached { get => parent != null; }
    public bool NotAttached { get => parent == null; }


    private void OnValidate()
    {
        if (colliders.Length == 0) colliders = GetComponentsInChildren<Collider>();

        if (rb == null) rb = GetComponent<Rigidbody>();

        if(orientation == null) orientation = GetComponent<OrientationChecker>();
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

    private void IgnoreCollisionsWith(Attachment other, bool ignore)
    {
        foreach (Collider m_collider in colliders)
        {
            foreach (Collider o_collider in parent.colliders)
            {
                Physics.IgnoreCollision(m_collider, o_collider, ignore);
            }
        }
    }


    public void DetachAllChildren()
    {
        foreach (Attachment child in children) child.Detach();
    }
}
