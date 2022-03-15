using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerformanceManager", menuName = "NewObject/PerformanceManager")]
public class PerformanceManager : ScriptableObject
{
    //Mistake List
    private List<MistakeType> MistakeList { get; set; } = new List<MistakeType>();
    public List<MistakeType> ListMistakes() => MistakeList;
    public void AddMistake(MistakeType type) => MistakeList.Add(type);
    public void Clear() => MistakeList.Clear();
}