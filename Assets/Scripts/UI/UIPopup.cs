using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] bool setInCameraPosition = false;
    [SerializeField] Transform Camera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float initialDistance = .75f;
    [SerializeField] float hitDistanceOffset = .1f;


    public delegate void Action();
    public event Action OnPopUp;
    public event Action OnPopOff;



    private void SetInCameraPosition()
    {
        Vector3 direction = Camera.forward;
        Vector3 position = Camera.position;
        float distance = initialDistance;

        Ray ray = new Ray();
        ray.direction = direction;
        ray.origin = position;

        RaycastHit hitInfo;
        bool hit = Physics.Raycast(ray, out hitInfo, .75f + hitDistanceOffset, layerMask);
        if(hit)
        {
            distance = Vector3.Distance(position, hitInfo.point) - hitDistanceOffset;
        }

        Vector3 finalPosition = position + direction.normalized * distance;
        transform.position = finalPosition;
    }

    private void SetObjectsActive(bool active)
    {
        foreach (GameObject _object in objects)
        {
            _object.SetActive(active);
        }
    }



    public void PopUp()
    {
        if(setInCameraPosition) SetInCameraPosition();
        SetObjectsActive(true);

        if (OnPopUp != null)
            OnPopUp();
    }
    public void PopOff()
    {
        SetObjectsActive(false);

        if (OnPopOff != null)
            OnPopOff();
    }

}
