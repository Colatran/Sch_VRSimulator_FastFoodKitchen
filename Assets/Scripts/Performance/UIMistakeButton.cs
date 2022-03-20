using UnityEngine;
using TMPro;

public class UIMistakeButton : PoolObject
{
    [SerializeField] TMP_Text text;



    private UIMistakeList list;
    private MistakeType mistakeType;

    public void SetUpButton(UIMistakeList list, MistakeType type)
    {
        this.list = list;

        mistakeType = type;
        Mistake mistake = MistakeLibrary.GetMistake(type);
        text.text = mistake.Title;
    }

    public void OnPressed() 
    { 
        list.SetDescription(mistakeType);
    }
}
