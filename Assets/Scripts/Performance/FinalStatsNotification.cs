using UnityEngine;

public class FinalStatsNotification : MonoBehaviour
{
    [SerializeField] UIPanel_MistakeList mistakeList;
    [SerializeField] UIPopUp popup;
    [SerializeField] Attachment attachment;
    [SerializeField] Animator animator;

    public void Open()
    {
        if (popup.isUp) return;
        popup.PopUp();

        mistakeList.Open();

        animator.SetBool("Open", true);
    }

    public void Finish()
    {
        if (attachment.IsAttached) return;
        Debug.Log("Finalize");
    }
}
