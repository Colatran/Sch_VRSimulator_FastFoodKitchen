using UnityEngine;

public class SaltProjectileTrigger : MonoBehaviour
{
    [SerializeField] PoolObject_SaltProjectile projectile;

    private void OnTriggerEnter(Collider other) 
        => projectile.TriggerEnter(other);
}
