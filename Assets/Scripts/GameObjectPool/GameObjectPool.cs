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
    [SerializeField] List<PoolObject> objects = new List<PoolObject>();
    [SerializeField] int inicialCount = 10;
    [SerializeField] PoolType poolType = PoolType.FAST;

    private void OnValidate()
    {
        if (inicialCount > objects.Count)
        {
            while (objects.Count < inicialCount)
            {
                GameObject _object = PrefabUtility.InstantiatePrefab(prefab, container) as GameObject;
                _object.SetActive(false);
                objects.Add(_object.GetComponent<PoolObject>());
            }
        }
        else if (inicialCount < objects.Count)
        {
            PoolObject poolObject = objects[0];
            objects.Remove(poolObject);
            Destroy(poolObject.gameObject);
        }
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

    public void DiactiveAllObjects()
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
