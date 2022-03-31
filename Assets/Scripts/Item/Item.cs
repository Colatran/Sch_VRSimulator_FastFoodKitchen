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



    [SerializeField] private ItemType[] typeFlags;
    public bool Is(ItemType flag)
    {
        foreach (ItemType m_flag in typeFlags) if (m_flag == flag) return true;
        return false;
    }



    private List<ItemAttribute> attributeFlags = new List<ItemAttribute>();
    public bool Has(ItemAttribute flag)
    {
        foreach (ItemAttribute m_flag in attributeFlags) if (m_flag == flag) return true;
        return false;
    }



    private int batch = 0;
    public int Batch { get => batch; set => batch = value; }
    public void SetNewBatch() => batch = GameManager.GetNewBatch();


    /*private void OnEnable()
    {
        attachment.OnAttach += OnAttach;
        attachment.OnDetach += OnDetach;
        //attachment.OnAddContent += OnAddContent;
        //attachment.OnRemoveContent += OnRemoveContent;
    }
    private void OnDisable()
    {
        attachment.OnAttach -= OnAttach;
        attachment.OnDetach -= OnDetach;
        //attachment.OnAddContent -= OnAddContent;
        //attachment.OnRemoveContent -= OnRemoveContent;
    }

    //protected virtual void OnAttach() { }
    //protected virtual void OnDetach() { }
    //protected virtual void OnAddContent(Attachment child) { }
    //protected virtual void OnRemoveContent(Attachment child) { }*/
}
