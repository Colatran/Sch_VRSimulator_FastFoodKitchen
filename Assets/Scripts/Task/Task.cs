
public static class Task
{
    public static float GetTime(TaskTime time)
    {
        switch(time)
        {
            case TaskTime.MIN5:
                return 300;

            case TaskTime.MIN10:
                return 600;

            case TaskTime.MIN15:
                return 900;

            case TaskTime.MIN30:
                return 1800;

            case TaskTime.MIN60:
                return 3600;

            default:
                return 300;
        }
    }

    public static string GetTimeName(TaskTime time)
    {
        switch (time)
        {
            case TaskTime.MIN5:
                return "5min";

            case TaskTime.MIN10:
                return "10min";

            case TaskTime.MIN15:
                return "15min";

            case TaskTime.MIN30:
                return "30min";

            case TaskTime.MIN60:
                return "60min";

            default:
                return "5min";
        }
    }

    public static string GetJobName(TaskJob job)
    {
        switch (job)
        {
            case TaskJob.BATCHER:
                return "Batcher";

            case TaskJob.PREPARADOR:
                return "Preparador";

            default:
                return "Bather";
        }
    }

    public static string GetDifficultyName(TaskDifficuty difficuty)
    {
        switch(difficuty)
        {
            case TaskDifficuty.EASY:
                return "Fácil";

            case TaskDifficuty.NORMAL:
                return "Normal";

            case TaskDifficuty.HARD:
                return "Difícil";

            default:
                return "Normal";
        }
    }
}

public enum TaskDifficuty
{
    EASY,
    NORMAL,
    HARD
}
public enum TaskTime
{
    MIN5,
    MIN10,
    MIN15,
    MIN30,
    MIN60,
}
public enum TaskJob
{
    BATCHER,
    PREPARADOR,
}
