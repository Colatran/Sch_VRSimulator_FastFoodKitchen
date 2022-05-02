using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour
{
    [SerializeField] CookingFactors cookingFactors;
    [SerializeField] FrierTimer[] frierTimers;

    public CookingFactors CookingFactors { get => cookingFactors; }

    private List<FrierBasket> baskets = new List<FrierBasket>();
    private ItemType contentType = ItemType.NONE;


    public void OnItemEnter(Item item)
    {
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
            if (item.Is(ItemType.EQUIPMENT_FRYERBASKET))
            {
                FrierBasket basket = item.GetComponent<FrierBasket>();

                baskets.Add(basket);

                //Defenir o batch
                //Manda ativar o timer
                basket.OnEnterOil();
            }
            else
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_ITEMERRADO_EQUIPAMENTO);
            }

        }
    }

    public void OnItemExit(Item item)
    {
        //POR O OLEO A ESCORRER

        if (item is Item_Cookable)
        {
            Item_Cookable itemCookable = item as Item_Cookable;
            itemCookable.SetHeatSource(HeatSource.NONE);
        }

        else if (item.Is(ItemType.EQUIPMENT_FRYERBASKET))
        {
            FrierBasket basket = item.GetComponent<FrierBasket>();

            baskets.Remove(basket);

            basket.OnExitOil();
        }
    }



    private void OnEnable()
    {
        foreach (FrierTimer timer in frierTimers) 
            timer.OnActivated += ActivateTimer;
    }
    private void OnDisable()
    {
        foreach (FrierTimer timer in frierTimers)
            timer.OnActivated -= ActivateTimer;
    }

    private void ActivateTimer()
    {
        foreach(FrierBasket basket in baskets)
        {
            if (basket.ActivateTimer()) return;
        }
    }
}
