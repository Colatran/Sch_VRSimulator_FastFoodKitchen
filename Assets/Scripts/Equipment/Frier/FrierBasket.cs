using System.Collections.Generic;
using UnityEngine;

public class FrierBasket : MonoBehaviour
{
    [SerializeField] BatchHandler batchHandler;
    [SerializeField] TriggerArea interiorTrigger;

    private const float _timeToPressTimer = 5;
    private float timeToPressTimer = _timeToPressTimer;
    private bool canFailPressTimer = false;
    private bool timerActivated = false;
    private ItemType contentType = ItemType.NONE;
    private List<Item_Cookable> content = new List<Item_Cookable>();

    public bool Contains(Item_Cookable item) => content.Contains(item);



    private void AddItem(Item_Cookable item)
    {
        if (Contains(item)) return;

        content.Add(item);
        CheckAddedItem(item);
    }
    private void RemoveItem(Item_Cookable item)
    {
        content.Remove(item);
        CheckRemovedItem(item);
    }

    private void CheckAddedItem(Item item)
    {
        if (item.Is(ItemType.FRIED))
        {
            if (contentType == ItemType.NONE)
            {
                if (item.Is(ItemType.FRIED_FISH_FILLET)) contentType = ItemType.FRIED_FISH_FILLET;
                else if (item.Is(ItemType.FRIED_FISH_STICKS)) contentType = ItemType.FRIED_FISH_STICKS;
                else if (item.Is(ItemType.FRIED_CHIKEN_FILLET)) contentType = ItemType.FRIED_CHIKEN_FILLET;
                else if (item.Is(ItemType.FRIED_CHIKEN_NUGGET)) contentType = ItemType.FRIED_CHIKEN_NUGGET;
            }

            else if (!item.Is(contentType))
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_CESTO_PRODUTOMISTURADO_TIPO);
            }

            if (item.BatchId != 0)
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_CESTO_PRODUTOMISTURADO_LOTE);
            }
        }

        else if (item.Is(ItemType.BEEF))
        {
            GameManager.MakeMistake(MistakeType.FRITADEIRA_CESTO_BIFE);
        }
    }
    private void CheckRemovedItem(Item item)
    {
        if (item.Is(contentType))
        {
            foreach (Item i in content)
            {
                if (i.Is(contentType))
                    return;
            }

            contentType = ItemType.NONE;
        }
    }



    private void OnEnable()
    {
        interiorTrigger.OnEnter += OnEnterInteriorArea;
        interiorTrigger.OnExit += OnExitInteriorArea;
    }
    private void OnDisable()
    {
        interiorTrigger.OnEnter -= OnEnterInteriorArea;
        interiorTrigger.OnExit -= OnExitInteriorArea;
    }

    private void OnEnterInteriorArea(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (Contains(item)) return;
        AddItem(item);
    }
    private void OnExitInteriorArea(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        RemoveItem(item);
    }



    public void OnEnterOil()
    { 
        //Manda ativar o timer
        canFailPressTimer = true;
        timeToPressTimer = _timeToPressTimer;

        //Defenir o batch
        batchHandler.NextBatch();
        foreach (Item_Cookable item in content)
            batchHandler.AddItem(item);
    }
    public void OnExitOil()
    {
        canFailPressTimer = false;
    }

    public bool ActivateTimer()
    {
        if (timerActivated) return false;

        canFailPressTimer = false;
        return true;
    }



    private void Update()
    {
        if (canFailPressTimer)
        {
            if (timeToPressTimer > 0)
            {
                timeToPressTimer -= Time.deltaTime;
                if (timeToPressTimer <= 0)
                {
                    canFailPressTimer = false;
                    GameManager.MakeMistake(MistakeType.FRITADEIRA_TEMPORIZADOR_NAOATIVOU);
                }
            }
        }
    }
}
