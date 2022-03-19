using UnityEngine;
using TMPro;

public class UIMistakeButton : PoolObject
{
    [SerializeField] TMP_Text text;
    [SerializeField] UIMistakeList list;

    public void SetList(UIMistakeList List) => list = List;




    private MistakeType mistakeType;


    public void SetMistakeType(MistakeType type)
    {
        mistakeType = type;

        Mistake mistake = MistakeLibrary.GetMistake(type);
        text.text = mistake.Title;
    }

    public void OnPressed() 
    { 
        list.SetDescription(mistakeType);
    }
}
