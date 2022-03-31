using UnityEngine;

public class Item_Cookable : Item
{
    [SerializeField] CookedMaterial cookedMaterial;

    private const float limitUndercooked = 0.9f;
    private const float limitOvercooked = 1.1f;


    private HeatSource source = HeatSource.NONE;        
    private float temperature = 0;
    private float cooked = 0;
    public bool IsUndercooked { get => cooked < limitUndercooked; }
    public bool IsOvercooked { get => cooked > limitOvercooked; }

    private bool toCallOnBurned = true;
    private bool toCallOnCold = true;
    public delegate void Action();
    public event Action OnBurned;
    public event Action OnCold;





    public void SetHeatSource(HeatSource source) => this.source = source;


    private void Update()
    {
        CalculateTemperature();
    }

    private void CalculateTemperature()
    {
        if (temperature < 0 || temperature > 100) return;
        float deltaTime = Time.fixedDeltaTime;

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
        if (temperature > 24)
        {
            temperature -= deltaTime;

            if (toCallOnCold && temperature < 25)
            {
                toCallOnCold = false;

                if (OnCold != null)
                    OnCold();
            }
        }
    }

    private void HeatSource_Cooker(float deltaTime)
    {
        temperature += deltaTime;
        cooked += deltaTime;
        cookedMaterial.Set(cooked / GameManager.CookingTime);

        if (toCallOnBurned && IsOvercooked)
        {
            toCallOnBurned = false;

            if (OnBurned != null)
                OnBurned();
        }
    }

    private void HeatSource_Stove(float deltaTime)
    {
        if (temperature > 45) temperature -= deltaTime;
        else if (temperature < 40) temperature += deltaTime;
    }
}


public enum HeatSource
{
    NONE,
    COOKER,
    STOVE
}
