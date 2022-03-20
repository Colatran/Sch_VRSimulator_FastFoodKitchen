using System.Collections.Generic;
using UnityEngine;

public class UIMistakeList : MonoBehaviour
{
    const float _presentTime = .0625f;

    [SerializeField] PerformanceManager manager;
    [SerializeField] GameObjectPool pool;
    [SerializeField] UIPopup popup;
    [SerializeField] Transform content;
    [ReadOnly, SerializeField] Transform poolContainer;

    private void OnValidate()
    {
        poolContainer = pool.Container;
    }

    private void Awake()
    {
        manager.OnAddMistake += AddMistake;
        popup.OnPopUp += OnPopUp;
        popup.OnPopOff += OnPopOff;
    }
    private void OnDestroy()
    {
        manager.OnAddMistake -= AddMistake;
        popup.OnPopUp -= OnPopUp;
        popup.OnPopOff -= OnPopOff;
    }


    private float presentTime;
    private List<MistakeType> mistakesToPresent = new List<MistakeType>();
    public void AddMistake(MistakeType type) => mistakesToPresent.Add(type);

    private void PresentNextMistake()
    {
        if (mistakesToPresent.Count == 0) return;

        GameObject b_object = pool.GetObject();
        b_object.transform.SetParent(content, false);

        UIMistakeButton button = b_object.GetComponent<UIMistakeButton>();
        button.SetUpButton(this, mistakesToPresent[0]);
        mistakesToPresent.RemoveAt(0);

        b_object.SetActive(true);
    } 

    private void OnPopUp()
    {
        SetDescription(mistakesToPresent[0]);
        PresentNextMistake();
        presentTime = _presentTime;
    }
    private void OnPopOff()
    {
        while (content.childCount > 0)
        {
            GameObject c_object = content.GetChild(0).gameObject;
            c_object.SetActive(false);
            c_object.transform.SetParent(poolContainer, false);
        }

        mistakesToPresent.Clear();
    }

    private void Update()
    {
        if (mistakesToPresent.Count > 0)
        {
            presentTime -= Time.deltaTime;

            if (presentTime < 0)
            {
                PresentNextMistake();
                presentTime = _presentTime;
            }
        }
    }



    public void SetDescription(MistakeType mistakeType) 
    {
        Mistake mistake = MistakeLibrary.GetMistake(mistakeType);
        Debug.Log(mistake.Title + " - " + mistake.Description);
    }
}
