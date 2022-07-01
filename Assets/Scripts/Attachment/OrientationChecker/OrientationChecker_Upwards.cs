using UnityEngine;

public class OrientationChecker_Upwards : OrientationChecker
{
    [SerializeField]
    [Range(-1, 1)]
    float maxDot = -0.85f;


    public override bool Check(Transform normalTransform) =>
        maxDot > Vector3.Dot(
            normalTransform == null ? Vector3.down : -normalTransform.up,
            transform.up
            );
}
