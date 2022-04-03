using System.Collections.Generic;
using UnityEngine;

public class GrillPlank : MonoBehaviour
{
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
    private ItemType beefType = ItemType.NONE;

    private void OnTriggerEnter(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        cookables.Add(item);
        CheckAddedItem(item);
    }
    private void OnTriggerExit(Collider other)
    {
        Item_Cookable item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        cookables.Remove(item);
        CheckRemovedItem(item);
    }

    private void CheckAddedItem(Item_Cookable item)
    {
        if (item.Has(ItemAttribute.DIRT))
            GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_CONTAMINADO);

        if (item.Is(ItemType.FRIED))
            GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_FRITO);

        else if(item.Is(ItemType.BEEF))
        {
            if (beefType == ItemType.NONE)
            {
                if (item.Is(ItemType.BEEF_NORMAL))
                    beefType = ItemType.BEEF_NORMAL;
                else
                    beefType = ItemType.BEEF_VEGAN;
            }
            else
            {
                if (!item.Is(beefType))
                    GameManager.MakeMistake(MistakeType.GRELHADOR_PRODUTO_MISTURADO);
                else
                    cookablesSequence.Add(item);
            }
        }
          
    }

    private void CheckRemovedItem(Item_Cookable item)
    {
        if(cookablesSequence.Contains(item)) {
            if (cookablesSequence[0] == item)
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

        if(cookables.Count == 0) 
        {
            beefType = ItemType.NONE;
        }
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
    }


    private void SetCookablesHeatSource(HeatSource source)
    {
        foreach (Item_Cookable item in cookables)
        {
            item.SetHeatSource(source);
        }
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
