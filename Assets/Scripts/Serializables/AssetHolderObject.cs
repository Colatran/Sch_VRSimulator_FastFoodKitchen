using UnityEngine;

[CreateAssetMenu(fileName = "AssetHolderObject", menuName = "NewObject/AssetHolderObject")]
public class AssetHolderObject : ScriptableObject
{
    public Material Material_Hilight;

    public Material Material_UHC_TimerOn;
    public Material Material_UHC_TimerOff;

    public SerializableLayerMask mask_oil;
    public SerializableLayerMask mask_popUp;
}
