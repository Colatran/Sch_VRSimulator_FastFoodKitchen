using UnityEngine;

public class PoolObject_SaltProjectile : PoolObject
{
    [SerializeField] Rigidbody rb;

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }



    public override void Enable()
    {
        base.Enable();

        time = 1;
    }



    private float time;

    private void UpdateTime(float deltaTime)
    {
        if (time > 0)
        {
            time -= deltaTime;

            if (time < 0)
            {
                DisableSelf();
            }
        }
    }



    private void Update()
    {
        float deltaTime = Time.deltaTime;

        UpdateTime(deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DisableSelf();
    }
}
