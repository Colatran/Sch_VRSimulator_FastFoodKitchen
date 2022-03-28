using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] HandInputManager manager;
    [SerializeField] Animator animator;


    private void OnValidate()
    {
        if(manager == null) manager = GetComponent<HandInputManager>();
    }


    private void Update()
    {
        animator.SetFloat("Grip", manager.SelectActionValue);
        animator.SetFloat("Trigger", manager.ActivateActionValue);
    }
}
