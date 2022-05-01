using UnityEngine;

public class FrierOil : MonoBehaviour
{
    [SerializeField] Frier frier;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        frier.OnItemEnter(item);
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        frier.OnItemExit(item);
    }
}
