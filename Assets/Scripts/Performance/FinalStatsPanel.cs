using UnityEngine;
using TMPro;

public class FinalStatsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text text_job;
    [SerializeField] TMP_Text text_difficulty;
    [SerializeField] TMP_Text text_time;
    [SerializeField] TMP_Text text_score;
    [SerializeField] TMP_Text text_dirt;
    [SerializeField] TMP_Text text_mistakes;

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
