using UnityEngine;

public class MaterialPropertyController_Offset : MaterialPropertyController
{
    public override void Set()
    {
        float x = Random.Range(0, 4);
        float y = Random.Range(0, 4);
        Vector4 value = new Vector4(x * 256 / 1024, y * 256 / 1024);

        propBlock = new MaterialPropertyBlock();
        m_renderer.GetPropertyBlock(propBlock);
        propBlock.SetVector("_Offset", value);
        m_renderer.SetPropertyBlock(propBlock);
    }
}