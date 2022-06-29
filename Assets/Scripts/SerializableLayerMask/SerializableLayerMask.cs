using UnityEngine;

[CreateAssetMenu(fileName = "SerializableLayerMask", menuName = "NewObject/SerializableLayerMask")]
public class SerializableLayerMask : ScriptableObject
{
    [SerializeField] LayerMask layerMask;
    public LayerMask value { get => layerMask; }
}
