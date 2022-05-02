using UnityEngine;

[System.Serializable]
public struct PositionGroup
{
    [SerializeField] PositionGroupCondition condition;
    [SerializeField] PositionRotation[] positions;

    public PositionGroupCondition Condition { get => condition; }
    public int Length { get => positions.Length; }
    public PositionRotation GetPosition(int i) => positions[i];
}