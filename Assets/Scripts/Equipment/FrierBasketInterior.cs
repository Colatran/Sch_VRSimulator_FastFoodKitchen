using System.Collections.Generic;
using UnityEngine;

public class FrierBasketInterior : MonoBehaviour
{
    private List<Item_Cookable> content = new List<Item_Cookable>();
    private ItemType contentType = ItemType.NONE;

    public List<Item_Cookable> Content { get => content; }



    private void OnTriggerEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        content.Add(item);
        CheckAddedItem(item);
    }

    private void OnTriggerExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        content.Remove(item);
        CheckRemovedItem(item);
    }



    private void CheckAddedItem(Item item)
    {
        if (item.Is(ItemType.FRIED))
        {
            if(contentType == ItemType.NONE)
            {
                if (item.Is(ItemType.FRIED_FISH_FILLET)) contentType = ItemType.FRIED_FISH_FILLET;
                else if (item.Is(ItemType.FRIED_FISH_STICKS)) contentType = ItemType.FRIED_FISH_STICKS;
                else if(item.Is(ItemType.FRIED_CHIKEN_FILLET)) contentType = ItemType.FRIED_CHIKEN_FILLET;
                else if(item.Is(ItemType.FRIED_CHIKEN_NUGGET)) contentType = ItemType.FRIED_CHIKEN_NUGGET;
            }

            else if(!item.Is(contentType))
            {
                GameManager.MakeMistake(MistakeType.FRITADEIRA_CESTO_PRODUTOMISTURADO);
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
}
