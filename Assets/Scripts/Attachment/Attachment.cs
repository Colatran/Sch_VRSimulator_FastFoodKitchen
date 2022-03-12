using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] OrientationChecker orientation;
    [SerializeField] bool isContainer;
    //[SerializeField] Collider[] colliders;

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (orientation == null) orientation = GetComponent<OrientationChecker>();
        /*if (colliders.Length == 0) 
        {
            List<Collider> all = new List<Collider>();
            List<Collider> final = new List<Collider>();
            all.AddRange(GetComponentsInChildren<Collider>());

            foreach (Collider col in all)
                if (col.gameObject.layer == 2)
                    final.Add(col);

            colliders = final.ToArray();
        }*/
    }



    public Attachment directParent;
    public Attachment endParent;
    public List<Attachment> directChildren = new List<Attachment>();
    public List<Attachment> endChildren;

    public bool HasProperOrientation(Transform other) => orientation.Check(other);
    public bool IsContainer { get => isContainer; }
    public Attachment DirectParent { get => directParent; }
    public Attachment EndParent { get => endParent; }
    public bool IsAttached { get => endParent != null; }
    public bool IsNotAttached { get => endParent == null; }
    public bool IsAttachable { get => orientation.Check(null) && (IsAttached || isContainer); }
    public bool IsNotAttachable { get => !IsAttachable; }

    private delegate void DetachAction();
    private event DetachAction OnDetach;
    private List<Attachment> colectiveParents = new List<Attachment>();


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
            endParent.endChildren.Remove(this);

        endParent = parent;

        if (endParent == null) return;
        endParent.endChildren.Add(this);
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

    }
    private void ClearParenting()
    {
        SetDirectParent(null);
        transform.parent = null;

        if (isContainer) SetChildrensEndParent(this);
        else DetachAllChildren();

        SetEndParent(null);
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

    public void Attach(Attachment parent)
    {
        DisableRigidbody();

        SetParenting(parent);
    }
    public void Detach()
    {
        EnableRigidbody();

        ClearParenting();

        if (OnDetach == null) return;
        OnDetach();
    }

    public void SwitchParent(Attachment parent)
    {
        ClearParenting();

        if (OnDetach != null)
            OnDetach();

        SetParenting(parent);
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
        parent.OnDetach += DetachEvent;
    }
    public void DetachEvent()
    {
        Detach();

        foreach (Attachment parent in colectiveParents)
            parent.OnDetach -= DetachEvent;

        colectiveParents.Clear();
    }
}
