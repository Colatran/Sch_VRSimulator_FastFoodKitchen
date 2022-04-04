using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    [SerializeField] PoolType poolType = PoolType.FAST;
    [Header("")]
    [SerializeField] int initialCount = 0;
    [SerializeField] List<PoolObject> objects = new List<PoolObject>();

    private void OnValidate()
    {
        if (initialCount > objects.Count)
        {
            GetAllExistingObjects();

            while (objects.Count < initialCount)
            {
                GameObject _object = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
                _object.SetActive(false);
                objects.Add(_object.GetComponent<PoolObject>());
            }
        }
        else if (initialCount < objects.Count)
        {
            GetAllExistingObjects();
        }
    }
    private void GetAllExistingObjects()
    {
        objects.RemoveAll(x => x == null);
        objects.AddRange(container.GetComponentsInChildren<PoolObject>());
    }


    public Transform Container { get => container; }
    public List<PoolObject> Objects { get => objects; }



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

        return AddObject();
    }

    private PoolObject AddObject()
    {
        PoolObject pObject = Instantiate(prefab, container).GetComponent<PoolObject>();
        Objects.Add(pObject);
        return pObject;
    }

    public void DisableAllObjects()
    {
        foreach (PoolObject poolObject in objects) 
        {
            DisableObject(poolObject);
        }
    }

    public void DisableObject(PoolObject poolObject)
    {
        poolObject.Disable();
        poolObject.gameObject.SetActive(false);
        poolObject.gameObject.transform.SetParent(container);
    }
}
