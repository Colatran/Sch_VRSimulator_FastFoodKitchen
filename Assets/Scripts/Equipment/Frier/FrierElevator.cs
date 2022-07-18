using UnityEngine;

public class FrierElevator : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Attachment attachment;
    [SerializeField] Frier frier;

    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private const float movingTime = 2;
    private float currentTime = 0;
    private bool moving = false;
    private bool up = true;
    private FrierBasket basket;
    public FrierBasket Basket { get => basket; }

    public delegate void Action();
    public Action OnStopped;


    private void Start()
    {
        initialPosition = transform.localPosition;
        finalPosition = transform.localPosition + new Vector3(0, -initialPosition.y, 0);
    }

    private void OnEnable()
    {
        attachment.OnAddContent += Attachment_OnAddContent;
        attachment.OnRemoveContent += Attachment_OnRemoveContent;
    }
    private void OnDisable()
    {
        attachment.OnAddContent -= Attachment_OnAddContent;
        attachment.OnRemoveContent -= Attachment_OnRemoveContent;
    }


    private void Attachment_OnAddContent(Attachment attachment)
    {
        if (basket != null) return;

        basket = attachment.GetComponent<FrierBasket>();
        if (basket == null) return;

        basket.DefineBatch();
        frier.AddBasket(basket);
    }
    private void Attachment_OnRemoveContent(Attachment attachment)
    {
        FrierBasket basket = attachment.GetComponent<FrierBasket>();
        if (this.basket = basket)
        {
            this.basket = null;
            frier.RemoveBasket(this.basket);
        }
    }

    public void Move(bool up)
    {
        this.up = up;
        moving = true;
    }
    public void Stop()
    {
        moving = false;
        if (OnStopped != null)
            OnStopped();
    }


    private void Update()
    {
        if (moving) Update_Position();
    }

    private void Update_Position()
    {
        float deltaTime = Time.deltaTime / movingTime;
        if (up)
            currentTime -= deltaTime;
        else
            currentTime += deltaTime;

        Vector3 position = Vector3.Lerp(initialPosition, finalPosition, currentTime);
        transform.localPosition = position;

        if (currentTime < 0)
        {
            currentTime = 0;
            Stop();
        }
        else if (currentTime > 1)
        {
            currentTime = 1;
            Stop();
        }
    }
}
