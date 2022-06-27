using UnityEngine;
using TMPro;

public class PoolObject_UIMistakeButton : PoolObject
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject selectedBackground;


    private UIPanel_MistakeList list;
    private MistakeType mistakeType;

    public MistakeType MistakeType { get => mistakeType; }

    public void SetUp(UIPanel_MistakeList list, MistakeType type)
    {
        this.list = list;

        mistakeType = type;
        Mistake mistake = MistakeLibrary.GetMistake(type);
        text.text = mistake.Title;
    }

    public void OnPressed()
    {
        list.PressMistakeButton(this);
    }


    public void SetSelected()
    {
        selectedBackground.SetActive(true);
    }
    public void SetUnselected()
    {
        selectedBackground.SetActive(false);
    }
}
