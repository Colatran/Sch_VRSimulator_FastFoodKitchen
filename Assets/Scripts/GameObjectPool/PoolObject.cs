using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] protected GameObjectPool pool;
    public GameObjectPool Pool { get => pool; set => pool = value; }

    public delegate void Action(PoolObject poolObject);
    public event Action OnEnable;
    public event Action OnDisable;


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
