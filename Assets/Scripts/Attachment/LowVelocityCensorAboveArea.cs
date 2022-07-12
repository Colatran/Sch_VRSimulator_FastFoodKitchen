using UnityEngine;
using System.Collections.Generic;

public class LowVelocityCensorAboveArea : MonoBehaviour
{
    public List<Rigidbody> bodies = new List<Rigidbody>();
    public bool Contains(Rigidbody rb) => bodies.Contains(rb);


    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        bodies.Add(rb);
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null)
        {
            bodies.RemoveAll(x => x == null);
            return;
        }

        bodies.Remove(rb);
    }
}
