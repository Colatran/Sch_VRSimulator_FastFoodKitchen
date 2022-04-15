using System.Collections.Generic;
using UnityEngine;

public class UHC : MonoBehaviour
{
    [SerializeField] List<UHCSlotSequence> sequences = new List<UHCSlotSequence>();

    private void OnValidate()
    {
        foreach (UHCSlotSequence sequence in sequences)
            sequence.OnValidate();
    }

    private void OnEnable()
    {
        foreach (UHCSlotSequence sequence in sequences)
            sequence.OnEnable();
    }
    private void OnDisable()
    {
        foreach (UHCSlotSequence sequence in sequences)
            sequence.OnDisable();
    }



    [System.Serializable]
    private class UHCSlotSequence
    {
        [SerializeField] private ItemType type;
        [SerializeField] private UHCSlot[] slots;

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
