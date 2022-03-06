using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    [SerializeField] MagnetPointsManager manager;

    private void OnValidate()
    {
        manager = GetComponentInParent<MagnetPointsManager>();
    }


    private List<Attachment> potencialParents = new List<Attachment>();
    public List<Attachment> PotencialParents { get => potencialParents; }

    public void OnEnterArea(Attachment attachment)
    {
        potencialParents.Add(attachment);
        if (potencialParents.Count == 1) manager.PointInArea();
    }
    public void OnExitArea(Attachment attachment)
    {
        potencialParents.Remove(attachment);
        if (potencialParents.Count == 0) manager.PointExitArea();
    }
}
