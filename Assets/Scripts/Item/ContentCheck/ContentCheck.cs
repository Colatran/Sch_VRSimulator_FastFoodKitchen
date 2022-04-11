using System.Collections.Generic;
using UnityEngine;

public class ContentCheck : MonoBehaviour
{
    [SerializeField] protected Item_Container container;

    private void OnValidate()
    {
        if (container == null) container = GetComponent<Item_Container>();
    }



    private List<Item> removed = new List<Item>();
    public void ClearRemoved() => removed.Clear();



    public virtual void OnAdd(Item item)
    {
        if (container.BatchId == 0)
            container.BatchId = item.BatchId;
    }

    public void OnRemove(Item item)
    {
        removed.Add(item);
    }



    protected bool MustSkipItem(Item item)
    {
        if (removed.Contains(item))
        {
            removed.Remove(item);
            return true;
        }

        return false;
    }



    protected bool IsOldBatch(Item item) => item.BatchId != 0 && item.BatchId != container.BatchId;
}
