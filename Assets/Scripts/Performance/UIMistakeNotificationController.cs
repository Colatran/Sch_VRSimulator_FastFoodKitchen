using UnityEngine;

public class UIMistakeNotificationController : MonoBehaviour
{
    [SerializeField] MistakeList mistakeList;
    [SerializeField] UIPopUp popup;
    [SerializeField] UIPopUpResponsiveness popupResponsiveness;
    [SerializeField] Animator animator;
    [SerializeField] Attachment attachment;



    private void OnEnable()
    {
        GameManager.PerformanceManager.OnAddMistake += OnAddMistake;
        mistakeList.OnButtonPressed += OnButtonPressed;
        popupResponsiveness.OnShouldPopOff += OnShouldPopOff;
    }
    private void OnDestroy()
    {
        GameManager.PerformanceManager.OnAddMistake -= OnAddMistake;
        mistakeList.OnButtonPressed -= OnButtonPressed;
        popupResponsiveness.OnShouldPopOff -= OnShouldPopOff;
    }


    private void OnAddMistake(MistakeType type)
    {
        Open();
    }
    private void OnButtonPressed()
    {
        if (!openDescription) OpenDescriptionSpoiler();
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

        mistakeList.Open();

        animator.SetTrigger("Reset");
        animator.SetBool("Open", true);
    }
    public void Close()
    {
        if (attachment.IsAttached) return;

        SetOpenHint(false);
        SetOpenDescription(false);

        animator.SetBool("Open", false);
    }
    public void OnFinishCloseAnimation()
    {
        mistakeList.Close();

        popup.PopOff();
    }



    [Header("")]
    [SerializeField] RectTransform spoilerArrow_Description;
    [SerializeField] RectTransform spoilerArrow_Hint;

    private bool openDescription = false;
    private bool openHint = false;

    public void OpenDescriptionSpoiler()
    {
        SetOpenDescription(!openDescription);

        SetOpenHint(false);
    }
    public void OpenHintSpoiler()
    {
        if (!openDescription) return;

        SetOpenHint(!openHint);
    }

    private void SetOpenDescription(bool open)
    {
        openDescription = open;

        if (openDescription)
            spoilerArrow_Description.localScale = new Vector3(2, -1, 1);
        else
            spoilerArrow_Description.localScale = new Vector3(2, 1, 1);

        animator.SetBool("Description", openDescription);
    }
    private void SetOpenHint(bool open)
    {
        openHint = open;

        if (openHint)
            spoilerArrow_Hint.localScale = new Vector3(2, -1, 1);
        else
            spoilerArrow_Hint.localScale = new Vector3(2, 1, 1);

        animator.SetBool("Hint", openHint);
    }
}
