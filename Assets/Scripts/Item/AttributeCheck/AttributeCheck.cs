using System.Collections.Generic;
using UnityEngine;



public abstract class AttributeCheck : MonoBehaviour
{
    protected List<ItemAttribute> attributes;

    protected virtual void Start()
    {
        attributes = GetComponent<Item>().AttributeFlags;
    }

    public abstract void OnAddAttribute(ItemAttribute flag);
}

