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



    private List<Attachment> potentialParents = new List<Attachment>();
    private List<Attachment> potentialParentsColective = new List<Attachment>();
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
        if (potentialParents.Contains(attachment)) return;

        int numPoints = 0;
        foreach (MagnetPoint point in points)
        {
            if (point.HasPotentialParent(attachment))
                numPoints++;

            if (numPoints == minPoints)
            {
                potentialParents.Add(attachment);
                return;
            }
        }


        if (potentialParentsColective.Contains(attachment)) return;

        Attachment endParent = attachment.EndParent;
        if (endParent == null) return;
        numPoints = 0;
        foreach (MagnetPoint point in points)
        {
            if (point.HasEndParent(endParent))
                numPoints++;

            if (numPoints == minPoints)
            {
                potentialParentsColective.Add(attachment);
                return;
            }
        }
    }
    private void CheckOutArea(Attachment attachment)
    {
        int numPoints = 0;

        foreach (MagnetPoint point in points)
            if (point.HasPotentialParent(attachment))
                numPoints++;

        if (numPoints < minPoints)
            potentialParents.Remove(attachment);
    }


    private bool IsAttachable(Attachment pParent)
    {
        return
            pParent.IsAttachable
            && transform.position.y - pParent.transform.position.y > 0
            && attachment.HasProperOrientation(pParent.transform);
    }

    private void Update()
    {
        if (attachment.IsNotAttached)
        {
            foreach (Attachment pParent in potentialParents)
            {
                if (IsAttachable(pParent))
                {
                    attachment.Attach(pParent);
                    potentialParents.Remove(pParent);
                    return;
                }
            }


            foreach (Attachment pParent in potentialParentsColective)
            {
                if (IsAttachable(pParent))
                {
                    foreach (MagnetPoint point in points)
                    {
                        Attachment parent = point.GetWithEndParent(pParent.EndParent);
                        if (parent == null || parent == pParent) continue;
                        attachment.AddColectiveParent(parent);
                    }

                    attachment.Attach(pParent);
                    potentialParentsColective.Remove(pParent);
                    return;
                }
            }
        }
    }
}
