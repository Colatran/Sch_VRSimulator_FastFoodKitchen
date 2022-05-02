using System.Collections.Generic;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    [SerializeField] Attachment attachment;

    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponentInParent<Attachment>();

        if (gameObject.layer != 11) Debug.LogError(gameObject.name + " - MagnetArea deve estar na layer 11(MagnetArea)!!!");

        Collider collider = GetComponent<Collider>();
        if(collider != null)
        {
            if (!collider.isTrigger)
                Debug.LogError(gameObject.name + " - MagnetArea tem de ser trigger!!!");
        }
    }



    private bool goodOrientation = true;
    private List<MagnetPoint> points = new List<MagnetPoint>();


    private void OnTriggerEnter(Collider other)
    {
        MagnetPoint point = other.GetComponent<MagnetPoint>();
        if (point == null) return;

        if (points.Contains(point)) return;

        points.Add(point);
        if (attachment.IsAttachable) point.OnEnterArea(attachment);
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
            if (attachment.IsNotAttachable)
            {
                goodOrientation = false;
                foreach (MagnetPoint point in points) point.OnExitArea(attachment);
                attachment.DetachAllChildren();
            }
        }
        else
        {
            if (attachment.IsAttachable)
            {
                goodOrientation = true;
                foreach (MagnetPoint point in points) point.OnEnterArea(attachment);
            }
        }
    }


    public void RectifyPoints()
    {
        points.RemoveAll(x => x == null);
    }
}
