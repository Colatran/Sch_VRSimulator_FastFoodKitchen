using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField] protected Attachment attachment;
    public Attachment Attachment { get => attachment; }

    [SerializeField] private ItemType[] typeFlags;
    public bool Is(ItemType flag)
    {
        foreach (ItemType m_flag in typeFlags) if (m_flag == flag) return true;
        return false;
    }

    [SerializeField] AttributeCheck attributeCheck;
    private List<ItemAttribute> attributeFlags = new List<ItemAttribute>();
    public List<ItemAttribute> AttributeFlags { get => attributeFlags; }
    public bool Has(ItemAttribute flag)
    {
        foreach (ItemAttribute m_flag in attributeFlags) if (m_flag == flag) return true;
        return false;
    }
    public void AddAttribute(ItemAttribute flag)
    {
        attributeFlags.Add(flag);

        if(attributeCheck != null)
            attributeCheck.OnAddAttribute(flag);
    }


    protected virtual void OnValidate()
    {
        if(attachment == null) attachment = GetComponent<Attachment>();

        if (attributeCheck == null) attributeCheck = GetComponent<AttributeCheck>();
    }


    public int BatchId { get; set; }
}
