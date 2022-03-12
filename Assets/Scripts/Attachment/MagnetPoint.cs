using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    [SerializeField] MagnetPointsManager manager;

    private void OnValidate()
    {
        if(manager == null) manager = GetComponentInParent<MagnetPointsManager>();
    }



    private List<Attachment> parents = new List<Attachment>();

    public bool HasPotentialParent(Attachment parent) => parents.Contains(parent);
    public bool HasEndParent(Attachment parent)
    {
        foreach (Attachment item in parents)
        {
            if (item.EndParent == parent) return true;
        }
        return false;
    }
    public Attachment GetWithEndParent(Attachment parent)
    {
        foreach (Attachment item in parents)
        {
            if (item.EndParent == parent) return item;
        }
        return null;
    }

    public void OnEnterArea(Attachment attachment)
    {
        parents.Add(attachment);

        manager.PointInArea(attachment, parents.Count == 1);
    }
    public void OnExitArea(Attachment attachment)
    {
        bool anyParentBefore = parents.Count > 0;

        parents.Remove(attachment);

        manager.PointExitArea(attachment, anyParentBefore && parents.Count == 0);
    }
}
