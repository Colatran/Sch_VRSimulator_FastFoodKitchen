using UnityEngine;

[CreateAssetMenu(fileName = "AssetHolderObject", menuName = "NewObject/AssetHolderObject")]
public class AssetHolderObject : ScriptableObject
{
    public Material Material_Hilight;

    public Material Material_Light_Green;
    public Material Material_Light_Off;
    public Material Material_Light_Red;

    public SerializableLayerMask mask_oil;
    public SerializableLayerMask mask_popUp;
}
