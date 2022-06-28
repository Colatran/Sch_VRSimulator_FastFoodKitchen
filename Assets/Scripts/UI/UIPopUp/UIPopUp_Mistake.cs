using UnityEngine;
using UnityEngine.UI;

public class UIPopUp_Mistake : UIPopUp
{
    [Header("Buttons")]
    [SerializeField] Button button_Close;

    [Header("")]
    [SerializeField] UIPanel_MistakeList mistakeList;


    
    private void OnEnable()
    {
        button_Close.onClick.AddListener(CallClose);
        responsiveness.OnShouldPopOff += CallClose;
    }
    private void OnDestroy()
    {
        button_Close.onClick.RemoveListener(CallClose);
        responsiveness.OnShouldPopOff -= CallClose;
    }

    public override bool Open()
    {
        if (base.Open())
        {
            mistakeList.Open();
            return true;
        }
        return false;
    }
    public override bool Close()
    {
        if (base.Close())
        {
            mistakeList.Close();
            return true;
        }
        return false;
    }
}
