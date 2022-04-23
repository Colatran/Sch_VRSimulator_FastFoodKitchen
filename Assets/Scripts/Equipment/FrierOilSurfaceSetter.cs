using System.Collections;
using UnityEngine;

public class FrierOilSurfaceSetter : MonoBehaviour
{
    [SerializeField] MaterialPropertyController_RandomOffset[] materialProperties;
    [SerializeField] float frameTime = 0.066666666f;


    private void OnEnable()
    {
        StartCoroutine(SetMaterialProperties(frameTime));
    }


    private IEnumerator SetMaterialProperties(float delayTime)
    {
        WaitForSeconds delay = new WaitForSeconds(delayTime);

        while (gameObject.activeSelf)
        {
            yield return delay;

            foreach (MaterialPropertyController_RandomOffset materialProperty in materialProperties)
                materialProperty.Set();
        }
    }
}
