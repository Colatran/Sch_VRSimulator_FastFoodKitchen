using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TMP_Text text_min;
    [SerializeField] TMP_Text text_sec;

    private void Update()
    {
        int time = (int)GameManager.TaskTime;

        int min = time / 60;
        int sec = time % 60;

        if (min < 10) text_min.text = "0" + min;
        else text_min.text = min + "";

        if (sec < 10) text_sec.text = "0" + sec;
        else text_sec.text = sec + "";
    }
}
