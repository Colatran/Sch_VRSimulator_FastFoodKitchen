using UnityEngine;

public class Notification_Mistake : Notification
{
    [Header("")]
    [SerializeField] UIPanel_MistakeList mistakeList;
    [SerializeField] UIPopUpResponsiveness popupResponsiveness;


    
    private void OnEnable()
    {
        GameManager.PerformanceManager.OnAddMistake += OnAddMistake;
        popupResponsiveness.OnShouldPopOff += NotificationClose;
    }
    private void OnDestroy()
    {
        GameManager.PerformanceManager.OnAddMistake -= OnAddMistake;
        popupResponsiveness.OnShouldPopOff -= NotificationClose;
    }


    private void OnAddMistake(MistakeType type)
    {
        if (GameManager.TaskData.taskDifficuty == TaskDifficuty.HARD) return;
        Open();
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
