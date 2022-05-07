using UnityEngine;

public class Interactible_Pickup : Interactible
{
    [SerializeField] Attachment attachment;
    public Attachment Attachment { get => attachment; }

    protected override void OnValidate()
    {
        base.OnValidate();

        if(attachment == null) attachment = GetComponent<Attachment>();
    }


    public override void Interact(HandInteractor sender, bool grab)
    {
        if (grab)
        {
            Attachment toAttach = sender.Attachment;
            if (toAttach == null) return;

            attachment.Attach(toAttach);
        }
        else
        {
            attachment.Detach();
        }
    }
}
