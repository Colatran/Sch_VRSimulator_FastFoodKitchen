using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenuPanel_Start : UIMainMenuPanel
{
    [Header("Start")] 
    [SerializeField] TaskData taskData;
    [SerializeField] TMP_Dropdown dropdown_job;
    [SerializeField] TMP_Dropdown dropdown_time;
    [SerializeField] TMP_Dropdown dropdown_orderTime;
    [SerializeField] TMP_Dropdown dropdown_difficulty;
    [SerializeField] Button button_start;
    [SerializeField] Button button_startQuit;


    private void OnEnable()
    {
        dropdown_job.onValueChanged.AddListener(Dropdown_Job_OnValueChanged);
        dropdown_time.onValueChanged.AddListener(Dropdown_Time_OnValueChanged);
        dropdown_orderTime.onValueChanged.AddListener(Dropdown_OrderTime_OnValueChanged);
        dropdown_difficulty.onValueChanged.AddListener(Dropdown_Difficulty_OnValueChanged);
        button_start.onClick.AddListener(Button_Start_OnClick);
        button_startQuit.onClick.AddListener(Button_StartQuit_OnClick);
    }
    private void OnDisable()
    {
        dropdown_job.onValueChanged.RemoveListener(Dropdown_Job_OnValueChanged);
        dropdown_time.onValueChanged.RemoveListener(Dropdown_Time_OnValueChanged);
        dropdown_orderTime.onValueChanged.RemoveListener(Dropdown_OrderTime_OnValueChanged);
        dropdown_difficulty.onValueChanged.RemoveListener(Dropdown_Difficulty_OnValueChanged);
        button_start.onClick.RemoveListener(Button_Start_OnClick);
        button_startQuit.onClick.RemoveListener(Button_StartQuit_OnClick);
    }

    private void Start()
    {
        TaskJob taskJob = taskData.Job;
        if (taskJob == TaskJob.PREPARADOR) dropdown_job.SetValueWithoutNotify(0);
        else if (taskJob == TaskJob.BATCHER) dropdown_job.SetValueWithoutNotify(1);
        else dropdown_job.SetValueWithoutNotify(0);

        TaskTime taskTime = taskData.Time;
        if (taskTime == TaskTime.MIN5) dropdown_time.SetValueWithoutNotify(0);
        else if (taskTime == TaskTime.MIN10) dropdown_time.SetValueWithoutNotify(1);
        else if(taskTime == TaskTime.MIN15) dropdown_time.SetValueWithoutNotify(2);
        else if(taskTime == TaskTime.MIN30) dropdown_time.SetValueWithoutNotify(3);
        else if(taskTime == TaskTime.MIN45) dropdown_time.SetValueWithoutNotify(4);
        else if(taskTime == TaskTime.MIN60) dropdown_time.SetValueWithoutNotify(5);
        else dropdown_time.SetValueWithoutNotify(0);

        TaskOrderTime taskOrderTime = taskData.OrderTime;
        if (taskOrderTime == TaskOrderTime.SLOW) dropdown_orderTime.SetValueWithoutNotify(0);
        else if (taskOrderTime == TaskOrderTime.MEDIUM) dropdown_orderTime.SetValueWithoutNotify(1);
        else if (taskOrderTime == TaskOrderTime.FAST) dropdown_orderTime.SetValueWithoutNotify(2);
        else dropdown_orderTime.SetValueWithoutNotify(2);

        TaskDifficuty taskDifficuty = taskData.Difficuty;
        if (taskDifficuty == TaskDifficuty.EASY) dropdown_difficulty.SetValueWithoutNotify(0);
        else if (taskDifficuty == TaskDifficuty.NORMAL) dropdown_difficulty.SetValueWithoutNotify(1);
        else if (taskDifficuty == TaskDifficuty.HARD) dropdown_difficulty.SetValueWithoutNotify(2);
        else dropdown_difficulty.SetValueWithoutNotify(0);
    }



    private void Dropdown_Job_OnValueChanged(int val)
    {
        TaskJob taskJob;
        if (val == 0) taskJob = TaskJob.PREPARADOR;
        else if (val == 1) taskJob = TaskJob.BATCHER;
        else taskJob = TaskJob.BATCHER;

        taskData.Job = taskJob;
    }
    private void Dropdown_Time_OnValueChanged(int val)
    {
        TaskTime taskTime;
        if (val == 0) taskTime = TaskTime.MIN5;
        else if (val == 1) taskTime = TaskTime.MIN10;
        else if (val == 2) taskTime = TaskTime.MIN15;
        else if (val == 3) taskTime = TaskTime.MIN30;
        else if (val == 4) taskTime = TaskTime.MIN45;
        else if (val == 5) taskTime = TaskTime.MIN60;
        else taskTime = TaskTime.MIN5;

        taskData.Time = taskTime;
    }
    private void Dropdown_OrderTime_OnValueChanged(int val)
    {
        TaskOrderTime taskOrderTime;
        if (val == 0) taskOrderTime = TaskOrderTime.SLOW;
        else if (val == 1) taskOrderTime = TaskOrderTime.MEDIUM;
        else if (val == 2) taskOrderTime = TaskOrderTime.FAST;
        else taskOrderTime = TaskOrderTime.MEDIUM;

        taskData.OrderTime = taskOrderTime;
    }
    private void Dropdown_Difficulty_OnValueChanged(int val)
    {
        TaskDifficuty taskDifficuty;
        if (val == 0) taskDifficuty = TaskDifficuty.EASY;
        else if (val == 1) taskDifficuty = TaskDifficuty.NORMAL;
        else if (val == 2) taskDifficuty = TaskDifficuty.HARD;
        else taskDifficuty = TaskDifficuty.EASY;

        taskData.Difficuty = taskDifficuty;
    }
    private void Button_Start_OnClick()
    {
        TaskJob taskJob = taskData.Job;
        int index;

        switch (taskJob)
        {
            case TaskJob.PREPARADOR:
                index = 1;
                break;
            case TaskJob.BATCHER:
                index = 2;
                break;
            default:
                index = 2;
                break;
        }

        LevelManager.Instance.LoadScene(index);
    }
    private void Button_StartQuit_OnClick()
    {
        ResetToMainMenu();
    }
}
