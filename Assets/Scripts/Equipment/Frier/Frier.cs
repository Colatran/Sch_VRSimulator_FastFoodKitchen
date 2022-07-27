using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour
{
    [SerializeField] TriggerArea oilTriggerArea;
    [SerializeField] CookingFactors cookingFactors;
    [SerializeField] FrierTimer[] frierTimers;
    [SerializeField] GameObjectPool oilPool;
    [SerializeField] GameObjectPool greasePool;

    private void OnValidate()
    {
        foreach (PoolObject pObject in oilPool.Objects)
            (pObject as PoolObject_OilDrop).SetGreasePool(greasePool);
    }



    private List<FrierBasket> baskets = new List<FrierBasket>();
    public void AddBasket(FrierBasket basket) => baskets.Add(basket);
    public void RemoveBasket(FrierBasket basket) => baskets.Remove(basket);

    private ItemType contentType = ItemType.NONE;
    private float oilDropMistakeTime = 0;

    public CookingFactors CookingFactors { get => cookingFactors; }
    public int ActiveGreaseCount { get => greasePool.GetActiveObjectCount(); }



    private void OnEnable()
    {
        oilTriggerArea.OnEnter += OnItemEnter;
        oilTriggerArea.OnExit += OnItemExit;

        foreach (PoolObject pObject in oilPool.Objects)
            (pObject as PoolObject_OilDrop).OnDrop += OnOilDrop;
    }
    private void OnDisable()
    {
        oilTriggerArea.OnEnter -= OnItemEnter;
        oilTriggerArea.OnExit -= OnItemExit;

        foreach (PoolObject pObject in oilPool.Objects)
            (pObject as PoolObject_OilDrop).OnDrop -= OnOilDrop;
    }

    private void OnItemEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        if (item is Item_Cookable)
        {
            Item_Cookable itemCookable = item as Item_Cookable;
            itemCookable.SetCookingFactors(cookingFactors.MaxTemperatureFactor, cookingFactors.CookingTimeFactor);
            itemCookable.SetHeatSource(HeatSource.COOKER);

            bool outside = true;
            foreach (FrierBasket basket in baskets)
            {
                if (basket.Contains(itemCookable))
                {
                    outside = false;
                    break;
                }
            }
            if (outside)
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_PRODUTO_DIRETAMENTENACUBA);
            }
            else
            {
                if (item.Is(ItemType.BEEF))
                {
                    GameManager.MakeMistake(MistakeType.FRITADEIRA_OLEO_BIFE);
                }
                else if (item.Is(ItemType.FRIED))
                {
                    if (item.Has(ItemAttribute.DIRT))
                        GameManager.MakeMistake(MistakeType.PRODUTO_CONTAMINADO);

                    //Vê se é do mesmo tipo
                    if (contentType == ItemType.NONE)
                    {
                        if (item.Is(ItemType.FRIED_FISH)) contentType = ItemType.FRIED_FISH;
                        else if (item.Is(ItemType.FRIED_CHIKEN)) contentType = ItemType.FRIED_CHIKEN;
                    }
                    else if (!item.Is(contentType))
                    {
                        GameManager.MakeMistake(MistakeType.FRITADEIRA_OLEO_PRODUTOMISTURADO_TIPO);
                        contentType = ItemType.NONE;
                    }
                }
            }
        }

        else if (item.Is(ItemType.EQUIPMENT))
        {
            if (!item.Is(ItemType.EQUIPMENT_FRYERBASKET))
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_ITEMERRADO_EQUIPAMENTO);
            }
        }
    }
    private void OnItemExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        //Por o oleo a escorrer
        GameObject oilDropObject = oilPool.GetObject();
        PoolObject_OilDrop oilDrop = oilDropObject.GetComponent<PoolObject_OilDrop>();
        oilDrop.SetHost(item.transform);
        oilDropObject.SetActive(true);

        if (item is Item_Cookable)
        {
            Item_Cookable itemCookable = item as Item_Cookable;
            itemCookable.SetHeatSource(HeatSource.NONE);
        }
    }


    private void OnOilDrop()
    {
        if (oilDropMistakeTime == 0)
        {
            GameManager.MakeMistake(MistakeType.FRITADEIRA_OLEO_NAOESCORREU);

            oilDropMistakeTime = 5;
        }
    }




    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (oilDropMistakeTime > 0)
        {
            oilDropMistakeTime -= deltaTime;

            if (oilDropMistakeTime <= 0)
            {
                oilDropMistakeTime = 0;
            }
        }
    }
}
