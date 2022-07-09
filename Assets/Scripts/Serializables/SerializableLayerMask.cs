using UnityEngine;

[CreateAssetMenu(fileName = "LayerMask", menuName = "NewObject/Serializable/LayerMask")]
public class SerializableLayerMask : ScriptableObject
{
    [SerializeField] LayerMask value;
    public LayerMask Value { get => value; }
}
