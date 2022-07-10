using UnityEngine;

public class OrientationChecker_Downwards : OrientationChecker
{
    [SerializeField]
    [Range(-1, 1)] 
    float minDot = 0.85f;

    public override bool Check(Transform normalTransform)
    {
        bool res = minDot < Vector3.Dot(
            normalTransform == null ? Vector3.down : -normalTransform.up,
            transform.up
            );

        return res;
    }
}
