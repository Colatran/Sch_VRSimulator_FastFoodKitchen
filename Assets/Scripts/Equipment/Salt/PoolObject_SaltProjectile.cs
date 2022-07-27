using UnityEngine;

public class PoolObject_SaltProjectile : PoolObject
{
    [SerializeField] SaltCanister canister;
    [SerializeField] Rigidbody rb;
    [SerializeField] TriggerArea IgnoreRaycast;
    [SerializeField] TriggerArea Interactible;

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }



    private bool used = false;

    public override void Enable()
    {
        base.Enable();

        time = 1;

        IgnoreRaycast.OnEnter += IgnoreRaycast_OnEnter;
        Interactible.OnEnter += Interactible_OnEnter;
        used = false;
    }

    private void IgnoreRaycast_OnEnter(Collider other)
    {
        used = true;
        DisableSelf();
    }
    private void Interactible_OnEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (used) return;
        used = true;

        item.AddAttribute(ItemAttribute.SALT);

        IgnoreRaycast.OnEnter -= IgnoreRaycast_OnEnter;
        Interactible.OnEnter -= Interactible_OnEnter;
        DisableSelf();
    }

    private float time;

    private void UpdateTime(float deltaTime)
    {
        if (time > 0)
        {
            time -= deltaTime;

            if (time < 0)
                DisableSelf();
        }
    }



    private void Update()
    {
        float deltaTime = Time.deltaTime;

        UpdateTime(deltaTime);
    }
}
