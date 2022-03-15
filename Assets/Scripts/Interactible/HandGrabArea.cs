using System.Collections.Generic;
using UnityEngine;

public class HandGrabArea : MonoBehaviour
{
    [SerializeField] HandGrabArea otherHand;
    [SerializeField] Rigidbody rb;


    public List<Interactible> interactibles = new List<Interactible>();
    public Interactible closest;
    private bool grabing;
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
        if (grabing) ReleaseIfTooFar();
        else FindClosest();
    }

    private void ReleaseIfTooFar()
    {
        if (
            interactibleRadius > 0 && 
            Vector3.Distance(closest.transform.position, transform.position) > interactibleRadius)
            ReleaseClosest();
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


    public void GrabClosest()
    {
        if (closest == null || grabing) return;

        if (otherHand.grabing && otherHand.closest == closest) otherHand.SwitchRelease();

        interactibleRadius = closest.InteractibleRadius;
        closest.Interact(transform.parent.gameObject, true);
        closest.RemoveHilight();

        grabing = true;


        if(closest is Interactible_Pickup)
        {
            rb.mass = 0.0001f;
            attachmentListeningOnDetach = (closest as Interactible_Pickup).Attachment;
            attachmentListeningOnDetach.OnDetach += OnPickupDetach;
        }
    }
    public void ReleaseClosest()
    {
        if (closest == null || !grabing) return;

        closest.AddHilight();
        closest.Interact(transform.parent.gameObject, false);
        
        grabing = false;
    }

    private void SwitchRelease()
    {
        closest.AddHilight();
        grabing = false;
    }



    private Attachment attachmentListeningOnDetach;

    private void OnPickupDetach()
    {
        rb.mass = 100;

        attachmentListeningOnDetach.OnDetach -= OnPickupDetach;

        Interactible interactible = attachmentListeningOnDetach.GetComponent<Interactible>();
        interactibles.Remove(interactible);
        interactible.RemoveHilight();

        closest = null;

        attachmentListeningOnDetach = null;
    }


}
