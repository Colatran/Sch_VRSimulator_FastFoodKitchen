using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    [SerializeField] MagnetPointsManager manager;

    private void OnValidate()
    {
        if (manager == null) manager = GetComponentInParent<MagnetPointsManager>();

        if (gameObject.layer != 12) Debug.LogError(gameObject.name + " - MagnetPoint - deve estar na layer 12(MagnetPoint)!!!");
        if (GetComponent<Collider>() == null) Debug.LogError(gameObject.name + " - MagnetPoint - precisa de um collider!!!");
        else if (GetComponent<Collider>().isTrigger) Debug.LogError(gameObject.name + " - MagnetPoint - não pode ser trigger!!!");
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
        if(!parents.Contains(attachment))
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
