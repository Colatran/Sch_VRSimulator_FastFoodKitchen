using System.Collections.Generic;
using UnityEngine;

public class BatchHandler : MonoBehaviour
{
    [SerializeField] int numberBatches = 5;

    private List<Batch> batches = new List<Batch>();
    private Batch currentBatch;
    private int currentBatchIndex = -1;


    private void Start()
    {
        for (int i = 0; i < numberBatches; i++)
            batches.Add(new Batch());

        NextBatch();
    }


    public void NextBatch()
    {
        currentBatchIndex++;
        if (currentBatchIndex == numberBatches)
            currentBatchIndex = 0;

        currentBatch = batches[currentBatchIndex];
        currentBatch.Reset();
    }

    public void AddItem(Item_Cookable item)
    {
        currentBatch.AddItem(item);
    }
}
