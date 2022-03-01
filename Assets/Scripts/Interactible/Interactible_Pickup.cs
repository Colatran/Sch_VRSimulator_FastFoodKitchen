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

        if(attachment.NotAttached)
        {
            attachment.AttachTo(toAttach);
            //RemoveHilight();
        }
        else if (attachment.Parent == toAttach) 
        {
            attachment.Detach();
            //AddHilight();
        }
        else
        {
            attachment.SwitchToParent(toAttach);
        }
    }
}
