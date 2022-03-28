using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    List<MistakeType> mistakeList = new List<MistakeType>();
    public List<MistakeType> MistakeList { get => mistakeList; }

    public delegate void MistakeTypeAction(MistakeType type);
    public event MistakeTypeAction OnAddMistake;

    public void AddMistake(MistakeType mistakeType)
    {
        mistakeList.Add(mistakeType);

        if(OnAddMistake != null)
            OnAddMistake(mistakeType);

        //MistakeToConsole(mistakeType);
    }



    private void MistakeToConsole(MistakeType mistakeType)
    {
        Mistake mistake = MistakeLibrary.GetMistake(mistakeType);
        Debug.Log(mistake.Title + " - " + mistake.Description);
    }
}
