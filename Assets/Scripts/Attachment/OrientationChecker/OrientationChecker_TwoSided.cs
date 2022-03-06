using UnityEngine;

public class OrientationChecker_TwoSided : OrientationChecker
{
    [SerializeField]
    [Range(0, 1)] 
    float maxCross = .6f;


    public override bool Check(Transform normalTransform) =>
        maxCross > Vector3.Cross(
            normalTransform == null ? Vector3.down : -normalTransform.up,
            transform.up
            ).magnitude;
}
