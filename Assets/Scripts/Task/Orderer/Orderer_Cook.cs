using UnityEngine;
using TMPro;

public class Orderer_Cook : Orderer
{
    [SerializeField] Order[] orders;
    [SerializeField, ReadOnly] float totalLikelihood = 0;

    public Order[] Orders { get => orders; }



    private void OnValidate()
    {
        totalLikelihood = 0;
        foreach (Order order in orders)
            totalLikelihood += order.Likelihood;
    }



    public override void MakeOrder()
    {
        float seed = Random.Range(0, totalLikelihood);
        float likelihoodStep = 0;

        for (int i = 0; i < orders.Length; i++)
        {
            Order order = orders[i];

            likelihoodStep += order.Likelihood;
            if (seed < likelihoodStep)
            {
                order.MakeOrder();
                return;
            }
        }
    }

    public override int TotalServed()
    {
        int total = 0;
        foreach(Order order in orders)
        {
            total += order.Served;
        }
        return total;
    }

    public void ServeOrder(ItemType itemType, int count)
    {
        foreach (Order order in orders)
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

        [SerializeField] TMP_Text text_count;

        private int orderd = 0;
        public int Ordered { get => orderd < 0 ? 0 : orderd; }

        private int served = 0;
        public int Served { get => served; }

        


        public void MakeOrder()
        {
            int batchCount = Random.Range(1, maxBatchCount + 1);
            orderd += batchCount * batchSize;

            text_count.text = orderd + "";
        }
        public void Serve(int count)
        {
            orderd -= count;
            served += count;

            if(orderd < 0)
            {
                GameManager.MakeMistake(MistakeType.BATCHER_COZINHOUAMAIS);

                served += orderd;
                orderd = 0;
            }

            text_count.text = orderd + "";
        }
    }
}
