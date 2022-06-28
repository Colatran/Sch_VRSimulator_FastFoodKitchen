using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPopUp_FinalStats : UIPopUp
{
    [Header("MistakeList")]
    [SerializeField] UIPanel_MistakeList mistakeList;

    [Header("Buttons")]
    [SerializeField] Button button_end;

    [Header("Stats")]
    [SerializeField] TMP_Text text_job;
    [SerializeField] TMP_Text text_difficulty;
    [SerializeField] TMP_Text text_time;
    [SerializeField] TMP_Text text_score;
    [SerializeField] TMP_Text text_dirt;
    [SerializeField] TMP_Text text_mistakes;

    private void OnEnable()
    {
        button_end.onClick.AddListener(CallClose);
    }
    private void OnDisable()
    {
        button_end.onClick.RemoveListener(CallClose);
    }





    public override bool Open()
    {
        if (base.Open())
        {
            SetStats();
            mistakeList.Open();
            return true;
        }
        return false;
    }
    public override bool Close()
    {
        if (base.Close())
        {
            GameManager.QuitToMainMenu();
            return true;
        }
        return false;
    }


    public void SetStats()
    {
        text_job.text = Task.GetJobName(GameManager.TaskData.taskJob);
        text_difficulty.text = Task.GetDifficultyName(GameManager.TaskData.taskDifficuty);
        text_time.text = Task.GetTimeName(GameManager.TaskData.taskTime);
        text_score.text = GameManager.TotalServed + "";
        text_dirt.text = GameManager.TotalDirt + "";
        text_mistakes.text = GameManager.PerformanceManager.Mistakes.Count + "";
    }
}
