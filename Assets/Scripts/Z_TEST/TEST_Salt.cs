using System.Collections;
using UnityEngine;

public class TEST_Salt : MonoBehaviour
{
    [SerializeField] SaltCanister canister;

    void Start()
    {
        StartCoroutine(Cicle(canister.transform.position));
    }

    IEnumerator Cicle(Vector3 position)
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            canister.transform.position = position;
            yield return delay;
        }
    }
}
