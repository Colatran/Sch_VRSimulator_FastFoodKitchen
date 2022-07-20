using System.Collections.Generic;
using UnityEngine;

public class MagnetSnap : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] MagnetSnapPositionGroups positionGroups;

    private void OnValidate()
    {
        if (attachment == null) attachment = GetComponentInParent<Attachment>();
    }



    private List<Attachment> attachmentsMustIgnore = new List<Attachment>();
    private PositionSlot[][] positionSlots;

    private void Awake()
    {
        int i;

        positionSlots = new PositionSlot[positionGroups.Length][];
        for (i = 0; i < positionSlots.Length; i++)
            positionSlots[i] = new PositionSlot[positionGroups.GroupLength(i)];

        for (i = 0; i < positionSlots.Length; i++)
        {
            for (int ii = 0; ii < positionSlots[i].Length; ii++)
                positionSlots[i][ii] = new PositionSlot();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!this.attachment.HasProperOrientation(null)) return;

        Attachment attachment = other.GetComponent<Attachment>();
        if (attachment == null) return;

        if (attachmentsMustIgnore.Contains(attachment)) return;
        attachmentsMustIgnore.Add(attachment);


        //Find Matching Group
        int groupIndex = positionGroups.GetMatchingGroupIndex(attachment);
        if (groupIndex == -1) return;
        PositionGroup group = positionGroups.GetGroup(groupIndex);

        //Try attaching group
        for (int i = 0; i < group.Length; i++)
        {
            PositionSlot slot = positionSlots[groupIndex][i];
            if (slot.Empty)
            {
                slot.Populate(attachment, this.attachment, group.GetPosition(i));
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Attachment attachment = other.GetComponent<Attachment>();
        if (attachment == null) return;

        attachmentsMustIgnore.Remove(attachment);
    }



    private class PositionSlot
    {
        private bool empty = true;
        public bool Empty { get => empty; }

        public void Populate(Attachment child, Attachment parent, PositionRotation pr)
        {
            child.Attach(parent);
            child.OnDetach += OnDetach;
            child.transform.localPosition = pr.Position;
            child.transform.localRotation = pr.Rotation;
            empty = false;
        }

        private void OnDetach()
        {
            empty = true;
        }
    }
}
