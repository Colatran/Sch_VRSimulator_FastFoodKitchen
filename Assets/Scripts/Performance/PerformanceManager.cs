using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField] UIPopup MistakeListPopUp;


    List<MistakeType> mistakeList = new List<MistakeType>();
    public List<MistakeType> MistakeList { get => mistakeList; }


    public void AddMistake(MistakeType type)
    {
        mistakeList.Add(type);

        Mistake mistake = MistakeLibrary.GetMistake(type);



        Debug.Log(mistake.Title + " - " + mistake.Description);



        MistakeListPopUp.PopUp();
    }
}
