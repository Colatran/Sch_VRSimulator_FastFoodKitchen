using UnityEngine;

public class MaterialPropertyController_RandomOffset : MaterialPropertyController
{
    public override void Set()
    {
        Vector2 value = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        m_renderer.GetPropertyBlock(propBlock);
        propBlock.SetVector("_Offset", value);
        m_renderer.SetPropertyBlock(propBlock);
    }
}
