
public static class Task
{
    public static string GetJobName(TaskJob job)
    {
        switch (job)
        {
            //case TaskJob.TUTORIAL:
            //    return "Tutorial";

            case TaskJob.BATCHER:
                return "Batcher";

            case TaskJob.PREPARADOR:
                return "Preparador";

            default:
                return "Batcher";
        }
    }

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

            case TaskTime.MIN45:
                return 2700;

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

            case TaskTime.MIN45:
                return "45min";

            case TaskTime.MIN60:
                return "60min";

            default:
                return "5min";
        }
    }

    public static float GetOrderTime(TaskOrderTime time, TaskJob job)
    {
        if(job == TaskJob.PREPARADOR)
        {
            switch (time)
            {
                case TaskOrderTime.SLOW:
                    return 80;

                case TaskOrderTime.MEDIUM:
                    return 40;

                case TaskOrderTime.FAST:
                    return 20;

                default:
                    return 40;
            }
        }
        else
        {
            switch (time)
            {
                case TaskOrderTime.SLOW:
                    return 120;

                case TaskOrderTime.MEDIUM:
                    return 60;

                case TaskOrderTime.FAST:
                    return 30;

                default:
                    return 60;
            }
        }
    }
    public static string GetOrderTimeName(TaskOrderTime time)
    {
        switch (time)
        {
            case TaskOrderTime.FAST:
                return "Rápido";

            case TaskOrderTime.MEDIUM:
                return "Moderado";

            case TaskOrderTime.SLOW:
                return "Lento";

            default:
                return "Moderado";
        }
    }

    public static string GetDifficultyName(TaskDifficuty difficuty)
    {
        switch(difficuty)
        {
            case TaskDifficuty.EASY:
                return "Aprendiz";

            case TaskDifficuty.NORMAL:
                return "Treino";

            case TaskDifficuty.HARD:
                return "Simulação";

            default:
                return "Aprendiz";
        }
    }
}

public enum TaskJob
{
    //TUTORIAL,
    BATCHER,
    PREPARADOR,
}
public enum TaskTime
{
    MIN5,
    MIN10,
    MIN15,
    MIN30,
    MIN45,
    MIN60,
}
public enum TaskOrderTime
{
    SLOW,
    MEDIUM,
    FAST,
}
public enum TaskDifficuty
{
    EASY,
    NORMAL,
    HARD
}

