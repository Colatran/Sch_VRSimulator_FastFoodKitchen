using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "NewObject/TaskData")]
public class TaskData : ScriptableObject
{
    public TaskJob taskJob;
    public TaskDifficuty taskDifficuty;
    public TaskTime taskTime;
}
