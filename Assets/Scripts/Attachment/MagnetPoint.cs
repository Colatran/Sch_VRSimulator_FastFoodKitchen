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

    public bool HasPotencialParent(Attachment parent) => parents.Contains(parent);


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
