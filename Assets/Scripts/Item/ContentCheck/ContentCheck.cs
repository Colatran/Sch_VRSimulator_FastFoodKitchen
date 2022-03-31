using System.Collections.Generic;
using UnityEngine;

public abstract class ContentCheck : MonoBehaviour
{
    [SerializeField] protected Item_Container container;

    private void OnValidate()
    {
        if (container == null) container = GetComponent<Item_Container>();
    }



    private List<Item> removed = new List<Item>();
    public void ClearRemoved() => removed.Clear();



    public abstract void OnAdd(Item item);

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

    protected bool IsOldBatch(Item item) 
    { 
        return item.Batch != 0 && item.Batch != container.Batch;
    }
}
