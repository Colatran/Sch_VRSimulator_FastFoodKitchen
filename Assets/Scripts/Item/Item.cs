using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField] protected Attachment attachment;
    public Attachment Attachment { get => attachment; }

    protected virtual void OnValidate()
    {
        attachment = GetComponent<Attachment>();
    }

    private void Start()
    {
        AddAttribute(ItemAttribute.NONE);
    }

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



    private int batch = 0;
    public int Batch { get => batch; set => batch = value; }
    public void SetNewBatch() => batch = GameManager.GetNewBatch();
}
