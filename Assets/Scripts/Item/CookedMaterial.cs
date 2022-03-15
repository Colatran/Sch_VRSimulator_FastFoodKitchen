using UnityEngine;

public class CookedMaterial : MonoBehaviour
{
    [SerializeField] Renderer m_renderer;
    [SerializeField] float min = 0.0f;
    [SerializeField] float max = 1.0f;

    private float value = 0.0f;
    private MaterialPropertyBlock propBlock;

    private void OnValidate()
    {
        if (m_renderer == null) Debug.LogWarning("Defina m_renderer"); 
    }


    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }


    public void Set(float val)
    {
        value = val;
        if (value > max) value = max;
        else if (value < min) value = min;

        m_renderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Position", value);
        m_renderer.SetPropertyBlock(propBlock);
    }
}
