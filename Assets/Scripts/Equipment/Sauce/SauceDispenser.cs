using UnityEngine;

public class SauceDispenser : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Button3D button;

    private void OnEnable()
    {
        button.OnPressed += Dispense;
    }
    private void OnDisable()
    {
        button.OnPressed -= Dispense;
    }

    private void Dispense()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
