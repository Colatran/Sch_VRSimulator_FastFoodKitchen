using UnityEngine;

public class Notification_Mistake : Notification
{
    [Header("")]
    [SerializeField] UIPanel_MistakeList mistakeList;
    [SerializeField] UIPopUpResponsiveness popupResponsiveness;


    
    private void OnEnable()
    {
        popupResponsiveness.OnShouldPopOff += NotificationClose;
    }
    private void OnDestroy()
    {
        popupResponsiveness.OnShouldPopOff -= NotificationClose;
    }

    public override bool Open()
    {
        if (base.Open())
        {
            mistakeList.Open();
            return true;
        }
        return false;
    }
    public override bool Close()
    {
        if (base.Close())
        {
            mistakeList.Close();
            return true;
        }
        return false;
    }
}
