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



    private Attachment directParent;
    private Attachment endParent;
    private List<Attachment> directChildren = new List<Attachment>();
    private List<Attachment> endChildren;


    public bool HasProperOrientation(Transform other) => orientation.Check(other);
    public bool IsContainer { get => isContainer; }
    public Attachment DirectParent { get => directParent; }
    public Attachment EndParent { get => endParent; }
    public bool IsAttached { get => endParent != null; }
    public bool IsNotAttached { get => endParent == null; }
    public bool IsAttachable { get => orientation.Check(null) && (IsAttached || isContainer); }
    public bool IsNotAttachable { get => !IsAttachable; }


    private void Awake()
    {
        if (isContainer) endChildren = new List<Attachment>();
    }



    public void AttachTo(Attachment newParent, bool dealWithRigidbody)
    {
        if (dealWithRigidbody) DisableRigidbody();

        SetDirectParent(newParent);
        transform.parent = directParent.transform;

        if (directParent.IsAttached) SetEndParent(directParent.endParent);
        else SetEndParent(directParent);
        
        SetChildrensEndParent(endParent);
    }

    public void Detach(bool dealWithRigidbody)
    {
        if (dealWithRigidbody) EnableRigidbody();

        SetDirectParent(null);
        transform.parent = null;

        if (isContainer) SetChildrensEndParent(this);
        else DetachAllChildren();

        SetEndParent(null);
    }

    public void SwitchParent(Attachment newParent) 
    {
        Detach(false);
        AttachTo(newParent, false);
    }

    public void DetachAllChildren()
    {
        while (directChildren.Count > 0)
        {
            directChildren[0].Detach(true);
        }
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

    public void SetChildrensEndParent(Attachment eParent)
    {
        foreach (Attachment child in directChildren)
        {
            child.SetEndParent(eParent);

            child.SetChildrensEndParent(eParent);
        }
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
}
