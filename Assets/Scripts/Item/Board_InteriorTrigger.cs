using System.Collections.Generic;
using UnityEngine;

public class Board_InteriorTrigger : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] ItemType type;
    [SerializeField] Vector3 position;

    private void OnValidate()
    {
        attachment = GetComponentInParent<Attachment>();   
    }



    private Attachment attachmentInRange;
    private List<Attachment> attachmentsMustIgnore = new List<Attachment>();

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null)
        {
            Attachment attachment = other.GetComponent<Attachment>();
            Attachment childAttachment = attachment.DirectChildren[0];
            if (childAttachment == null) return;

            item = childAttachment.GetComponent<Item>();
            if(item == null) return;
        }

        Attachment itemAttachment = item.Attachment;

        if (itemAttachment == attachmentsMustIgnore.Contains(item.Attachment)) {
            attachmentsMustIgnore.Remove(itemAttachment);
            return;
        }

        if (item.Is(type))
        {
            attachmentInRange = itemAttachment;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();

        if (attachment == attachmentInRange) attachmentInRange = null;
    }


    private void Update()
    {
        if (attachmentInRange == null) return;

        if (attachmentInRange.HasProperOrientation(null))
        {
            attachmentInRange.Attach(attachment);
            attachmentInRange.transform.localRotation = Quaternion.identity;
            attachmentInRange.transform.localPosition = position;

            attachmentsMustIgnore.Add(attachmentInRange);

            attachmentInRange = null;
        }
    }

}
