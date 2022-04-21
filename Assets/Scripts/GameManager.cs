using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    [SerializeField] float lmtTemp_roomTemperature = 24.5f;
    [SerializeField] float lmtTemp_cold = 25f;
    [SerializeField] float lmtTemp_stoveMin = 40f;
    [SerializeField] float lmtTemp_stoveMax = 41f;
    [SerializeField] float lmtCook_undercooked = 0.9f;
    [SerializeField] float lmtCook_overcooked = 1.1f;

    public static float LmtTemp_roomTemperature { get => reference.lmtTemp_roomTemperature; }
    public static float LmtTemp_cold { get => reference.lmtTemp_cold; }
    public static float LmtTemp_stoveMin { get => reference.lmtTemp_stoveMin; }
    public static float LmtTemp_stoveMax { get => reference.lmtTemp_stoveMax; }
    public static float LmtCook_undercooked { get => reference.lmtCook_undercooked; }
    public static float LmtCook_overcooked { get => reference.lmtCook_overcooked; }






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
