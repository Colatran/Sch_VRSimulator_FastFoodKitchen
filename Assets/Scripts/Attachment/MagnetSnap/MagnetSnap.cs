using System.Collections.Generic;
using UnityEngine;

public class MagnetSnap : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] MagnetSnapPositionGroups positionGroups;



    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponentInParent<Attachment>();
    }



    private List<Attachment> attachmentsMustIgnore = new List<Attachment>();

    private void OnTriggerEnter(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();
        if (attachment == null) return;

        if (attachmentsMustIgnore.Contains(attachment))
        {
            attachmentsMustIgnore.Remove(attachment);
            return;
        }

        foreach (PositionGroup group in positionGroups.Groups)
        {
            if (positionGroups.ChecksCondition(group.Condition, attachment))
            {
                group.TryAttach(attachment, this.attachment);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();

        attachmentsMustIgnore.Remove(attachment);
    }
}
