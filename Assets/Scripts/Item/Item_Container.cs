using System.Collections.Generic;
using UnityEngine;

public class Item_Container : Item
{
    [SerializeField] ContentCheck contentCheck;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (contentCheck == null) contentCheck = GetComponent<ContentCheck>();
    }



    private void OnEnable()
    {
        attachment.OnAddContent += OnAddContent;
        attachment.OnRemoveContent += OnRemoveContent;
    }
    private void OnDisable()
    {
        attachment.OnAddContent -= OnAddContent;
        attachment.OnRemoveContent -= OnRemoveContent;
    }





    private List<Item> content = new List<Item>();
    public List<Item> Content { get => content; }
    public Item[] FindAll(ItemType type) => content.FindAll(x => x.Is(type)).ToArray();

    private void OnAddContent(Attachment child)
    {
        Item item = child.GetComponent<Item>();
        if (item == null) return;

        content.Add(item);

        contentCheck.OnAdd(item);
    }

    private void OnRemoveContent(Attachment child)
    {
        Item item = child.GetComponent<Item>();
        if (item == null) return;

        content.Add(item);

        contentCheck.OnRemove(item);
    }
}
