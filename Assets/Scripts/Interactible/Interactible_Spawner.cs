using UnityEngine;

public class Interactible_Spawner : Interactible
{
    [SerializeField] GameObject prefab;


    public override void Grab(HandInteractor sender)
    {
        GameObject gObject = Instantiate(prefab);
        Interactible interactible = gObject.GetComponent<Interactible>();
        sender.Grab(interactible);

        gObject.transform.position = new Vector3(0, -0.07f, 0.128f);
        gObject.transform.rotation = new Quaternion(0, 0, 270, 0);
    }
    public override void Release(HandInteractor sender)
    {
        throw new System.NotImplementedException();
    }
}
