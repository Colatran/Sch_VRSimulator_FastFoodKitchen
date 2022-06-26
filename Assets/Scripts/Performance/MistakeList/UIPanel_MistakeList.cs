using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPanel_MistakeList : MonoBehaviour
{
    [SerializeField] GameObjectPool buttonPool;
    [SerializeField] Transform buttonListContainer;
    [SerializeField] Animator animator;
    [Header("Description")]
    [SerializeField] TMP_Text text_title;
    [SerializeField] TMP_Text text_description;
    [SerializeField] TMP_Text text_hint;
    [Header("Listing")]
    [SerializeField] GameObject sectorPanel;
    [SerializeField] TMP_Text text_currentSector;
    [SerializeField] TMP_Text text_sectorCount;
    [SerializeField] GameObject nextSectorButton;
    [SerializeField] GameObject previousSectorButton;


    private void OnEnable()
    {
        GameManager.PerformanceManager.OnAddMistake += AddMistake;
    }
    private void OnDisable()
    {
        GameManager.PerformanceManager.OnAddMistake -= AddMistake;
    }

    public void Open()
    {
        isOpen = true;

        currentSector = 0;
        StartSectorListing();

        SetOpenDescription(false);
        SetOpenHint(false);
        animator.SetTrigger("Reset");
    }
    public void Close()
    {
        isOpen = false;

        buttonPool.DisableAllObjects();

        mistakes.Clear();
    }





    private List<MistakeType> mistakes = new List<MistakeType>();
    private void AddMistake(MistakeType type) 
    {
        mistakes.Add(type);

        SetSectorPanel();
    }





    private bool isOpen = false;

    private int sectorCount = 0;
    private int currentSector = 0;
    private int baseIndex = 0;
    private int currentIndex = 0;
    private int finalIndex = 0;
    private bool inFirstSector { get => currentSector == 0; }
    private bool inLastSector { get => currentSector + 1 == sectorCount; }

    private const float _presentTime = .0625f;
    private float presentTime;


    public void PressNextSector() 
    {
        if (inLastSector) return;
        currentSector++;

        CheckSectorBoundries();

        StartSectorListing();
    }
    public void PressPreviousSector()
    {
        if (currentSector == 0) return;
        currentSector--;

        CheckSectorBoundries();

        StartSectorListing();
    }


    private void CheckSectorBoundries()
    {
        if (inLastSector)
        {
            previousSectorButton.SetActive(true);
            nextSectorButton.SetActive(false);
        }
        else if(inFirstSector)
        {
            previousSectorButton.SetActive(false);
            nextSectorButton.SetActive(true);
        }
        else
        {
            previousSectorButton.SetActive(true);
            nextSectorButton.SetActive(true);
        }
    }

    private void SetSectorPanel()
    {
        int mistakeCount = mistakes.Count;

        sectorCount = mistakeCount / 10;
        if (mistakeCount % 10 > 0) sectorCount++;

        if (sectorCount > 1)
        {
            sectorPanel.SetActive(true);
            text_sectorCount.text = sectorCount + "";
        }
        else
        {
            sectorPanel.SetActive(false);
            text_currentSector.text = 1 + "";
        }

        CheckSectorBoundries();
    }


    private void StartSectorListing()
    {
        buttonPool.DisableAllObjects();
        CurrentButton = null;

        baseIndex = currentSector * 10;
        currentIndex = 0;
        finalIndex = baseIndex;

        text_currentSector.text = currentSector + 1 + "";
    }

    private void PresentButton(int mistakeIndex)
    {
        GameObject buttonObject = buttonPool.GetObject();
        buttonObject.transform.SetParent(buttonListContainer, false);
        buttonObject.SetActive(true);

        PoolObject_UIMistakeButton button = buttonObject.GetComponent<PoolObject_UIMistakeButton>();
        button.SetUp(this, mistakes[mistakeIndex]);

        if (CurrentButton == null)
            CurrentButton = button;
    }

    private void Update_SectorListing()
    {
        if (finalIndex == mistakes.Count) return;
        if (currentIndex > 9) return;

        presentTime -= Time.deltaTime;

        if (presentTime < 0)
        {
            PresentButton(finalIndex);

            presentTime = _presentTime;
            currentIndex++;
            finalIndex = baseIndex + currentIndex;
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            Update_SectorListing();
        }
    }





    private PoolObject_UIMistakeButton currentButton;
    private PoolObject_UIMistakeButton CurrentButton
    {
        get => currentButton;
        set
        {
            if (currentButton != null)
                currentButton.SetUnselected();

            currentButton = value;

            if (currentButton != null)
            {
                currentButton.SetSelected();
                SetDescription(currentButton.MistakeType);
            }
        }
    }


    private void SetDescription(MistakeType mistakeType)
    {
        Mistake mistake = MistakeLibrary.GetMistake(mistakeType);

        text_title.text = mistake.Title;
        text_description.text = mistake.Description;
        text_hint.text = mistake.Hint;
    }

    public void PressMistakeButton(PoolObject_UIMistakeButton button)
    {
        CurrentButton = button;
    }





    [Header("")]
    [SerializeField] RectTransform spoilerArrow_Description;
    [SerializeField] RectTransform spoilerArrow_Hint;

    private bool openDescription = false;
    private bool openHint = false;

    public void PressDescriptionSpoiler()
    {
        SetOpenDescription(!openDescription);

        SetOpenHint(false);
    }
    public void PressHintSpoiler()
    {
        if (!openDescription) return;

        SetOpenHint(!openHint);
    }

    private void SetOpenDescription(bool open)
    {
        openDescription = open;

        if (openDescription)
            spoilerArrow_Description.localScale = new Vector3(2, -1, 1);
        else
            spoilerArrow_Description.localScale = new Vector3(2, 1, 1);

        animator.SetBool("Description", openDescription);
    }
    private void SetOpenHint(bool open)
    {
        openHint = open;

        if (openHint)
            spoilerArrow_Hint.localScale = new Vector3(2, -1, 1);
        else
            spoilerArrow_Hint.localScale = new Vector3(2, 1, 1);

        animator.SetBool("Hint", openHint);
    }
}
