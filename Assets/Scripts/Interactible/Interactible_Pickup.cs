using UnityEngine;

public class Interactible_Pickup : Interactible
{
    [SerializeField] Attachment attachment;


    protected override void OnValidate()
    {
        base.OnValidate();

        if(attachment == null) attachment = GetComponent<Attachment>();
    }


    public override void Interact(GameObject gObject)
    {
        Attachment toAttach = gObject.GetComponent<Attachment>();
        if (toAttach == null) return;

        if(attachment.IsNotAttached)
        {
            attachment.AttachTo(toAttach, true);
        }
        else if (attachment.EndParent == toAttach)
        {
            attachment.Detach(true);
        }
        else
        {
            attachment.SwitchParent(toAttach);
        }
    }
}
