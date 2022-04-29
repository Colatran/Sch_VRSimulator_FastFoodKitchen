using UnityEngine;


public enum Condition
{
    MUSTBE_ITEM_PAPER,
    MUSTBE_ITEM_GRIDSMALL,
    MUSTBE_ITEM_GRIDBIG,

    MUSTBE_ITEM_FISHFILLET,
    MUSTBE_ITEM_FISHSTICKS,
    MUSTBE_ITEM_CHIKENFILLET,
    MUSTBE_ITEM_CHIKENNUGGET,
}



[System.Serializable]
public class PositionGroup
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
public class Position
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