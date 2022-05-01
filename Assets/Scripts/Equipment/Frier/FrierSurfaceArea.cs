using System.Collections.Generic;
using UnityEngine;

public class FrierSurfaceArea : MonoBehaviour
{
    private const float _timeToFix = 5;

    private List<Item_Cookable> items = new List<Item_Cookable>();
    private bool canFail = false;
    private float timeToFix = 0;



    private void OnTriggerEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (item.Is(ItemType.FRIED))
        {
            items.Add(item);
            canFail = true;
            timeToFix = _timeToFix;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (item.Is(ItemType.FRIED))
        {
            items.Remove(item);

            if (items.Count == 0)
                canFail = false;
        }
    }


    private void Update()
    {
        if(canFail)
        {
            if(timeToFix > 0)
            {
                timeToFix -= Time.deltaTime;
                if(timeToFix <= 0)
                {
                    canFail = false;
                    GameManager.MakeMistake(MistakeType.FRITADEIRA_PRODUTO_NAOSUBMERSO);
                }
            }
        }
    }
}
