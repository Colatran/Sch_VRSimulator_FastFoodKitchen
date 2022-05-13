using UnityEngine;

public class CookOrderer : MonoBehaviour
{
    [SerializeField] Order[] orders;
    [SerializeField, ReadOnly] float totalLikelihood = 0;
    [SerializeField] float orderDelay = 60;

    private float  nextOrderTime = 0;

    public Order[] Orders { get => orders; }



    private void OnValidate()
    {
        totalLikelihood = 0;
        foreach (Order order in orders)
            totalLikelihood += order.Likelihood;
    }



    public void MakeOrder()
    {
        float seed = Random.Range(0, totalLikelihood);
        float likelihoodStep = 0;

        for (int i = 0; i < orders.Length; i++)
        {
            Order order = orders[i];

            likelihoodStep += order.Likelihood;
            if (seed < likelihoodStep)
                order.MakeOrder();
        }
    }

    private void Update()
    {
        nextOrderTime -= Time.deltaTime;

        if(nextOrderTime < 0)
        {
            MakeOrder();
            nextOrderTime = orderDelay;
        }
    }



    public void ServeOrder(ItemType itemType, int count)
    {
        foreach(Order order in orders) 
        {
            if (order.ItemType == itemType)
            {
                order.Serve(count);
                return;
            }
        }
    }



    [System.Serializable]
    public class Order
    {
        [SerializeField] ItemType itemType;
        public ItemType ItemType { get => itemType; }

        [SerializeField, Range(0, 100)] float likelihood = 0;
        public float Likelihood { get => likelihood; }

        [SerializeField, Min(1)] int batchSize = 5;
        [SerializeField, Min(2)] int maxBatchCount = 2;

        private int orderd = 0;
        public int Ordered { get => orderd < 0 ? 0 : orderd; }

        private int served = 0;
        public int Served { get => served; }

        public delegate void Action();
        public event Action OnOrderdChanged;


        public void MakeOrder()
        {
            int batchCount = Random.Range(1, maxBatchCount + 1);
            orderd += batchCount * batchSize;

            if (OnOrderdChanged != null)
                OnOrderdChanged();
        }
        public void Serve(int count)
        {
            orderd -= count;
            served += count;

            if (OnOrderdChanged != null)
                OnOrderdChanged();
        }
    }
}
