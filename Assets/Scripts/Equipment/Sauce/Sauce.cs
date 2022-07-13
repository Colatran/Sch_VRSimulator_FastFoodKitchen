using UnityEngine;

public class Sauce : MonoBehaviour
{
    static Vector3 scale_onAir = new Vector3(.2f, .2f, 8f);

    [SerializeField] Rigidbody rb;
    [SerializeField] Attachment attachment;
    [SerializeField] Transform mesh;

    private bool isAirborn = false;
    private float time = 3;

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (attachment == null) attachment = GetComponent<Attachment>();
    }



    private void Start()
    {
        SetAirborn();
        mesh.localRotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
    }

    private void OnEnable()
    {
        attachment.OnAttach += SetGrounded;
        attachment.OnDetach += SetAirborn;
    }
    private void OnDisable()
    {
        attachment.OnAttach -= SetGrounded;
        attachment.OnDetach -= SetAirborn;
    }



    void Update()
    {
        if (isAirborn)
        {
            Update_Falling();
            Update_Time();
        }
    }

    private void Update_Falling()
    {
        float distance = rb.velocity.magnitude * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(
            transform.position, rb.velocity,
            distance, AssetHolder.Asset.mask_popUp.Value
            );

        foreach (RaycastHit hit in hits)
        {
            Attachment attachment = hit.collider.GetComponentInParent<Attachment>();
            if (attachment == null)
            {
                if (rb == null) return;
                SetGrounded();
                GameManager.AddDirt();
                transform.position = hit.point;
                return;
            }

            Item item = attachment.GetComponent<Item>();
            if(item == null)
            {
                Destroy(gameObject);
            }
            else if(!item.Is(ItemType.EQUIPMENT_CLEANINGSPONGE))
            {
                this.attachment.Attach(attachment);
                transform.position = hit.point;
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }
    }
    private void Update_Time()
    {
        time -= Time.deltaTime;
        if (time < 0) Destroy(gameObject);
    }



    private void SetAirborn()
    {
        isAirborn = true;
        attachment.EnableRigidbody();

        mesh.localScale = scale_onAir;
        transform.localRotation = Quaternion.identity;
    }
    private void SetGrounded()
    {
        isAirborn = false;
        attachment.DisableRigidbody();

        mesh.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }



    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) item = other.GetComponentInParent<Item>();
        if (item == null) return;

        if (item.Is(ItemType.EQUIPMENT_CLEANINGSPONGE))
        {
            GameManager.RemoveDirt();

            Destroy(gameObject);
        }
    }
}
