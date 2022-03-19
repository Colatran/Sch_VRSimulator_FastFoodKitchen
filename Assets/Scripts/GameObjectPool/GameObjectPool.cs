using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    private enum PoolType
    {
        FAST,
        NONSCALABLE,
        SACALABLE,
    }

    [SerializeField] GameObject instance;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    [SerializeField] int inicialCount = 10;
    [SerializeField] PoolType poolType = PoolType.FAST;

    private void OnValidate()
    {
        if (inicialCount > objects.Count)
        {
            while (objects.Count < inicialCount)
            {
                GameObject _object = Instantiate(instance, parent);
                _object.SetActive(false);
                objects.Add(_object);
            }
        }
        else if (inicialCount < objects.Count)
        {
            GameObject gObject = objects[0];
            objects.Remove(gObject);
            Destroy(gObject);
        }
    }


    public List<GameObject> Objects { get => objects; }



    private int currentIndex = 0;

    public GameObject GetObject()
    {
        if (poolType == PoolType.FAST) return FastGetter();
        else if (poolType == PoolType.NONSCALABLE) return NonScalableGetter();
        else return ScalableGetter();
    }

    private GameObject FastGetter()
    {
        currentIndex++;
        if (currentIndex == objects.Count) currentIndex = 0;

        return objects[currentIndex];
    }
    private GameObject NonScalableGetter()
    {
        foreach (GameObject _object in objects)
        {
            if (!_object.activeSelf) return _object;
        }

        return FastGetter();
    }
    private GameObject ScalableGetter()
    {
        foreach (GameObject _object in objects)
        {
            if (!_object.activeSelf) return _object;
        }

        return Instantiate(instance, transform);
    }


    public void DisableAllObjects()
    {
        foreach (GameObject _object in objects) _object.SetActive(false);
    }
}
