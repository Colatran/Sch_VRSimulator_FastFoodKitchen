using System.Collections.Generic;
using UnityEngine;

public class LowVelocityCensor : MonoBehaviour
{
    private const float minVelocity = .05f;

    [SerializeField] Attachment attachment;

    private List<Rigidbody> bodies = new List<Rigidbody>();



    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        bodies.Add(rb);
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        bodies.Remove(rb);
    }

    private void Update()
    {
        int bodieCount = bodies.Count;
        for (int i = 0; i < bodieCount;)
        {
            Rigidbody rb = bodies[i];
            if(rb == null)
            {
                bodies.RemoveAt(i);
                bodieCount--;
                continue;
            } 
            else  if (rb.velocity.magnitude < minVelocity && rb.angularVelocity.magnitude < .05f)
            {
                Item item = rb.GetComponent<Item>();
                if (item == null || !(item.Is(ItemType.BEEF) || item.Is(ItemType.FRIED)))
                {
                    bodies.RemoveAt(i);
                    bodieCount--;
                    continue;
                }

                Attachment attachment = item.Attachment;
                if (attachment.IsAttached)
                {
                    i++;
                    continue;
                }
                else
                {
                    attachment.Attach(this.attachment);

                    bodies.RemoveAt(i);
                    bodieCount--;
                    continue;
                }
            }

            i++;
        }
    }
}
