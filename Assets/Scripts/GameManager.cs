using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager reference;
    
    private void Awake()
    {
        reference = this;
    }





    [SerializeField] private float cookingTime = 60;
    public static float CookingTime { get => reference.cookingTime; }





    [SerializeField] AssetHolder assetHolder;
    public static AssetHolder Asset { get => reference.assetHolder; }





    [SerializeField] PerformanceManager performanceManager;
    public static PerformanceManager PerformanceManager { get => reference.performanceManager; }
    public static void MakeMistake(MistakeType type)
    {
        reference.performanceManager.AddMistake(type);
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
