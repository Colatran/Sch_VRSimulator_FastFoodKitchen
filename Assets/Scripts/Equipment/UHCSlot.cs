using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UHCSlot : MonoBehaviour
{
    [SerializeField] UHCSlotTip[] tips;
    [SerializeField] private ItemType boardType;
    [SerializeField] private ItemType type;
    [SerializeField] private Button3D buttonTimer;
    [Header("")]
    [SerializeField] private TMP_Text[] tags;
    [SerializeField] private MeshRenderer[] lights;
    
    public void SetItemType(ItemType type)
    {
        this.type = type;

        BoardData data = BoardTypeData.GetBoardData(type);

        boardType = data.boardType;

        foreach (TMP_Text text in tags)
            text.text = data.tag;
    }
    
    private void OnDrawGizmos()
    {
        BoardData data = BoardTypeData.GetBoardData(type);
        
        Gizmos.color = data.color;
        Gizmos.DrawCube(transform.position + Vector3.up * .025f, new Vector3(.05f, .05f, .6f));
    }





    private void OnEnable()
    {
        buttonTimer.OnPressed += OnButtonTimerPressed;
    }
    private void OnDisable()
    {
        buttonTimer.OnPressed -= OnButtonTimerPressed;
    }





    private Item_Container board;
    private bool HasBoard { get => board != null; }
    private bool HasNoBoard { get => board == null; }

    public void OnTipEnter(Item_Container container)
    {
        if (HasBoard) return;

        foreach (UHCSlotTip tip in tips)
            if (tip.Container != container)
                return;

        board = container;
        OnBoardEnter();
    }
    public void OnTipExit(Item_Container container)
    {
        if (HasNoBoard) return;

        if (container == board)
        {
            OnBoardExit();
            board = null;
        }
    }

    private void OnBoardEnter()
    {
        CheckBoard();

        board.SetCookablesHeatSource(HeatSource.STOVE);
        board.BatchId = 0;

        timer_timeToPressTimer = 5;

        hasItemsOfType = true;
        timeToRemoveItem = timeToRemoveItem_initial;
    }
    private void OnBoardExit()
    {
        board.SetCookablesHeatSource(HeatSource.NONE);
    }

    private void CheckBoard()
    {
        if (!board.Is(boardType))
            GameManager.MakeMistake(MistakeType.UHC_GAVETA_TIPO);

        foreach (Item item in board.Content)
        {
            if (!item.Is(type))
            {
                GameManager.MakeMistake(MistakeType.UHC_GAVETA_PRODUTO);
                return;
            }
        }
    }



    private void RemoveItem(int index)
    {
        Item item = board.Content[index];

        item.Attachment.Detach();

        Destroy(item.gameObject);
    }
 




    private float timer_timeToPressTimer = 5;
    private float timer_timeIrrevocable = 5;
    private bool timerActivated = false;
    private bool timerInvalidated = false;

    private void OnButtonTimerPressed()
    {
        SetLightsMaterial(GameManager.Asset.Material_UHC_TimerOn);

        if (HasNoBoard) return;

        if(timerActivated)
        {
            if (timer_timeIrrevocable <= 0 && !timerInvalidated)
            {
                GameManager.MakeMistake(MistakeType.UHC_TEMPORIZADOR_INVALIDADO);
                timerInvalidated = true;
            }
        }
        else
        {
            timerActivated = true;
            timer_timeIrrevocable = 5;
        }
    }

    private void SetLightsMaterial(Material instance)
    {
        foreach (MeshRenderer light in lights)
            light.material = instance;
    }

    private void Update_Timer(float deltaTime)
    {
        if (timerActivated)
        {
            if(timer_timeIrrevocable > 0)
                timer_timeIrrevocable -= deltaTime;
        }
        else
        {
            if (timer_timeToPressTimer > 0)
            {
                timer_timeToPressTimer -= deltaTime;

                if (timer_timeToPressTimer <= 0)
                    GameManager.MakeMistake(MistakeType.UHC_TEMPORIZADOR_NAOACIONADO);
            }
        }
    }





    private const float timeToRemoveItem_initial = 20;
    private float timeToRemoveItem = 20;
    private bool hasItemsOfType = false;

    private void Update_RemoveItem(float deltaTime)
    {
        if(timeToRemoveItem > 0)
        {
            timeToRemoveItem -= deltaTime;

            if (timeToRemoveItem <= 0)
            {
                timeToRemoveItem = timeToRemoveItem_initial;

                FindAndDestroyFirstOfType();
            } 
        }
    }
    private void FindAndDestroyFirstOfType()
    {
        List<Item> content = board.Content;

        for (int i = 0; i < content.Count; i++)
        {
            Item item = content[i];
            if (item.Is(type))
            {
                RemoveItem(i);
                return;
            }
        }

        hasItemsOfType = false;

        SetLightsMaterial(GameManager.Asset.Material_UHC_TimerOff);
    }





    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (HasBoard)
        {
            Update_Timer(deltaTime);

            if (hasItemsOfType)
                Update_RemoveItem(deltaTime);
        }
    }
}
