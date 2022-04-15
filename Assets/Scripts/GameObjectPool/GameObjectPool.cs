using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameObjectPool : MonoBehaviour
{
    private enum PoolType
    {
        FAST,
        NONSCALABLE,
        SACALABLE,
    }

    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;

    [Tooltip(
        "Fast - Gets object acording to a index\n" +
        "NonScalable - Gets first incative object\n" +
        "Scalable - If ran out of objects instanciates another\n")]
    [SerializeField] PoolType poolType = PoolType.FAST;
    [Header("")]
    [SerializeField] int initialCount = 0;
    [SerializeField] List<PoolObject> objects = new List<PoolObject>();

    public Transform Container { get => container; }
    public List<PoolObject> Objects { get => objects; }


    private void OnValidate()
    {
        if (initialCount > objects.Count)
        {
            GetAllExistingObjects();

            while (objects.Count < initialCount)
            {
                InstanciateObject();
            }
        }
        else if (initialCount < objects.Count)
        {
            GetAllExistingObjects();
        }
    }

    private void GetAllExistingObjects()
    {
        objects.Clear();
        objects.RemoveAll(x => x == null);
        objects.AddRange(container.GetComponentsInChildren<PoolObject>());
    }

    private PoolObject InstanciateObject()
    {
        GameObject _object;

#if UNITY_EDITOR
        _object = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
#else
        _object = Instantiate(prefab, container);
#endif

        _object.SetActive(false);

        PoolObject pObject = _object.GetComponent<PoolObject>();
        pObject.Pool = this;
        Objects.Add(pObject);
        return pObject;
    }





    private int currentIndex = 0;

    public GameObject GetObject()
    {
        PoolObject pObject;
        if (poolType == PoolType.FAST) pObject = FastGetter();
        else if (poolType == PoolType.NONSCALABLE) pObject = NonScalableGetter();
        else pObject = ScalableGetter();

        if (pObject != null) pObject.Enable();
        return pObject.gameObject;
    }

    private PoolObject FastGetter()
    {
        currentIndex++;
        if (currentIndex == objects.Count) currentIndex = 0;

        return objects[currentIndex];
    }
    private PoolObject NonScalableGetter()
    {
        foreach (PoolObject pObject in objects)
        {
            if (!pObject.gameObject.activeSelf) return pObject;
        }

        return FastGetter();
    }
    private PoolObject ScalableGetter()
    {
        foreach (PoolObject pObject in objects)
        {
            if (!pObject.gameObject.activeSelf) return pObject;
        }

        return InstanciateObject();
    }





    public void DisableObject(PoolObject poolObject)
    {
        poolObject.Disable();
        poolObject.gameObject.SetActive(false);
        poolObject.gameObject.transform.SetParent(container);
    }

    public void DisableAllObjects()
    {
        foreach (PoolObject poolObject in objects) 
        {
            DisableObject(poolObject);
        }
    }
}
