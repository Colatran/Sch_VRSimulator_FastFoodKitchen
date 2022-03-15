using UnityEngine;

public class Item_Cookable : Item
{
    [SerializeField] CookedMaterial cookedMaterial;

    private const float limitUndercooked = 0.9f;
    private const float limitOvercooked = 1.1f;


    private HeatSource source = HeatSource.NONE;
    private float temperature = 0;
    private float cooked = 0;
    public bool Undercooked { get => cooked < limitUndercooked; }
    public bool Overcooked { get => cooked > limitOvercooked; }
    private bool calledOnCold = false;
    public delegate void EmptyAction();
    public event EmptyAction OnCold;



    private void Update()
    {
        SetTemperature();
    }

    private void SetTemperature()
    {
        if (temperature < 0) return;
        float deltatime = Time.fixedDeltaTime;

        switch (source)
        {
            case HeatSource.NONE:
                if (temperature > 24)
                {
                    temperature -= deltatime;

                    if (
                        temperature < 25
                        && !calledOnCold
                        && cooked > limitUndercooked)
                    {
                        calledOnCold = true;
                        if (OnCold != null) OnCold();
                    }
                }
                break;

            case HeatSource.COOKER:
                temperature += deltatime;
                cooked += deltatime;
                cookedMaterial.Set(cooked / GameManager.CookingTime);
                break;

            case HeatSource.STOVE:
                if (temperature > 40) temperature -= deltatime;
                else if (temperature < 35) temperature += deltatime;
                break;
        }
    }
}


public enum HeatSource
{
    NONE,
    COOKER,
    STOVE
}
