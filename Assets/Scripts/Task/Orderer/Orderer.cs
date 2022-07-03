using UnityEngine;

public abstract class Orderer : MonoBehaviour
{
    public abstract void MakeOrder();
    public abstract int TotalServed();
}
