using UnityEngine;

public class UHCSlotTip : MonoBehaviour
{
    [SerializeField] UHCSlot slot;


    private Item_Container container;
    public Item_Container Container { get => container; }


    private void OnTriggerEnter(Collider other)
    {
        Item_Container itemContainer = other.GetComponent<Item_Container>();
        if (itemContainer == null) return;

        container = itemContainer;
        slot.OnTipEnter(container);
    }

    private void OnTriggerExit(Collider other)
    {
        Item_Container itemContainer = other.GetComponent<Item_Container>();
        if (itemContainer == null) return;

        if (container == itemContainer)
        {
            slot.OnTipExit(container);
            container = null;
        }
    }
}
