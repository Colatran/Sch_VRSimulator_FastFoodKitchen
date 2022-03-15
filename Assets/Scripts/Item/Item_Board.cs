using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Board : Item
{
    protected override void OnAttach()
    {
        //Debug.Log(gameObject.name + " OnAttach");
    }

    protected override void OnDetach()
    {
        //Debug.Log(gameObject.name + " OnDetach");
    }

    protected override void OnAddContent(Attachment child)
    {
        //Debug.Log(gameObject.name + " OnNewContent : " + child.name);
    }

    protected override void OnRemoveContent(Attachment child)
    {
        //Debug.Log(gameObject.name + " OnRemoveContent : " + child.name);
    }
}
