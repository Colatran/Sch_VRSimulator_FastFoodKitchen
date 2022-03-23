using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] UIPopUpResponsiveness responsiveness;


    private bool up = false;
    public bool isUp { get => up; }

    public void PopUp()
    {
        if (up) return;
        up = true;

        SetObjectsActive(true);

        if (responsiveness != null)
            responsiveness.PopUp();
    }
    public void PopOff()
    {
        if (!up) return;
        up = false;

        SetObjectsActive(false);

        if (responsiveness != null)
            responsiveness.PopOff();
    }

    private void SetObjectsActive(bool active)
    {
        foreach (GameObject _object in objects)
        {
            _object.SetActive(active);
        }
    }
}
