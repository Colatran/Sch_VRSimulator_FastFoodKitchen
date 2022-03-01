using System.Collections.Generic;
using UnityEngine;

public class HandGrabArea : MonoBehaviour
{
    public List<Interactible> interactibles = new List<Interactible>();
    private Interactible closest;
    private bool grabing;

    private void OnTriggerEnter(Collider other)
    {
        Interactible interactible = other.GetComponent<Interactible>();
        if (interactible == null) return;

        interactibles.Add(interactible);
    }

    private void OnTriggerExit(Collider other)
    {
        Interactible interactible = other.GetComponent<Interactible>();
        if (interactible == null) return;

        interactibles.Remove(interactible);

        if (interactible == closest)
        {
            closest.RemoveHilight("2");
            closest = null;
        }
    }


    void Update()
    {
        FindClosest();
    }


    private void FindClosest()
    {
        if (grabing) return;
        if (interactibles.Count == 0) return;

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

        if (closest == null)
        {
            closest = _closest;
            closest.AddHilight("1");
            return;
        }

        if (closest == _closest) return;

        closest.RemoveHilight("1");
        closest = _closest;
        closest.AddHilight("2");
    }


    public void GrabClosest()
    {
        if (closest == null) return;

        closest.Interact(transform.parent.gameObject);

        grabing = true;
    }

    public void ReleaseClosest()
    {
        if (!grabing || closest == null) return;

        closest.Interact(transform.parent.gameObject);
        interactibles.Remove(closest);

        grabing = false;
    }
}
