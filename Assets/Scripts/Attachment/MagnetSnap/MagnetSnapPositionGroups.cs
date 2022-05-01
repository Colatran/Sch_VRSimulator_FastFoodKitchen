using UnityEngine;

[CreateAssetMenu(fileName = "PositionGroups", menuName = "NewObject/MagnetSnapPositionGroups")]
public class MagnetSnapPositionGroups : ScriptableObject
{
    [SerializeField] PositionGroup[] groups;


    private bool ChecksCondition(Condition condition, Attachment attachment)
    {
        switch (condition)
        {
            case Condition.MUSTBE_ITEM_PAPER:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_PAPER))
                            return true;
                    }
                    break;
                }
            case Condition.MUSTBE_ITEM_GRIDSMALL:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_GRIDSMALL))
                            return true;
                    }
                    break;
                }
            case Condition.MUSTBE_ITEM_GRIDBIG:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_GRIDBIG))
                            return true;
                    }
                    break;
                }

            case Condition.MUSTBE_ITEM_FISHFILLET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_FISH_FILLET))
                            return true;
                    }
                    break;
                }
            case Condition.MUSTBE_ITEM_FISHSTICKS:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_FISH_STICKS))
                            return true;
                    }
                    break;
                }
            case Condition.MUSTBE_ITEM_CHIKENFILLET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_CHIKEN_FILLET))
                            return true;
                    }
                    break;
                }
            case Condition.MUSTBE_ITEM_CHIKENNUGGET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_CHIKEN_NUGGET))
                            return true;
                    }
                    break;
                }
        }
        return false;
    }

    public void TryAttach(Attachment child, Attachment parent)
    {
        foreach (PositionGroup group in groups)
        {
            if (ChecksCondition(group.Condition, child))
            {
                group.TryAttach(child, parent);
                return;
            }
        }
    }
}
