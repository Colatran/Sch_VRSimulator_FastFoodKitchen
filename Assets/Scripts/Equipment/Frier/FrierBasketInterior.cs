using UnityEngine;

public class FrierBasketInterior : MonoBehaviour
{
    [SerializeField] FrierBasket basket;


    private void OnTriggerEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if(basket.Contains(item)) return;
        basket.AddItem(item);
    }

    private void OnTriggerExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        basket.RemoveItem(item);
    }
}
