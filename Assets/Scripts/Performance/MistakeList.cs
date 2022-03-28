using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MistakeList : MonoBehaviour
{
    [SerializeField] PerformanceManager performanceManager;
    [SerializeField] GameObjectPool buttonPool;
    [SerializeField] Transform buttonContainer;
    [Header("Secription")]
    [SerializeField] TMP_Text text_title;
    [SerializeField] TMP_Text text_description;
    [SerializeField] TMP_Text text_hint;



    private void Awake()
    {
        performanceManager.OnAddMistake += AddMistake;
    }
    private void OnDestroy()
    {
        GameManager.PerformanceManager.OnAddMistake -= AddMistake;
    }



    private const float _presentTime = .0625f;
    private float presentTime;

    private List<MistakeType> mistakesToPresent = new List<MistakeType>();
    private void AddMistake(MistakeType type)
    {
        mistakesToPresent.Add(type);
    }

    private UIMistakeButton currentButton;
    private void SetCurrentButton(UIMistakeButton button)
    {
        if (currentButton != null)
            currentButton.SetUnselected();

        currentButton = button;

        if (currentButton != null)
        {
            currentButton.SetSelected();
            SetDescription(currentButton.MistakeType);
        }
    }

    private bool isOpen = false;

    public delegate void Action();
    public event Action OnButtonPressed;



    public void Open()
    {
        isOpen = true;
    }
    public void Close()
    {
        isOpen = false;

        buttonPool.DiactiveAllObjects();

        mistakesToPresent.Clear();

        SetCurrentButton(null);
    }


    private void PresentNextButton()
    {
        if (mistakesToPresent.Count == 0) return;

        GameObject buttonObject = buttonPool.GetObject();
        buttonObject.transform.SetParent(buttonContainer, false);
        buttonObject.SetActive(true);

        UIMistakeButton button = buttonObject.GetComponent<UIMistakeButton>();
        button.SetUp(this, mistakesToPresent[0]);

        if (currentButton == null)
        {
            SetCurrentButton(button);
        }

        mistakesToPresent.RemoveAt(0);

        presentTime = _presentTime;
    }

    private void ButtonListing()
    {
        if (mistakesToPresent.Count > 0)
        {
            presentTime -= Time.deltaTime;

            if (presentTime < 0)
                PresentNextButton();
        }
    }



    public void PressButton(UIMistakeButton button)
    {
        SetCurrentButton(button);

        if (OnButtonPressed != null)
            OnButtonPressed();
    }

    private void SetDescription(MistakeType mistakeType)
    {
        Mistake mistake = MistakeLibrary.GetMistake(mistakeType);

        text_title.text = mistake.Title;
        text_description.text = mistake.Description;
        text_hint.text = mistake.Hint;
    }



    private void Update()
    {
        if (isOpen)
        {
            ButtonListing();
        }
    }
}
