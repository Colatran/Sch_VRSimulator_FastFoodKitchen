using System.Collections.Generic;
using UnityEngine;

public class Item_Container : Item
{
    [SerializeField] ContentCheck contentCheck;
    [SerializeField] MagnetArea magnetArea;

    protected override void OnValidate()
    {
        base.OnValidate();

        if (contentCheck == null) contentCheck = GetComponent<ContentCheck>();
        if (magnetArea == null) magnetArea = GetComponentInChildren<MagnetArea>();

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
    public bool Contains(ItemType type)
    {
        foreach (Item item in content)
            if (item.Is(type)) return true;
        return false;
    }



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


    public void SetCookablesHeatSource(HeatSource source)
    {
        foreach (Item item in content)
            if (item is Item_Cookable)
                (item as Item_Cookable).SetHeatSource(source);
    }



    public void RectifyContent()
    {
        Content.RemoveAll(x => x == null);
        magnetArea.RectifyPoints();
    }
}
