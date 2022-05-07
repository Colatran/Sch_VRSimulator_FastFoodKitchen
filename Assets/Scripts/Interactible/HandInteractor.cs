using System.Collections.Generic;
using UnityEngine;

public class HandInteractor : MonoBehaviour
{
    [SerializeField] HandInteractor otherHand;
    [SerializeField] Rigidbody rb;
    [SerializeField] TriggerArea grabArea;
    [SerializeField] Attachment attachment;

    public Attachment Attachment { get => attachment; }

    private List<Interactible> interactibles = new List<Interactible>();
    private Interactible closest;
    private Interactible grabbed;
    private bool grabing { get => grabbed != null; }
    private bool notGrabing { get => grabbed == null; }
    private float interactibleRadius = 0;



    private void OnEnable()
    {
        grabArea.OnEnter += OnEnterGrabArea;
        grabArea.OnExit += OnExitGrabArea;
    }
    private void OnDisable()
    {
        grabArea.OnEnter -= OnEnterGrabArea;
        grabArea.OnExit -= OnExitGrabArea;
    }

    private void OnEnterGrabArea(Collider other)
    {
        Interactible interactible = other.GetComponent<Interactible>();
        if (interactible == null) return;

        if (interactibles.Contains(interactible)) return;

        interactibles.Add(interactible);
    }
    private void OnExitGrabArea(Collider other)
    {
        Interactible interactible = other.GetComponent<Interactible>();
        if (interactible == null) return;

        interactibles.Remove(interactible);
    }



    void Update()
    {
        if (notGrabing) FindClosest();
        else ReleaseIfTooFar();
    }

    private void FindClosest()
    {
        if (interactibles.Count == 0)
        {
            if (closest == null) return;
            closest.RemoveHilight();
            closest = null;
            return;
        }

        Interactible _closest = null;
        float minDistance = 1f;

        foreach (var interactible in interactibles)
        {
            if (interactible == null)
            {
                interactibles.Remove(interactible);
                return;
            }

            float distance = Vector3.Distance(transform.position, interactible.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                _closest = interactible;
            }
        }

        if (_closest == closest) return;

        if (closest != null) closest.RemoveHilight();

        closest = _closest;
        closest.AddHilight();
    }

    private void ReleaseIfTooFar()
    {
        if (interactibleRadius > 0
            && Vector3.Distance(grabbed.transform.position, transform.position) > interactibleRadius)
            ReleaseGrabbed();
    }



    public void GrabClosest()
    {
        if (grabing || closest == null) return;

        grabbed = closest;
        GrabWork();
    }
    public void Grab(Interactible interactible)
    {
        if (grabing) return;

        grabbed = interactible;
        GrabWork();
    }
    private void GrabWork()
    {
        closest = null;

        if (otherHand.grabbed == grabbed) otherHand.ReleaseGrabbedWork();

        interactibleRadius = grabbed.InteractibleRadius;
        grabbed.Grab(this);
        grabbed.RemoveHilight();

        WorkCase_InteractiblePickup();
    }

    public void ReleaseGrabbed()
    {
        if (notGrabing) return;

        StopWorkCase_InteractiblePickup();

        grabbed.Release(this);

        ReleaseGrabbedWork();
    }
    private void ReleaseGrabbedWork()
    {
        grabbed = null;
    }



    private Attachment attachment_InteractiblePickup;
    private void WorkCase_InteractiblePickup()
    {
        if (grabbed is Interactible_Pickup)
        {
            //rb.mass = 1f;
            attachment_InteractiblePickup = (grabbed as Interactible_Pickup).Attachment;
            rb.mass = attachment_InteractiblePickup.Mass;
            attachment_InteractiblePickup.OnDetach += OnPickupDetach;
        }
    }
    private void StopWorkCase_InteractiblePickup()
    {
        rb.mass = 100;

        if (attachment_InteractiblePickup == null) return;

        attachment_InteractiblePickup.OnDetach -= OnPickupDetach;
        attachment_InteractiblePickup = null;
    }
    private void OnPickupDetach()
    {
        Interactible interactible = attachment_InteractiblePickup.GetComponent<Interactible>();
        interactibles.Remove(interactible);

        StopWorkCase_InteractiblePickup();

        ReleaseGrabbedWork();
    }
}
