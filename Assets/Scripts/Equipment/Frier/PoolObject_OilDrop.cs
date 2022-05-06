using UnityEngine;

public class PoolObject_OilDrop : PoolObject
{
    [SerializeField] GameObjectPool greasePool;
    public void SetGreasePool(GameObjectPool pool) => greasePool = pool;

    private Transform host;
    private float health = 0;
    private float greaseTime = 0;



    public override void Enable()
    {
        base.Enable();

        health = Random.Range(5, 9);
    }

    public void SetHost(Transform _host)
    {
        host = _host;
    }

    public delegate void Action();
    public event Action OnDrop;



    void FixedUpdate()
    {
        if (health > 1)
        {
            health -= Time.fixedDeltaTime;

            RaycastHit hitInfo;
            Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, 1, GameManager.OilMask);

            float diameter = health / 10;
            float length = hitInfo.distance;
            transform.localScale = new Vector3(diameter, length, diameter);

            transform.position = host.position;
            transform.rotation = Quaternion.identity;

            if(hitInfo.collider == null)
            {
                EndDrop();
                return;
            }

            if (hitInfo.collider.tag != "Oil")
            {
                if (greaseTime > 0)
                {
                    greaseTime -= Time.fixedDeltaTime;
                }
                else
                {
                    greaseTime = .25f;

                    GameObject grease = greasePool.GetObject();
                    grease.transform.position = hitInfo.point;
                    grease.SetActive(true);

                    if (OnDrop != null)
                        OnDrop();
                }
            }
        }
        else
        {
            EndDrop();
        }
    }



    private void EndDrop()
    {
        health = 0;
        transform.localScale = Vector3.zero;
        DisableSelf();
    }
}
