using UnityEngine;

public class UIFaceTarget : MonoBehaviour
{
    [SerializeField] bool lockX = false;
    [SerializeField] bool lockY = false;
    [SerializeField] bool lockZ = false;

    [Header("Target")]
    [SerializeField] Transform target;

    private void Start()
    {
        if (target == null) target = GameManager.PlayerMainCameraTransform;
    }

    private void Update()
    {
        transform.LookAt(target);
        Quaternion currRotation = transform.rotation;

        float x;
        if(lockX) x = 0;
        else x = currRotation.x;

        float y;
        if (lockY) y = 0;
        else y = currRotation.y;

        float z;
        if (lockZ) z = 0;
        else z = currRotation.z;

        transform.rotation = new Quaternion(x, y, z, currRotation.w);
    }
}
