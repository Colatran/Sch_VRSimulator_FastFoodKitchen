using UnityEngine;

[System.Serializable]
public struct PositionRotation
{
    [SerializeField] Vector3 position;
    [SerializeField] Quaternion rotation;

    public Vector3 Position { get => position; }
    public Quaternion Rotation { get => rotation; }
}