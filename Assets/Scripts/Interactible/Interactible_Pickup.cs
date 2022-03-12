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
            attachment.Attach(toAttach);
        }
        else if (attachment.EndParent == toAttach)
        {
            attachment.Detach();
        }
        else
        {
            attachment.SwitchParent(toAttach);
        }
    }
}
