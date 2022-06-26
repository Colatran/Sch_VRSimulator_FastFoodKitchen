using UnityEngine;

public class PoolObject_Dirt : PoolObject
{
    [SerializeField] MaterialPropertyController materialProperty;

    private void OnValidate()
    {
        if (materialProperty == null) materialProperty = GetComponent<MaterialPropertyController>();
    }


    public override void Enable()
    {
        base.Enable();

        materialProperty.Set();

        GameManager.AddDirt();
    }


    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) item = other.GetComponentInParent<Item>();
        if (item == null) return;

        if (item.Is(ItemType.EQUIPMENT_CLEANINGSPONGE))
        {
            GameManager.RemoveDirt();

            DisableSelf();
        }
    }
}
