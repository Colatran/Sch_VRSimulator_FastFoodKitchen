using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] HandInputController controller;
    [SerializeField] Animator[] animators;


    private void OnValidate()
    {
        controller = GetComponent<HandInputController>();
    }


    private void Update()
    {
        foreach (Animator animator in animators)
        {
            animator.SetFloat("Grip", controller.SelectActionValue);
            animator.SetFloat("Trigger", controller.ActivateActionValue);
        }
    }
}
