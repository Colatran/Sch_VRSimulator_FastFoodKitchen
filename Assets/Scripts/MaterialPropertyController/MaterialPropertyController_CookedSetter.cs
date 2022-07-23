using UnityEngine;

public class MaterialPropertyController_CookedSetter : MonoBehaviour
{
    [SerializeField]
    MaterialPropertyController_Cooked materialProperty;

    void Start()
    {
        materialProperty.Set(1);
    }
}
