using UnityEngine;

public class MistakeNotification : MonoBehaviour
{
    [SerializeField] UIPanel_MistakeList mistakeList;
    [SerializeField] UIPopUp popup;
    [SerializeField] UIPopUpResponsiveness popupResponsiveness;
    [SerializeField] Animator animator;
    [SerializeField] Attachment attachment;
    [SerializeField] AudioSource audioSource;


    
    private void OnEnable()
    {
        GameManager.PerformanceManager.OnAddMistake += OnAddMistake;
        popupResponsiveness.OnShouldPopOff += OnShouldPopOff;
    }
    private void OnDestroy()
    {
        GameManager.PerformanceManager.OnAddMistake -= OnAddMistake;
        popupResponsiveness.OnShouldPopOff -= OnShouldPopOff;
    }


    private void OnAddMistake(MistakeType type)
    {
        Open();
    }
    private void OnShouldPopOff()
    {
        if (popup.isUp)
            Close();
    }


    private void Open()
    {
        if (popup.isUp) return;
        popup.PopUp();

        audioSource.Play();

        mistakeList.Open();

        animator.SetBool("Open", true);
    }
    public void Close()
    {
        if (attachment.IsAttached) return;

        mistakeList.Close();

        animator.SetBool("Open", false);
    }

    public void OnFinishCloseAnimation()
    {
        mistakeList.Close();

        popup.PopOff();
    }
}
