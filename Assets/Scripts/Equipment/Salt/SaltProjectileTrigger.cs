using UnityEngine;

public class SaltProjectileTrigger : MonoBehaviour
{
    [SerializeField] PoolObject_SaltProjectile projectile;
    [SerializeField] Collider projectile_collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == projectile_collider) return;

        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item != null) item.AddAttribute(ItemAttribute.SALT);

        projectile.DisableSelf();
    }
}
