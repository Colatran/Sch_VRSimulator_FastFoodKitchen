using UnityEngine;
using UnityEngine.UI;

public class TeleportBlinkCanvas : MonoBehaviour
{
    [SerializeField] Image[] images;

    public void SetAlpha(float alpha)
    {
        Color color = new Color(0, 0, 0, alpha);

        foreach (Image image in images)
        {
            image.color = color;
        }
    }
}
