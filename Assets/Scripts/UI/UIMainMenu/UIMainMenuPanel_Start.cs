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
        if (taskJob == TaskJob.TUTORIAL) dropdown_job.SetValueWithoutNotify(0);
        else if (taskJob == TaskJob.BATCHER) dropdown_job.SetValueWithoutNotify(1);
        else if (taskJob == TaskJob.PREPARADOR) dropdown_job.SetValueWithoutNotify(2);
        else dropdown_job.SetValueWithoutNotify(0);
        Start_UnlockSettings(taskJob != TaskJob.TUTORIAL);

        TaskTime taskTime = taskData.Time;
        if (taskTime == TaskTime.MIN5) dropdown_time.SetValueWithoutNotify(0);
        else if (taskTime == TaskTime.MIN10) dropdown_time.SetValueWithoutNotify(1);
        else if(taskTime == TaskTime.MIN15) dropdown_time.SetValueWithoutNotify(2);
        else if(taskTime == TaskTime.MIN30) dropdown_time.SetValueWithoutNotify(3);
        else if(taskTime == TaskTime.MIN45) dropdown_time.SetValueWithoutNotify(4);
        else if(taskTime == TaskTime.MIN60) dropdown_time.SetValueWithoutNotify(5);
        else dropdown_time.SetValueWithoutNotify(0);

        TaskOrderTime taskOrderTime = taskData.OrderTime;
        if (taskOrderTime == TaskOrderTime.SEG120) dropdown_orderTime.SetValueWithoutNotify(0);
        else if (taskOrderTime == TaskOrderTime.SEG90) dropdown_orderTime.SetValueWithoutNotify(1);
        else if (taskOrderTime == TaskOrderTime.SEG60) dropdown_orderTime.SetValueWithoutNotify(2);
        else if (taskOrderTime == TaskOrderTime.SEG45) dropdown_orderTime.SetValueWithoutNotify(3);
        else if (taskOrderTime == TaskOrderTime.SEG30) dropdown_orderTime.SetValueWithoutNotify(4);
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
        if(val == 0) taskJob = TaskJob.TUTORIAL;
        else if (val == 1) taskJob = TaskJob.BATCHER;
        else if (val == 2) taskJob = TaskJob.PREPARADOR;
        else taskJob = TaskJob.BATCHER;

        taskData.Job = taskJob;

        Start_UnlockSettings(val != 0);
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
        if (val == 0) taskOrderTime = TaskOrderTime.SEG120;
        else if (val == 1) taskOrderTime = TaskOrderTime.SEG90;
        else if (val == 2) taskOrderTime = TaskOrderTime.SEG60;
        else if (val == 3) taskOrderTime = TaskOrderTime.SEG45;
        else if (val == 4) taskOrderTime = TaskOrderTime.SEG30;
        else taskOrderTime = TaskOrderTime.SEG60;

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

    }
    private void Button_StartQuit_OnClick()
    {
        ResetToMainMenu();
    }

    private void Start_UnlockSettings(bool unlock)
    {
        dropdown_time.interactable = unlock;
        dropdown_orderTime.interactable = unlock;
        dropdown_difficulty.interactable = unlock;
    }
}
