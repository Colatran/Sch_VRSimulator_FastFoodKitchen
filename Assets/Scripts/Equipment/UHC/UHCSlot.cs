using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UHCSlot : MonoBehaviour
{
    [SerializeField] private Button3D buttonTimer;
    [SerializeField] private UHCSlotTip[] tips;
    [SerializeField] private TMP_Text[] tags;
    [SerializeField] private MeshRenderer[] lights;
    [Header("")]
    [ReadOnly, SerializeField] private ItemType type;
    [ReadOnly, SerializeField] private ItemType boardType;

    public delegate void Action(UHCSlot slot);
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnEmpty;


    public void SetType(ItemType type)
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
        Gizmos.DrawCube(transform.position + new Vector3(0, .02f, -.325f), new Vector3(.15f, .075f, .01f));
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
        if (type == ItemType.NONE) return;
        if (HasBoard) return;

        foreach (UHCSlotTip tip in tips)
            if (tip.Container != container)
                return;

        board = container;
        OnBoardEnter();
    }
    public void OnTipExit(Item_Container container)
    {
        if (type == ItemType.NONE) return;
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
        timerInvalidated = false;
        timeToRemoveItem = timeToRemoveItem_initial;

        if (OnEnter != null)
            OnEnter(this);
    }
    private void OnBoardExit()
    {
        board.SetCookablesHeatSource(HeatSource.NONE);

        if (OnExit != null)
            OnExit(this);
    }

    private void CheckBoard()
    {
        if (!board.Is(boardType))
            GameManager.MakeMistake(MistakeType.UHC_GAVETA_TIPO);

        foreach (Item item in board.Content)
        {
            if (item.Is(ItemType.BOARDINTERIOR))
            {
                continue;
            }

            if (!item.Is(type))
            {
                GameManager.MakeMistake(MistakeType.UHC_GAVETA_PRODUTO);
                return;
            }
        }
    }





    private float timer_timeToPressTimer = 5;
    private float timer_timeIrrevocable = 5;
    private bool timerActivated = false;
    private bool timerInvalidated = false;

    private void OnButtonTimerPressed()
    {
        SetLightsMaterial(GameManager.Asset.Material_UHC_TimerOn);

        if (type == ItemType.NONE) return;
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
        if (timeToRemoveItem > 0)
        {
            timeToRemoveItem -= deltaTime;

            if (timeToRemoveItem <= 0)
            {
                timeToRemoveItem = timeToRemoveItem_initial;

                board.RectifyContent();

                int first = FirstOfType();

                if (first == -1)
                {
                    hasItemsOfType = false;
                    SetLightsMaterial(GameManager.Asset.Material_UHC_TimerOff);

                    if (OnEmpty != null)
                        OnEmpty(this);
                }
                else
                {
                    RemoveItem(first);
                }
            }
        }
    }

    private int FirstOfType()
    {
        List<Item> content = board.Content;

        for (int i = content.Count - 1; i > -1; i--)
            if (content[i].Is(type))
                return i;

        return -1;
    }

    private void RemoveItem(int index)
    {
        Item item = board.Content[index];

        Destroy(item.gameObject);
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