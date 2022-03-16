using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Attachment attachment;
    public Attachment Attachment { get => attachment; }

    protected virtual void OnValidate()
    {
        attachment = GetComponent<Attachment>();
    }



    private void OnEnable()
    {
        attachment.OnAttach += OnAttach;
        attachment.OnDetach += OnDetach;
        attachment.OnAddContent += OnAddContent;
        attachment.OnRemoveContent += OnRemoveContent;
    }
    private void OnDisable()
    {
        attachment.OnAttach -= OnAttach;
        attachment.OnDetach -= OnDetach;
        attachment.OnAddContent -= OnAddContent;
        attachment.OnRemoveContent -= OnRemoveContent;
    }

    protected virtual void OnAttach() { }
    protected virtual void OnDetach() { }
    protected virtual void OnAddContent(Attachment child) { }
    protected virtual void OnRemoveContent(Attachment child) { }



    [SerializeField] private ItemType[] flags;
    public bool Is(ItemType flag) 
    {
        foreach (ItemType m_flag in flags) if (m_flag == flag) return true;
        return false;
    }
}
