using UnityEngine;

public class Interactible_Spawner : Interactible
{
    [SerializeField] GameObject prefab;


    public override void Grab(HandInteractor sender)
    {
        GameObject gObject = Instantiate(prefab);
        Interactible interactible = gObject.GetComponent<Interactible>();

        sender.SetGrabbed(interactible);
        interactible.Grab(sender);

        gObject.transform.localPosition = new Vector3(-0.01f, -0.04f, 0.1f);
        gObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
    public override void Release(HandInteractor sender)
    {
        
    }
}
