using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "NewObject/TaskData")]
public class TaskData : ScriptableObject
{
    public TaskJob Job;
    public TaskDifficuty Difficuty;
    public TaskTime Time;
    public TaskOrderTime OrderTime;
}
