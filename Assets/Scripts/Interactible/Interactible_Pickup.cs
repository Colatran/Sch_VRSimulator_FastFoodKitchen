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



    public override void Grab(HandInteractor sender)
    {
        Attachment toAttach = sender.Attachment;
        attachment.Attach(toAttach);
    }
    public override void Release(HandInteractor sender)
    {
        attachment.Detach();
    }
}
