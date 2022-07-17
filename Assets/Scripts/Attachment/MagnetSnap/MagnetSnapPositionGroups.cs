using UnityEngine;

[CreateAssetMenu(fileName = "PositionGroups", menuName = "NewObject/MagnetSnapPositionGroups")]
public class MagnetSnapPositionGroups : ScriptableObject
{
    [SerializeField] PositionGroup[] groups;

    public int Length { get => groups.Length; }
    public int GroupLength(int i) => groups[i].Length;
    public PositionGroup GetGroup(int i) => groups[i];


    private bool ChecksCondition(PositionGroupCondition condition, Attachment attachment)
    {
        switch (condition)
        {
            case PositionGroupCondition.MUSTBE_ITEM_PAPER:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_PAPER))
                            return true;
                    }
                    break;
                }
            case PositionGroupCondition.MUSTBE_ITEM_GRIDSMALL:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_GRIDSMALL))
                            return true;
                    }
                    break;
                }
            case PositionGroupCondition.MUSTBE_ITEM_GRIDBIG:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BOARDINTERIOR_GRIDBIG))
                            return true;
                    }
                    break;
                }

            case PositionGroupCondition.MUSTBE_ITEM_FISHFILLET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_FISH_FILLET))
                            return true;
                    }
                    break;
                }
            case PositionGroupCondition.MUSTBE_ITEM_FISHSTICKS:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_FISH_STICKS))
                            return true;
                    }
                    break;
                }
            case PositionGroupCondition.MUSTBE_ITEM_CHIKENFILLET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_CHIKEN_FILLET))
                            return true;
                    }
                    break;
                }
            case PositionGroupCondition.MUSTBE_ITEM_CHIKENNUGGET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.FRIED_CHIKEN_NUGGET))
                            return true;
                    }
                    break;
                }

            case PositionGroupCondition.MUSTBE_ITEM_FRYERBASKET:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.EQUIPMENT_FRYERBASKET))
                            return true;
                    }
                    break;
                }

            case PositionGroupCondition.MUSTBE_BUNLOWER:
                {
                    Item item = attachment.GetComponent<Item>();
                    if (item != null)
                    {
                        if (item.Is(ItemType.BREAD_LOWER))
                            return true;
                    }
                    break;
                }
        }
        return false;
    }

    public int GetMatchingGroupIndex(Attachment child)
    {
        for (int i = 0; i < groups.Length; i++)
            if (ChecksCondition(groups[i].Condition, child))
                return i;

        return -1;
    }
}
