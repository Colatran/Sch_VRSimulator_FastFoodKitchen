using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPopUp_InitialStats : UIPopUp
{
    [Header("Buttons")]
    [SerializeField] Button button_Start;

    [Header("")]
    [SerializeField] TMP_Text text_job;
    [SerializeField] TMP_Text text_time;
    [SerializeField] TMP_Text text_orderTime;
    [SerializeField] TMP_Text text_difficulty;

    private void OnEnable()
    {
        button_Start.onClick.AddListener(CallClose);
    }
    private void OnDisable()
    {
        button_Start.onClick.RemoveListener(CallClose);
    }


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
        text_job.text = Task.GetJobName(GameManager.TaskData.Job);
        text_time.text = Task.GetTimeName(GameManager.TaskData.Time);
        text_orderTime.text = Task.GetOrderTimeName(GameManager.TaskData.OrderTime);
        text_difficulty.text = Task.GetDifficultyName(GameManager.TaskData.Difficuty);
    }
}
