using UnityEngine;

public class InteractibleDisabler : MonoBehaviour
{
    [SerializeField] Attachment attachment;
    [SerializeField] Interactible interactible;
    [SerializeField] Collider box;
    [SerializeField] ItemType parentType;


    private void OnEnable()
    {
        attachment.OnAttach += OnAttach;
        attachment.OnDetach += OnDetach;
    }
    private void OnDisable()
    {
        attachment.OnAttach -= OnAttach;
        attachment.OnDetach -= OnDetach;
    }



    private void OnAttach()
    {
        Item item = attachment.DirectParent.GetComponent<Item>();
        if (item == null) return;

        if (item.Is(parentType))
        {
            DisableInteractible();
        }
    }
    private void OnDetach()
    {
        EnableInteractible();
    }



    public void EnableInteractible()
    {
        box.enabled = true;
    }
    public void DisableInteractible()
    {
        box.enabled = false;
        //interactible.RemoveHilight();
    }

}
