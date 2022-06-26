using System.Collections.Generic;
using UnityEngine;

public class UHC : MonoBehaviour
{
    [SerializeField] List<UHCSlotSequence> sequences = new List<UHCSlotSequence>();
    [SerializeField, ReadOnly] List<UHCSlot> slots = new List<UHCSlot>();
    [SerializeField] Orderer_Cook cookOrderer;

    private void OnValidate()
    {
        slots.Clear();
        foreach (UHCSlotSequence sequence in sequences)
        {
            sequence.OnValidate();
            slots.AddRange(sequence.Slots);
        }
    }



    private void OnEnable()
    {
        foreach (UHCSlotSequence sequence in sequences)
            sequence.OnEnable();

        foreach (UHCSlot slot in slots)
            slot.OnServe += Slot_OnServe;
    }
    private void OnDisable()
    {
        foreach (UHCSlotSequence sequence in sequences)
            sequence.OnDisable();
    }



    private void Slot_OnServe(ItemType itemType, int count)
    {
        cookOrderer.ServeOrder(itemType, count);
    }



    [System.Serializable]
    private class UHCSlotSequence
    {
        [SerializeField] private ItemType type;
        [SerializeField] private UHCSlot[] slots;

        public UHCSlot[] Slots { get => slots; }

        public void OnValidate() 
        {
            foreach (UHCSlot slot in slots)
                if(slot != null)
                    slot.SetType(type);
        }

        public void OnEnable() 
        {
            foreach (UHCSlot slot in slots)
            {
                slot.OnEnter += Slot_OnEnter;
                slot.OnExit += Slot_OnExit;
            }
        }
        public void OnDisable() 
        {
            foreach (UHCSlot slot in slots)
            {
                slot.OnEnter -= Slot_OnEnter;
                slot.OnExit -= Slot_OnExit;
            }
        }



        private int currentSlot = 0;
        private int SlotCount { get => slots.Length; }

        private void NextSlot()
        {
            currentSlot++;

            if (currentSlot == SlotCount)
                currentSlot = 0;
        }
        private void PreviousSlot()
        {
            currentSlot--;

            if (currentSlot == -1)
                currentSlot = SlotCount - 1;
        }

        private void Slot_OnEnter(UHCSlot slot)
        {
            if (slots[currentSlot] == slot)
            {
                NextSlot();
            }
            else 
            {
                GameManager.MakeMistake(MistakeType.UHC_ORDEMERRADA);
                currentSlot = GetIndexOf(slot) + 1;
            }
        }
        private void Slot_OnExit(UHCSlot slot)
        {
            PreviousSlot();
        }


        private int GetIndexOf(UHCSlot slot)
        {
            for (int i = 0; i < SlotCount; i++)
            {
                if (slot == slots[i])
                    return i;
            }
            return 0;
        }
    }
}
