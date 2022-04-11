using UnityEngine;

public class Batch
{
    private static int batchNumber = 0;
    private static int newBatch
    {
        get
        {
            batchNumber++;
            return batchNumber;
        }
    }


    private Item_Cookable pivot;
    private int batchId = 0;

    private void OnPivotCold()
    {
        GameManager.MakeMistake(MistakeType.PRODUTO_FRIO);
    }
    private void OnPivotBurned()
    {
        GameManager.MakeMistake(MistakeType.PRODURO_QUEIMADO);
    }


    private void SetPivot(Item_Cookable item)
    {
        ClearPivot();

        pivot = item;
        pivot.OnBurned += OnPivotBurned;
        pivot.OnCold += OnPivotCold;
    }

    private void ClearPivot() 
    { 
        if(pivot != null)
        {
            pivot.OnBurned -= OnPivotBurned;
            pivot.OnCold -= OnPivotCold;
        }

        pivot = null;
    }


    public void Reset()
    {
        ClearPivot();

        batchId = newBatch;
    }

    public void AddItem(Item_Cookable item)
    {
        if (item.BatchId != 0) return;
        item.BatchId = batchId;

        if (pivot == null)
            SetPivot(item);
    }
}
