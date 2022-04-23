using UnityEngine;

public class MaterialPropertyController : MonoBehaviour
{
    [SerializeField] protected Renderer m_renderer;

    protected MaterialPropertyBlock propBlock;


    protected virtual void OnValidate()
    {
        if (m_renderer == null) Debug.LogWarning("Defina Renderer");
    }

    protected virtual void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }


    public virtual void Set() { }
    public virtual void Set(float val) { }
}
