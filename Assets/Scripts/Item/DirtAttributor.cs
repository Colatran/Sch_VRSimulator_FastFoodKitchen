using UnityEngine;

public class DirtAttributor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;
        if (item.Has(ItemAttribute.DIRT)) return;

        item.AddAttribute(ItemAttribute.DIRT);
    }
}
