using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TaskData taskData;
    [SerializeField] PerformanceManager performanceManager;
    [SerializeField] Orderer orderer;

    [Header("Player")]
    [SerializeField] Transform playerMainCameraTransform;
    [SerializeField] XRMovementManager playerMovementManager;

    [Header("PopUps")]
    [SerializeField] UIPopUp uIPopUp_Mistake;
    [SerializeField] UIPopUp uIPopUp_InitialStats;
    [SerializeField] UIPopUp uIPopUp_FinalStats;
    [SerializeField] UIPopUp uIPopUp_PauseMenu;

    [Header("Cooking")]
    [SerializeField] float lmtTemp_roomTemperature = 24.5f;
    [SerializeField] float lmtTemp_cold = 25f;
    [SerializeField] float lmtTemp_stoveMin = 40f;
    [SerializeField] float lmtTemp_stoveMax = 41f;
    [SerializeField] float lmtCook_undercooked = 0.9f;
    [SerializeField] float lmtCook_overcooked = 1.25f;

    private bool running = false;
    private float taskTime = 0;
    private int totalDirt = 0;
    private float orderDelay = 60;
    private float nextOrderTime = 0;

    private void Awake()
    {
        reference = this;
    }

    private void Start()
    {
        taskTime = Task.GetTime(taskData.Time);
        orderDelay = Task.GetOrderTime(taskData.OrderTime);
        uIPopUp_InitialStats.Open();
    }

    private void Update()
    {
        if (running)
        {
            Update_CountTime();
            Update_MakeOrder();
        }
    }

    private void Update_CountTime()
    {
        float deltaTime = Time.deltaTime;
        taskTime -= deltaTime;

        if (taskTime < 0)
        {
            taskTime = 0;
            ReferenceFinishTask();
        }
    }
    private void Update_MakeOrder()
    {
        nextOrderTime -= Time.deltaTime;

        if (nextOrderTime < 0)
        {
            orderer.MakeOrder();
            nextOrderTime = orderDelay;
        }
    }

    private void ReferenceMakeMistake(MistakeType type) {
        reference.performanceManager.AddMistake(type);
        uIPopUp_Mistake.Open();
    }
    private void ReferenceStartTask()
    {
        SetRunning(true);
        playerMovementManager.Unlock();
    }
    private void ReferenceFinishTask()
    {
        SetRunning(false);
        playerMovementManager.Lock();
        uIPopUp_FinalStats.Open();
    }

    public void SetRunning(bool running)
    {
        if (taskData.Job == TaskJob.TUTORIAL) this.running = false;
        this.running = running;
    }


    private void ReferenceQuitToMainMenu()
    {
        LevelManager.Instance.LoadScene(0);
    }





    public static GameManager reference;
    public static PerformanceManager PerformanceManager { get => reference.performanceManager; }
    public static Transform PlayerMainCameraTransform { get => reference.playerMainCameraTransform; }
    public static XRMovementManager PlayerMovementManager { get => reference.playerMovementManager; }
    public static UIPopUp UIPopUp_PauseMenu { get => reference.uIPopUp_PauseMenu; }

    public static float LmtTemp_roomTemperature { get => reference.lmtTemp_roomTemperature; }
    public static float LmtTemp_cold { get => reference.lmtTemp_cold; }
    public static float LmtTemp_stoveMin { get => reference.lmtTemp_stoveMin; }
    public static float LmtTemp_stoveMax { get => reference.lmtTemp_stoveMax; }
    public static float LmtCook_undercooked { get => reference.lmtCook_undercooked; }
    public static float LmtCook_overcooked { get => reference.lmtCook_overcooked; }

    public static TaskData TaskData { get => reference.taskData; }
    public static int TotalServed { get => reference.orderer.TotalServed(); }
    public static float TaskTime { get => reference.taskTime; }
    public static int TotalDirt { get => reference.totalDirt; }

    public static bool Running { get => reference.running; }



    public static void AddDirt() => reference.totalDirt++;
    public static void RemoveDirt() => reference.totalDirt--;

    public static void MakeMistake(MistakeType type) => reference.ReferenceMakeMistake(type);
    public static void StartTask() => reference.ReferenceStartTask();
    public static void FinishTask() => reference.ReferenceFinishTask();
    public static void QuitToMainMenu() => reference.ReferenceQuitToMainMenu();
}
