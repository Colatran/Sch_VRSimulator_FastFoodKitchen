using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [HideInInspector] [SerializeField] float rb_mass;
    [HideInInspector] [SerializeField] float rb_drag;
    [HideInInspector] [SerializeField] float rb_angdrag;
    [SerializeField] OrientationChecker orientation;
    [SerializeField] bool isContainer;

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (orientation == null) orientation = GetComponent<OrientationChecker>();
    }



    private Attachment directParent;
    private Attachment endParent;
    private List<Attachment> directChildren = new List<Attachment>();
    private List<Attachment> endChildren;
    private List<Attachment> colectiveParents = new List<Attachment>();

    public delegate void ParentAction(Attachment child);
    public event ParentAction OnAddContent;
    public event ParentAction OnRemoveContent;
    public delegate void EmptyAction();
    public event EmptyAction OnAttach;
    public event EmptyAction OnDetach;

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

        SetChildrensEndParent(endParent);

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

    private void DisableRigidbody()
    {
        if (rb == null) return;

        Destroy(rb);
    }
    private void EnableRigidbody()
    {
        if (rb != null) return;

        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.mass = rb_mass;
        rb.drag = rb_drag;
        rb.angularDrag = rb_angdrag;

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
        parent.OnDetach += DetachFromColectiveParentEvent;
    }
    public void DetachFromColectiveParentEvent()
    {
        Detach();

        foreach (Attachment parent in colectiveParents)
            parent.OnDetach -= DetachFromColectiveParentEvent;

        colectiveParents.Clear();
    }
}
