using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AssetHolder assetHolder;
    [SerializeField] TaskData taskData;
    [SerializeField] PerformanceManager performanceManager;
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] XRMovementManager playerMovementManager;
    [SerializeField] UIPopUp pauseMenu;
    [SerializeField] Orderer orderer;

    [Header("Notifications")]
    [SerializeField] Notification notification_Mistake;
    [SerializeField] Notification notification_InitialStats;
    [SerializeField] Notification notification_FinalStats;

    [Header("Cooking")]
    [SerializeField] float lmtTemp_roomTemperature = 24.5f;
    [SerializeField] float lmtTemp_cold = 25f;
    [SerializeField] float lmtTemp_stoveMin = 40f;
    [SerializeField] float lmtTemp_stoveMax = 41f;
    [SerializeField] float lmtCook_undercooked = 0.9f;
    [SerializeField] float lmtCook_overcooked = 1.25f;
    [SerializeField] LayerMask oilMask;

    private bool started = false;
    private float taskTime = 0;
    private int totalDirt = 0;



    private void Awake()
    {
        reference = this;
    }

    private void Start()
    {
        taskTime = Task.GetTime(taskData.taskTime);

        notification_InitialStats.Open();
        ///
        ///
        started = true;
    }

    private void Update()
    {
        if (started) Update_CountTime();
    }


    private void Update_CountTime()
    {
        float deltaTime = Time.deltaTime * 100;
        taskTime -= deltaTime;

        if (taskTime < 0)
        {
            taskTime = 0;
            ReferenceFinishTask();
        }
    }

    private void ReferenceMakeMistake(MistakeType type) {
        reference.performanceManager.AddMistake(type);
        notification_Mistake.Open();
    }

    private void ReferenceStartTask()
    {
        started = true;

        playerMovementManager.Unlock();
    }

    private void ReferenceFinishTask()
    {
        started = false;

        playerMovementManager.Lock();

        notification_FinalStats.Open();
    }





    public static GameManager reference;

    public static AssetHolder Asset { get => reference.assetHolder; }
    public static PerformanceManager PerformanceManager { get => reference.performanceManager; }
    public static Transform MainCameraTransform { get => reference.mainCameraTransform; }
    public static UIPopUp PauseMenu { get => reference.pauseMenu; }

    public static float LmtTemp_roomTemperature { get => reference.lmtTemp_roomTemperature; }
    public static float LmtTemp_cold { get => reference.lmtTemp_cold; }
    public static float LmtTemp_stoveMin { get => reference.lmtTemp_stoveMin; }
    public static float LmtTemp_stoveMax { get => reference.lmtTemp_stoveMax; }
    public static float LmtCook_undercooked { get => reference.lmtCook_undercooked; }
    public static float LmtCook_overcooked { get => reference.lmtCook_overcooked; }

    public static LayerMask OilMask { get => reference.oilMask; }
    public static TaskData TaskData { get => reference.taskData; }
    public static int TotalServed { get => reference.orderer.TotalServed(); }
    public static float TaskTime { get => reference.taskTime; }
    public static int TotalDirt { get => reference.totalDirt; }



    public static void AddDirt() => reference.totalDirt++;
    public static void RemoveDirt() => reference.totalDirt--;

    public static void MakeMistake(MistakeType type) => reference.ReferenceMakeMistake(type);

    public static void StartTask() => reference.ReferenceStartTask();
    public static void FinishTask() => reference.ReferenceFinishTask();
}
