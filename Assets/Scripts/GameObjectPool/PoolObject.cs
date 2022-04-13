using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] protected GameObjectPool pool;
    public GameObjectPool Pool { get => pool; set => pool = value; }

    public delegate void PoolAction(PoolObject poolObject);
    public event PoolAction OnEnable;
    public event PoolAction OnDisable;


    public virtual void Enable()
    {
        if (OnEnable != null)
            OnEnable(this);
    }
    public virtual void Disable()
    {
        if (OnDisable != null)
            OnDisable(this);
    }


    public void DisableSelf()
    {
        pool.DisableObject(this);
    }
}
