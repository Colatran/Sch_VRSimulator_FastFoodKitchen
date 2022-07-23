using UnityEngine;

public class Item_Cookable : Item
{
    [SerializeField] MaterialPropertyController cookedMaterial;
    [SerializeField] bool isHandSensitive = true;


    private HeatSource source = HeatSource.NONE;        
    private float temperature = 0;
    private float cooked = 0;
    private float maxTemperatureFactor = 0;
    private float cookingTimeFactor = 0;


    private bool canCallOnBurned = true;
    private bool canCallOnCold = true;
    public delegate void Action();
    public event Action OnBurned;
    public event Action OnCold;

    public bool IsUndercooked { get => cooked < GameManager.LmtCook_undercooked; }
    public bool IsOvercooked { get => cooked > GameManager.LmtCook_overcooked; }
    public bool IsCooked { get => !IsUndercooked && !IsOvercooked; }



    public void SetHeatSource(HeatSource source) => this.source = source;

    public void SetCookingFactors(float maxTemp, float cookingTime)
    {
        maxTemperatureFactor = maxTemp;
        cookingTimeFactor = cookingTime;
    }



    private void Update()
    {
        CalculateTemperature();
    }



    private void CalculateTemperature()
    {
        switch (source)
        {
            case HeatSource.NONE:
                HeatSource_None();
                return; 

            case HeatSource.COOKER:
                HeatSource_Cooker();
                return;

            case HeatSource.STOVE:
                HeatSource_Stove();
                return;
        }
    }

    private void HeatSource_None()
    {
        if (temperature > GameManager.LmtTemp_roomTemperature)
        {
            float deltaTime = Time.deltaTime;
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

    private void HeatSource_Cooker()
    {
        float deltaTime = Time.deltaTime;
        temperature += deltaTime * maxTemperatureFactor;
        cooked += deltaTime * cookingTimeFactor;

        cookedMaterial.Set(cooked);

        if (canCallOnBurned && IsOvercooked)
        {
            canCallOnBurned = false;

            if (OnBurned != null)
                OnBurned();
        }
    }

    private void HeatSource_Stove()
    {
        float deltaTime = Time.deltaTime;
        if (temperature > GameManager.LmtTemp_stoveMax) temperature -= deltaTime;
        else if (temperature < GameManager.LmtTemp_stoveMin) temperature += deltaTime;
    }



    public void SetCooked()
    {
        temperature = 100;
        cooked = 1;
        cookedMaterial.Set(1);
    }



    private void OnEnable()
    {
        if (isHandSensitive)
            attachment.OnAttach += OnAttach;
    }
    private void OnDisable()
    {
        if (isHandSensitive)
            attachment.OnAttach -= OnAttach;
    }
    
    private void OnAttach()
    {
        if (IsCooked || IsOvercooked)
        {
            if (Attachment.DirectParent.GetComponent<HandPhysicsController>() != null)
            {
                GameManager.MakeMistake(MistakeType.PRODUTO_COMASMAOS);
            }
        }
    }
}
