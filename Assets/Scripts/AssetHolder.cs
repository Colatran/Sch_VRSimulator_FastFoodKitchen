using UnityEngine;

[CreateAssetMenu(fileName = "AssetHolder", menuName = "NewObject/AssetHolder")]
public class AssetHolder : ScriptableObject
{
    public Material Material_Hilight;

    public Material Material_UHC_TimerOn;
    public Material Material_UHC_TimerOff;

    public SerializableLayerMask mask_oil;
    public SerializableLayerMask mask_popUp;
}
