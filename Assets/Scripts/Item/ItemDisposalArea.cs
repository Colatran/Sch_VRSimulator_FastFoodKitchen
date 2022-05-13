using UnityEngine;

public class ItemDisposalArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        if (item.Disposable)
            Destroy(item.gameObject);
    }
}
