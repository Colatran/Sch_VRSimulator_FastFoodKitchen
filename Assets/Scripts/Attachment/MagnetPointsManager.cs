using System.Collections.Generic;
using UnityEngine;

public class MagnetPointsManager : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] MagnetPoint[] points;
    [SerializeField] int minPoints = 0;

    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponent<Attachment>();
        if (points.Length == 0) points = GetComponentsInChildren<MagnetPoint>();
    }



    private List<Attachment> potencialParents = new List<Attachment>();
    private int currentPoints = 0;

    public void PointInArea(Attachment attachment, bool check)
    {
        if (check) currentPoints++;

        if (currentPoints >= minPoints) CheckInArea(attachment);
    }
    public void PointExitArea(Attachment attachment, bool check)
    {
        if (check) currentPoints--;

        CheckOutArea(attachment);
    }
    
    private void CheckInArea(Attachment attachment)
    {
        int numPoints = 0;

        if (potencialParents.Contains(attachment)) return;

        foreach (MagnetPoint point in points)
        {
            if (point.HasPotencialParent(attachment))
                numPoints++;

            if (numPoints == minPoints)
            {
                potencialParents.Add(attachment);
                return;
            }
        }
    }
    private void CheckOutArea(Attachment attachment)
    {
        int numPoints = 0;

        foreach (MagnetPoint point in points)
            if (point.HasPotencialParent(attachment))
                numPoints++;

        if (numPoints < minPoints)
            potencialParents.Remove(attachment);
    }



    private void Update()
    {
        if (attachment.IsNotAttached)
        {
            foreach (Attachment pParent in potencialParents)
            {
                if (
                    pParent.IsAttachable
                    && transform.position.y - pParent.transform.position.y > 0
                    && attachment.HasProperOrientation(pParent.transform)
                    )
                {
                    attachment.AttachTo(pParent, true);
                    potencialParents.RemoveAll(x => x == pParent);
                    return;
                }
            }
        }
    }
}
