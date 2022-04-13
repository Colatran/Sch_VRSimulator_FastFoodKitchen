using System.Collections.Generic;
using UnityEngine;

public class GrillPlank : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;
    [SerializeField] BatchHandler batchHandler;
    [SerializeField] Transform plank;
    [SerializeField] Button3D button;
    [SerializeField] Animator animator;
    [SerializeField] float plankSpeed = .5f;


    private void OnEnable()
    {
        button.OnPressed += OnButtonPressed;
    }
    private void OnDisable()
    {
        button.OnPressed -= OnButtonPressed;
    }





    private List<Item_Cookable> cookables = new List<Item_Cookable>();
    private List<Item_Cookable> cookablesSequence = new List<Item_Cookable>();
    private List<Item_Cookable> cookablesMustIgnore = new List<Item_Cookable>();
    private ItemType beefType = ItemType.NONE;

    private void OnTriggerEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        cookables.Add(item);

        bool isNew = !cookablesMustIgnore.Contains(item);
        if (isNew) cookablesMustIgnore.Add(item);

        CheckAddedItem(item, isNew);
    }
    private void OnTriggerExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        cookables.Remove(item);
        CheckRemovedItem(item);
    }

    private void CheckAddedItem(Item_Cookable item, bool isNew)
    {
        if (item.Has(ItemAttribute.DIRT))
            GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_CONTAMINADO);

        if (isNew)
        {
            if (greaseCount > 0)
                GameManager.MakeMistake(MistakeType.GRELHADOR_SUJO);

            if (item.Is(ItemType.FRIED))
                GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_FRITO);
        }

        if(item.Is(ItemType.BEEF))
        {
            if (beefType == ItemType.NONE)
            {
                if (item.Is(ItemType.BEEF_NORMAL))
                    beefType = ItemType.BEEF_NORMAL;
                else
                    beefType = ItemType.BEEF_VEGAN;
            }

            if (isNew)
            {
                if (!item.Is(beefType))
                    GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_MISTURADO);
            }
        }
          
    }

    private void CheckRemovedItem(Item_Cookable item)
    {
        if (cookablesSequence.Contains(item))
        {
            if (!item.IsCooked)
            {
                cookablesSequence.Remove(item);
            }
            else if (cookablesSequence[0] == item)
            {
                cookablesSequence.Remove(item);

                if (cookablesSequence.Count == 0)
                    beefType = ItemType.NONE;
            }
            else
            {
                GameManager.MakeMistake(MistakeType.GRELHADOR_ORDEMERRADA);
                cookablesSequence.Clear();
            }
        }

        if (cookables.Count == 0)
        {
            beefType = ItemType.NONE;
        }

        cookablesMustIgnore.RemoveAll(x => x == null);
    }





    private bool mustMove = false;
    private bool mustBeClosed = false;
    private float plankOpenness = 1;
    private bool cooking = false;
    private float cookingTime = 0;

    private void OnButtonPressed()
    {
        if (mustBeClosed)
            StartOpen();
        else
            StartClose();
    }

    private void StartOpen()
    {
        mustBeClosed = false;
        mustMove = true;

        if (cooking)
            StopCooking();
    }
    private void FinishOpen()
    {
        mustMove = false;
        animator.SetFloat("Open", 1);
    }

    private void StartClose()
    {
        mustBeClosed = true;
        mustMove = true;
    }
    private void FinishClose()
    {
        mustMove = false;
        animator.SetFloat("Open", 0);

        StartCooking();
    }


    private void StartCooking()
    {
        cookingTime = GameManager.CookingTime;
        cooking = true;
        SetCookablesHeatSource(HeatSource.COOKER);
    }
    private void StopCooking()
    {
        cooking = false;
        SetCookablesHeatSource(HeatSource.NONE);
        SetGrease();

        foreach (Item_Cookable item in cookables)
            if (item.Is(beefType) && item.BatchId == 0)
                cookablesSequence.Add(item);

        batchHandler.NextBatch();
        foreach (Item_Cookable item in cookablesSequence)
            batchHandler.AddItem(item);
    }

    private void SetCookablesHeatSource(HeatSource source)
    {
        foreach (Item_Cookable item in cookables)
            item.SetHeatSource(source);
    }





    private int greaseCount = 0;

    private void SetGrease()
    {
        foreach (Item_Cookable item in cookables)
        {
            Vector3 itemPosition = item.transform.position;
            GameObject grease;

            grease = pool.GetObject();
            grease.transform.position = new Vector3(itemPosition.x, transform.position.y, itemPosition.z);
            grease.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            grease.SetActive(true);
            grease.GetComponent<PoolObject>().OnDisable += OnGreaseDisable;

            grease = pool.GetObject();
            grease.transform.position = new Vector3(itemPosition.x, transform.position.y + 0.011f, itemPosition.z);
            grease.transform.rotation = Quaternion.Euler(180, Random.Range(0, 360), 0);
            grease.transform.parent = plank;
            grease.SetActive(true);
            grease.GetComponent<PoolObject>().OnDisable += OnGreaseDisable;

            greaseCount += 2;
        }
    }

    private void OnGreaseDisable(PoolObject poolObject)
    {
        poolObject.OnDisable -= OnGreaseDisable;
        greaseCount--;
    }



    private void UpdatePlankOpenness(float deltaTime)
    {
        if (mustBeClosed)
        {
            if (plankOpenness == 0) FinishClose();
            else if (plankOpenness < 0) plankOpenness = 0;
            else plankOpenness -= deltaTime * plankSpeed;
        }
        else
        {
            if (plankOpenness == 1) FinishOpen();
            else if (plankOpenness > 1) plankOpenness = 1;
            else plankOpenness += deltaTime * plankSpeed;
        }

        animator.SetFloat("Open", plankOpenness);
    }

    private void UpdateCookingTime(float deltaTime)
    {
        cookingTime -= deltaTime;

        if (cookingTime < 0) 
        {
            StartOpen();
        }
    }



    private void Update()
    {
        float deltaTime = Time.deltaTime;

        if (mustMove)
            UpdatePlankOpenness(deltaTime);

        if (cooking)
            UpdateCookingTime(deltaTime);
    }
}
