using UnityEngine;

public class Item_Cookable : Item
{
    [SerializeField] MaterialPropertyController cookedMaterial;

    private HeatSource source = HeatSource.NONE;        
    private float temperature = 0;
    private float cooked = 0;

    private bool canCallOnBurned = true;
    private bool canCallOnCold = true;
    public delegate void Action();
    public event Action OnBurned;
    public event Action OnCold;

    public bool IsUndercooked { get => cooked < GameManager.LmtCook_undercooked; }
    public bool IsOvercooked { get => cooked > GameManager.LmtCook_overcooked; }
    public bool IsCooked { get => !IsUndercooked && !IsOvercooked; }



    public void SetHeatSource(HeatSource source) => this.source = source;


    private void Update()
    {
        CalculateTemperature();
    }


    private void CalculateTemperature()
    {
        float deltaTime = Time.deltaTime;

        switch (source)
        {
            case HeatSource.NONE:
                HeatSource_None(deltaTime);
                return; 

            case HeatSource.COOKER:
                HeatSource_Cooker(deltaTime);
                return;

            case HeatSource.STOVE:
                HeatSource_Stove(deltaTime);
                return;
        }
    }

    private void HeatSource_None(float deltaTime)
    {
        if (temperature > GameManager.LmtTemp_roomTemperature)
        {
            temperature -= deltaTime;

            if (canCallOnCold 
                && IsCooked
                && temperature < GameManager.LmtTemp_cold)
            {
                canCallOnCold = false;

                if (OnCold != null)
                    OnCold();
            }
        }
    }

    private void HeatSource_Cooker(float deltaTime)
    {
        float normalizedDeltaTime = deltaTime * GameManager.CookingTimeFactor;

        temperature += deltaTime * GameManager.MaxTemperatureFactor;

        cooked += deltaTime * GameManager.CookingTimeFactor;
        cookedMaterial.Set(cooked);

        if (canCallOnBurned && IsOvercooked)
        {
            canCallOnBurned = false;

            if (OnBurned != null)
                OnBurned();
        }
    }

    private void HeatSource_Stove(float deltaTime)
    {
        if (temperature > GameManager.LmtTemp_stoveMax) temperature -= deltaTime;
        else if (temperature < GameManager.LmtTemp_stoveMin) temperature += deltaTime;
    }



    public void SetCooked()
    {
        temperature = 100;
        cooked = 1;
        cookedMaterial.Set(1);

        if (Is(ItemType.BEEF))
            AddAttribute(ItemAttribute.SALT);
    }
}
