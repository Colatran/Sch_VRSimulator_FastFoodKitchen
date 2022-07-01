using System.Collections.Generic;
using UnityEngine;


public class Attachment : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [ReadOnly, SerializeField] float rb_mass;
    [ReadOnly, SerializeField] float rb_drag;
    [ReadOnly, SerializeField] float rb_angdrag;
    [ReadOnly, SerializeField] bool rb_isKinematic;
    [ReadOnly, SerializeField] bool rb_useGravity;
    [ReadOnly, SerializeField] CollisionDetectionMode rb_collisionDetectionMode;
    [ReadOnly, SerializeField] RigidbodyInterpolation rb_interpolation;
    [SerializeField] OrientationChecker orientation;
    [SerializeField] bool isContainer;
    [SerializeField] bool isDeadEnd;

    private void OnValidate()
    {
        if (rb == null) 
        {
            rb = GetComponent<Rigidbody>();
            rb_mass = rb.mass;
            rb_drag = rb.drag;
            rb_angdrag = rb.angularDrag;
            rb_isKinematic = rb.isKinematic;
            rb_useGravity = rb.useGravity;
            rb_collisionDetectionMode = rb.collisionDetectionMode;
            rb_interpolation = rb.interpolation;
        }
        if (orientation == null) orientation = GetComponent<OrientationChecker>();
    }

    private void OnDestroy()
    {
        if (directParent != null) directParent.directChildren.Remove(this);
        if (endParent != null) endParent.endChildren.Remove(this);

        foreach (Attachment child in colectiveChildren)
            OnDetach -= child.DetachFromColectiveParent;

        if (OnDetach != null)
            OnDetach();
    }



    private Attachment directParent;
    private Attachment endParent;
    private List<Attachment> directChildren = new List<Attachment>();
    private List<Attachment> endChildren;
    private List<Attachment> colectiveParents = new List<Attachment>();
    private List<Attachment> colectiveChildren = new List<Attachment>();

    public delegate void EmptyAction();
    public event EmptyAction OnAttach;
    public event EmptyAction OnDetach;
    public delegate void AttachmentAction(Attachment attachment);
    public event AttachmentAction OnAddContent;
    public event AttachmentAction OnRemoveContent;

    public float Mass { get => rb_mass; }
    public bool HasProperOrientation(Transform other) => orientation.Check(other);
    public bool IsContainer { get => isContainer; }
    public Attachment DirectParent { get => directParent; }
    public Attachment EndParent { get => endParent; }
    public bool IsAttached { get => endParent != null; }
    public bool IsNotAttached { get => endParent == null; }
    public bool IsAttachable { get => orientation.Check(null) && (IsAttached || isContainer); }
    public bool IsNotAttachable { get => !IsAttachable; }
    public List<Attachment> DirectChildren { get => directChildren; }
    public List<Attachment> EndChildren { get => endChildren; }


    private void Awake()
    {
        if (isContainer) endChildren = new List<Attachment>();
    }



    private void SetDirectParent(Attachment parent)
    {
        if (directParent != null)
            directParent.directChildren.Remove(this);

        directParent = parent;

        if (directParent == null) return;
        directParent.directChildren.Add(this);
    }
    private void SetEndParent(Attachment parent)
    {
        if (endParent != null)
        {
            endParent.endChildren.Remove(this);

            if (endParent.OnRemoveContent != null)
                endParent.OnRemoveContent(this);
        }

        endParent = parent;

        if (endParent == null) return;
        endParent.endChildren.Add(this);

        if (endParent.OnAddContent != null)
            endParent.OnAddContent(this);
    }
    private void SetChildrensEndParent(Attachment parent)
    {
        foreach (Attachment child in directChildren)
        {
            child.SetEndParent(parent);

            child.SetChildrensEndParent(parent);
        }
    }

    private void SetParenting(Attachment parent)
    {
        SetDirectParent(parent);
        transform.parent = directParent.transform;

        if (directParent.IsAttached) SetEndParent(directParent.endParent);
        else SetEndParent(directParent);

        if (!isDeadEnd) SetChildrensEndParent(endParent);

        if (OnAttach == null) return;
        OnAttach();
    }
    private void ClearParenting()
    {
        SetDirectParent(null);
        transform.parent = null;

        if (isContainer) SetChildrensEndParent(this);
        else DetachAllChildren();

        SetEndParent(null);

        if (OnDetach == null) return;
        OnDetach();
    }

    public void DisableRigidbody()
    {
        if (rb == null) return;

        Destroy(rb);
    }
    public void EnableRigidbody()
    {
        if (rb != null) return;

        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.mass = rb_mass;
        rb.drag = rb_drag;
        rb.angularDrag = rb_angdrag;
        rb.isKinematic = rb_isKinematic;
        rb.useGravity = rb_useGravity;
        rb.collisionDetectionMode = rb_collisionDetectionMode;
        rb.interpolation = rb_interpolation;
    }

    public void Attach(Attachment parent)
    {
        if (directChildren.Contains(parent)) return;

        if (IsAttached) 
        {
            ClearParenting();
        }

        DisableRigidbody();

        SetParenting(parent);
    }
    public void Detach()
    {
        EnableRigidbody();

        ClearParenting();
    }

    public void DetachAllChildren()
    {
        while (directChildren.Count > 0)
        {
            directChildren[0].Detach();
        }
    }


    public void AddColectiveParent(Attachment parent)
    {
        if (colectiveParents.Contains(parent)) return;
        colectiveParents.Add(parent);
        parent.OnDetach += DetachFromColectiveParent;
        parent.colectiveChildren.Add(this);
    }
    public void DetachFromColectiveParent()
    {
        Detach();

        foreach (Attachment parent in colectiveParents)
        {
            if (parent == null) continue;

            parent.colectiveChildren.Remove(this);
            parent.OnDetach -= DetachFromColectiveParent;
        }

        colectiveParents.Clear();
    }
}
