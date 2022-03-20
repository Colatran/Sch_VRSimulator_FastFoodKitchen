using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField] UIPopup popup;



    List<MistakeType> mistakeList = new List<MistakeType>();
    public List<MistakeType> MistakeList { get => mistakeList; }

    public delegate void MistakeTypeAction(MistakeType type);
    public event MistakeTypeAction OnAddMistake;

    public void AddMistake(MistakeType type)
    {
        mistakeList.Add(type);

        if(OnAddMistake != null)
            OnAddMistake(type);

        popup.PopUp();
    }
}
