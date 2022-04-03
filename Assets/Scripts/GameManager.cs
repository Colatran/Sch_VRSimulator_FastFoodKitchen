using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnValidate()
    {
        ValidateCooking();
    }





    public static GameManager reference;
    
    private void Awake()
    {
        reference = this;
    }





    [Header("References")]
    [SerializeField] AssetHolder assetHolder;
    public static AssetHolder Asset { get => reference.assetHolder; }

    [SerializeField] PerformanceManager performanceManager;
    public static PerformanceManager PerformanceManager { get => reference.performanceManager; }
    public static void MakeMistake(MistakeType type)
    {
        reference.performanceManager.AddMistake(type);
    }

    [SerializeField] UIPopUp pauseMenu;
    public static UIPopUp PauseMenu { get => reference.pauseMenu; }





    [Header("Cooking")]
    [SerializeField] float cookingTime = 60;
    [SerializeField] float maxTemperature = 85;
    [ReadOnly, SerializeField] float cookingTimeFactor = 0;
    [ReadOnly, SerializeField] float maxTemperatureFactor = 0;
    [SerializeField] float lmtTemp_roomTemperature = 24.5f;
    [SerializeField] float lmtTemp_cold = 25f;
    [SerializeField] float lmtTemp_stoveMin = 40f;
    [SerializeField] float lmtTemp_stoveMax = 41f;
    [SerializeField] float lmtCook_undercooked = 0.9f;
    [SerializeField] float lmtCook_overcooked = 1.1f;

    private void ValidateCooking()
    {
        cookingTimeFactor = 1 / cookingTime;
        maxTemperatureFactor = maxTemperature / cookingTime;
    }

    public static float CookingTime { get => reference.cookingTime; }
    public static float CookingTimeFactor { get => reference.cookingTimeFactor; }
    public static float MaxTemperature { get => reference.maxTemperature; }
    public static float MaxTemperatureFactor { get => reference.maxTemperatureFactor; }
    public static float LmtTemp_roomTemperature { get => reference.lmtTemp_roomTemperature; }
    public static float LmtTemp_cold { get => reference.lmtTemp_cold; }
    public static float LmtTemp_stoveMin { get => reference.lmtTemp_stoveMin; }
    public static float LmtTemp_stoveMax { get => reference.lmtTemp_stoveMax; }
    public static float LmtCook_undercooked { get => reference.lmtCook_undercooked; }
    public static float LmtCook_overcooked { get => reference.lmtCook_overcooked; }

    private int currentBatch = 0;
    public static int GetNewBatch()
    {
        reference.currentBatch++;
        return reference.currentBatch;
    }





    /*
    [SerializeField] EffectUpdatedManager effectUpdatedManager;

    public static void AddEffect(EffectUpdated effect)
    {
        reference.effectUpdatedManager.AddEffect(effect);
    }
    public static void RemoveEffect(EffectUpdated effect)
    {
        reference.effectUpdatedManager.RemoveEffect(effect);
    }

    [SerializeField] GreasePool _greasePool;
    public static GreasePool greasePool { get => reference._greasePool; }
    public LayerMask oilMask;*/
}
