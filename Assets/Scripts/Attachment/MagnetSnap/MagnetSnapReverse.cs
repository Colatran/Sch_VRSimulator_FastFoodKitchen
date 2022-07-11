using UnityEngine;

public class MagnetSnapReverse : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] GameObject magnetSnap;
    private Attachment child;

    private void OnEnable()
    {
        attachment.OnAddContent += Attachment_OnAddContent;
    }

    private void Attachment_OnAddContent(Attachment attachment)
    {
        child = attachment;

        magnetSnap.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();
        if (attachment == null) return;

        if(attachment == child)
        {
            child = null;
            magnetSnap.SetActive(true);
        }
    }
}
