using System.Collections.Generic;
using UnityEngine;

public class HandGrabArea : MonoBehaviour
{
    [SerializeField] HandGrabArea otherHand;

    private List<Interactible> interactibles = new List<Interactible>();
    private Interactible closest;
    private bool grabing;

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
        if (Vector3.Distance(closest.transform.position, transform.position) > .2f) ReleaseClosest();
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
        if (closest == null) return;

        if (otherHand.grabing && otherHand.closest == closest) otherHand.SwitchRelease();

        closest.Interact(transform.parent.gameObject);
        closest.RemoveHilight();
        grabing = true;
    }
    public void ReleaseClosest()
    {
        if (!grabing || closest == null) return;

        closest.Interact(transform.parent.gameObject);
        closest.AddHilight();
        grabing = false;
    }
    public void SwitchRelease()
    {
        closest.AddHilight();
        grabing = false;
    }
}
