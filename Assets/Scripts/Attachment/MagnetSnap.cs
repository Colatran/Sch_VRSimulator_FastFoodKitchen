using System.Collections.Generic;
using UnityEngine;

public class MagnetSnap : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] PositionGroup[] positionGroups;

    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponentInParent<Attachment>();
    }



    private List<Attachment> attachmentsMustIgnore = new List<Attachment>();

    private void OnTriggerEnter(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();
        if (attachment == null) return;

        if (attachmentsMustIgnore.Contains(attachment))
        {
            attachmentsMustIgnore.Remove(attachment);
            return;
        }

        foreach (PositionGroup group in positionGroups)
        {
            if (ChecksCondition(group.Condition, attachment))
            {
                group.TryAttach(attachment, this.attachment);
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();

        attachmentsMustIgnore.Remove(attachment);
    }



    public enum Condition
    {
        MUSTBE_ITEM_PAPER,
        MUSTBE_ITEM_GRIDSMALL,
        MUSTBE_ITEM_GRIDBIG,
    }

    public bool ChecksCondition(Condition condition, Attachment attachment)
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
        }
        return false;
    }


    [System.Serializable]
    private class PositionGroup
    {
        [SerializeField] Condition condition;
        [SerializeField] Position[] positions;

        public Condition Condition { get => condition; }

        public void TryAttach(Attachment child, Attachment parent)
        {
            foreach (Position position in positions)
                if (position.Empty)
                {
                    position.Populate(child, parent);
                    return;
                }
        }
    }

    [System.Serializable]
    private class Position
    {
        [SerializeField] Vector3 position;
        [SerializeField] Quaternion rotation;

        private bool empty = true;
        public bool Empty { get => empty; }

        public void Populate(Attachment child, Attachment parent)
        {
            child.Attach(parent);
            child.OnDetach += OnDetach;
            child.transform.localPosition = position;
            child.transform.localRotation = rotation;
            empty = false;
        }

        private void OnDetach()
        {
            empty = true;
        }
    }
}
