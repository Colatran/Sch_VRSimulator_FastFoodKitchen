using UnityEngine;
using TMPro;

public class Notification_InitialStats : Notification
{
    [Header("")]
    [SerializeField] TMP_Text text_job;
    [SerializeField] TMP_Text text_difficulty;
    [SerializeField] TMP_Text text_time;



    public override bool Open()
    {
        if(base.Open())
        {
            SetStats();
            return true;
        }
        return false;
    }
    public override bool Close()
    {
        if (base.Close())
        {
            GameManager.StartTask();
            return true;
        }
        return false;
    }


    private void SetStats()
    {
        text_job.text = Task.GetJobName(GameManager.TaskData.taskJob);
        text_difficulty.text = Task.GetDifficultyName(GameManager.TaskData.taskDifficuty);
        text_time.text = Task.GetTimeName(GameManager.TaskData.taskTime);
    }
}
