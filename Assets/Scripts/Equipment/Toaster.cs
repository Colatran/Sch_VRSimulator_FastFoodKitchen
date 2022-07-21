using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField] CookingFactors cookingFactors;
    [SerializeField] Button3D button;
    [SerializeField] MeshRenderer tosterLight;

    private List<Item_Cookable> cookables = new List<Item_Cookable>();
    private bool On = false;
    private float cookingTime = 0;



    private void OnEnable()
    {
        button.OnPressed += Button_OnPressed;
    }
    private void OnDisable()
    {
        button.OnReleased -= Button_OnPressed;
    }

    private void Button_OnPressed()
    {
        if (On)
        {
            StopToasing();
        }
        else
        {
            StarToasing();
        }
    }


    private void StarToasing()
    {
        cookingTime = cookingFactors.CookingTime;
        On = true;

        foreach(Item_Cookable cookable in cookables)
            cookable.SetHeatSource(HeatSource.COOKER);

        tosterLight.material = AssetHolder.Asset.Material_Light_Red;
    }
    private void StopToasing()
    {
        On = false;

        foreach (Item_Cookable cookable in cookables)
            cookable.SetHeatSource(HeatSource.NONE);
        
        tosterLight.material = AssetHolder.Asset.Material_Light_Off;
    }


    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (item.Is(ItemType.BREAD))
        {
            Item_Cookable cookable = item as Item_Cookable;
            AddCookable(cookable);
            cookable.SetCookingFactors(cookingFactors.MaxTemperatureFactor, cookingFactors.CookingTimeFactor);

            if (On) cookable.SetHeatSource(HeatSource.COOKER);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item_Cookable>();
        if (item == null) return;

        if (item.Is(ItemType.BREAD))
        {
            Item_Cookable cookable = item as Item_Cookable;
            RemoveCookable(cookable);
            if (On) cookable.SetHeatSource(HeatSource.NONE);
        }
    }

    private void AddCookable(Item_Cookable cookable)
    {
        cookables.Add(cookable);
        cookable.OnBurned += OnBurned;

    } 
    private void RemoveCookable(Item_Cookable cookable)
    {
        foreach (Item_Cookable _cookable in cookables)
            _cookable.OnBurned -= OnBurned;

        cookables.RemoveAll(x => x == cookable);
    }

    private void OnBurned()
    {
        GameManager.MakeMistake(MistakeType.PREPARADOR_TORRADEIRA_QUEIMADO);
    }

    private void Update()
    {
        if(On)
        {
            cookingTime -= Time.deltaTime;
            if(cookingTime <= 0)
            {
                StopToasing();
            }
        }
    }
}
