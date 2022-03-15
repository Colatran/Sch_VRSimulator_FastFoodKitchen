using System.Collections.Generic;
using UnityEngine;

public class HandGrabArea : MonoBehaviour
{
    [SerializeField] HandGrabArea otherHand;
    [SerializeField] Rigidbody rb;


    public List<Interactible> interactibles = new List<Interactible>();
    public Interactible closest;
    public Interactible grabbed;
    private bool grabing { get => grabbed != null; }
    private bool notGrabing { get => grabbed == null; }
    private float interactibleRadius = 0;


    private void OnTriggerEnter(Collider other)
    {
        Interactible interactible = other.GetComponent<Interactible>();
        if (interactible == null) return;

        if (interactibles.Contains(interactible)) return;

        interactibles.Add(interactible);
    }

    private void OnTriggerExit(Collider other)
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
        if (closest == null || grabing) return;

        grabbed = closest;
        closest = null; 

        if (otherHand.grabbed == grabbed) otherHand.ReleaseGrabbedWork();

        interactibleRadius = grabbed.InteractibleRadius;
        grabbed.Interact(transform.parent.gameObject, true);
        grabbed.RemoveHilight();

        TryListeningOnPickupEvents();
    }
    public void ReleaseGrabbed()
    {
        if (notGrabing) return;

        StopListeningOnPickupEvents();

        grabbed.Interact(transform.parent.gameObject, false);

        ReleaseGrabbedWork();
    }

    private void ReleaseGrabbedWork()
    {
        grabbed = null;
    }



    private Attachment attachmentListeningOnDetach;
    private void TryListeningOnPickupEvents()
    {
        if (grabbed is Interactible_Pickup)
        {
            rb.mass = 0.0001f;
            attachmentListeningOnDetach = (grabbed as Interactible_Pickup).Attachment;
            attachmentListeningOnDetach.OnDetach += OnPickupDetach;
        }
    }
    private void StopListeningOnPickupEvents()
    {
        rb.mass = 100;

        if (attachmentListeningOnDetach == null) return;

        attachmentListeningOnDetach.OnDetach -= OnPickupDetach;
        attachmentListeningOnDetach = null;
    }
    private void OnPickupDetach()
    {
        Interactible interactible = attachmentListeningOnDetach.GetComponent<Interactible>();
        interactibles.Remove(interactible);

        StopListeningOnPickupEvents();

        ReleaseGrabbedWork();
    }
}
