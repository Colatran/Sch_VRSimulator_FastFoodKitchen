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

        if(item.Is(type))
        {
            attachmentInRange = item.Attachment;
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

            attachmentInRange = null;
        }
    }

}
