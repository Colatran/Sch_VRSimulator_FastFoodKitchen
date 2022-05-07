using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Spawner : Interactible
{
    [SerializeField] GameObject prefab;

    public override void Interact(HandInteractor sender, bool grab)
    {
        if (!grab) return;

        
    }
}
