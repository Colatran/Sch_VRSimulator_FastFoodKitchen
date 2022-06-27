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


    [SerializeField] Transform mainCameraTransform;
    public static Transform MainCameraTransform { get => reference.mainCameraTransform; }


    [SerializeField] UIPopUp pauseMenu;
    public static UIPopUp PauseMenu { get => reference.pauseMenu; }





    [Header("Cooking")]
    [SerializeField] float lmtTemp_roomTemperature = 24.5f;
    [SerializeField] float lmtTemp_cold = 25f;
    [SerializeField] float lmtTemp_stoveMin = 40f;
    [SerializeField] float lmtTemp_stoveMax = 41f;
    [SerializeField] float lmtCook_undercooked = 0.9f;
    [SerializeField] float lmtCook_overcooked = 1.25f;

    public static float LmtTemp_roomTemperature { get => reference.lmtTemp_roomTemperature; }
    public static float LmtTemp_cold { get => reference.lmtTemp_cold; }
    public static float LmtTemp_stoveMin { get => reference.lmtTemp_stoveMin; }
    public static float LmtTemp_stoveMax { get => reference.lmtTemp_stoveMax; }
    public static float LmtCook_undercooked { get => reference.lmtCook_undercooked; }
    public static float LmtCook_overcooked { get => reference.lmtCook_overcooked; }

    [SerializeField] LayerMask oilMask;
    public static LayerMask OilMask { get => reference.oilMask; }





    [Header("Stats")]
    [SerializeField] TaskData taskData;
    public static TaskData TaskData { get => reference.taskData; }

    [SerializeField] Orderer orderer;
    public static int TotalServed { get => reference.orderer.TotalServed(); }

    private int totalDirt = 0;
    public static int TotalDirt { get => reference.totalDirt; }
    public static void AddDirt() { reference.totalDirt++; }
    public static void RemoveDirt() { reference.totalDirt--; }
}
