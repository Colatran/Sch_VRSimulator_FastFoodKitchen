using UnityEngine;

[CreateAssetMenu(fileName = "ArrayString", menuName = "NewObject/Serializable/ArrayString")]
public class SerializableArrayString : ScriptableObject
{
    [SerializeField] string[] value;
    public string[] Value { get => value; }
}
