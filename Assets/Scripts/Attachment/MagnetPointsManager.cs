using UnityEngine;

public class MagnetPointsManager : MonoBehaviour
{
    [SerializeField] Attachment Attachment;
    [SerializeField] MagnetPoint[] points;
    [SerializeField] int minPoints = 0;

    private int currentPoints = 0;

    public void PointInArea()
    {
        currentPoints++;

        if (Attachment.Attached) return;

        if (currentPoints >= minPoints) CheckPoints();
    }
    public void PointExitArea()
    {
        currentPoints--;
    }


    public void CheckPoints()
    {

    }
}
