using UnityEngine;

public class MaterialPropertyController_Cooked : MaterialPropertyController
{
    [SerializeField] float min = 0.0f;
    [SerializeField] float max = 1.0f;

    private float value = 0.0f;


    public override void Set(float val)
    {
        value = val;
        if (value > max) value = max;
        else if (value < min) value = min;

        m_renderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Position", value);
        m_renderer.SetPropertyBlock(propBlock);
    }
}
