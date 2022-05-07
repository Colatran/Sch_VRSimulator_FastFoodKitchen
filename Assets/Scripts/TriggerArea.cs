using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public delegate void Action(Collider other);
    public event Action OnEnter;
    public event Action OnExit;


    private void OnTriggerEnter(Collider other)
    {
        if (OnEnter == null) return;
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnExit == null) return;
        OnExit(other);
    }
}
