using System.Collections.Generic;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    [SerializeField] Attachment attachment;

    private bool goodOrientation = false;
    private List<MagnetPoint> points;


    private void OnTriggerEnter(Collider other)
    {
        MagnetPoint point = other.GetComponent<MagnetPoint>();
        if (point == null) return;

        if (points.Contains(point)) return;

        points.Add(point);
        if (attachment.HasProperOrientation) point.OnEnterArea(attachment);
    }
    private void OnTriggerExit(Collider other)
    {
        MagnetPoint point = other.GetComponent<MagnetPoint>();
        if (point == null) return;

        if (points.Contains(point))
        {
            point.OnExitArea(attachment);
            points.Remove(point);
        }
    }


    private void Update()
    {
        if (goodOrientation)
        {
            if (!attachment.HasProperOrientation)
            {
                goodOrientation = false;
                foreach (MagnetPoint point in points) point.OnEnterArea(attachment);
            }
        }
        else
        {
            if (attachment.HasProperOrientation)
            {
                goodOrientation = false;
                foreach (MagnetPoint point in points) point.OnExitArea(attachment);
                attachment.DetachAllChildren();
            }
        }
    }
}
