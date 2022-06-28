using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    [SerializeField] protected GameObject[] objects;
    [SerializeField] protected UIPopUpResponsiveness responsiveness;
    [SerializeField] protected Attachment attachment;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource audioSource;

    protected bool isOpen = false;
    public bool IsOpen { get => isOpen; }

    private void OnValidate()
    {
        if (responsiveness == null) GetComponent<UIPopUpResponsiveness>();
        if (attachment == null) GetComponent<Attachment>();
        if (animator == null) GetComponent<Animator>();
        if (audioSource == null) GetComponent<AudioSource>();
    }





    protected void PopUp()
    {
        if (isOpen) return;
        isOpen = true;

        SetObjectsActive(true);

        if (responsiveness != null)
            responsiveness.PopUp();
    }
    protected void PopOff()
    {
        if (!isOpen) return;
        isOpen = false;

        SetObjectsActive(false);

        if (responsiveness != null)
            responsiveness.PopOff();
    }

    protected void SetObjectsActive(bool active)
    {
        foreach (GameObject _object in objects)
        {
            _object.SetActive(active);
        }
    }





    public virtual bool Open()
    {
        if (isOpen) return false;

        PopUp();
        animator.SetBool("Open", true);
        if (audioSource != null) audioSource.Play();
        return true;
    }
    public virtual bool Close()
    {
        if (attachment.IsAttached || !isOpen) return false;

        animator.SetBool("Open", false);
        return true;
    }

    public void CallClose()
    {
        Close();
    }
}
