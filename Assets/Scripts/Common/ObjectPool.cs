using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>  where T:Component

{
    private readonly T prefab;
    private readonly Queue<T> objects = new Queue<T>();

    public ObjectPool(T newPrefab, int initialSize)
    {
        prefab = newPrefab;

        // Pre-instantiate the objects
        for (int i = 0; i < initialSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }

    public T Get()
    {
        if (objects.Count > 0)
        {
            T obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObject = GameObject.Instantiate(prefab);
            return newObject;
        }
    }

    public void ReturnToPool(T obj,bool activity = false)
    {
        obj.gameObject.SetActive(activity);
        objects.Enqueue(obj);
    }
}
