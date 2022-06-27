using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] protected UIPopUp popup;
    [SerializeField] protected Attachment attachment;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource audioSource;

    private void OnValidate()
    {
        if (popup == null) GetComponent<UIPopUp>();
        if (attachment == null) GetComponent<Attachment>();
        if (animator == null) GetComponent<Animator>();
        if (audioSource == null) GetComponent<AudioSource>();
    }



    public virtual bool Open()
    {
        if (popup.isUp) return false;

        popup.PopUp();
        animator.SetBool("Open", true);
        if (audioSource != null) audioSource.Play();
        return true;
    }

    public virtual bool Close()
    {
        if (attachment.IsAttached || !popup.isUp) return false;

        animator.SetBool("Open", false);
        return true;
    }

    public void NotificationClose()
    {
        Close();
    }
    public void NotificationPopOff()
    {
        popup.PopOff();
    }
}
