using UnityEngine;

public class Interactible_SpawnerCookedListener : MonoBehaviour
{
    [SerializeField] Interactible_Spawner spawner;

    private void OnEnable()
    {
        spawner.OnSpawn += OnSpawn;
    }
    private void OnDisable()
    {
        spawner.OnSpawn -= OnSpawn;
    }


    private void OnSpawn(GameObject gObject)
    {
        gObject.GetComponent<Item_Cookable>().SetCooked();
    }
}
