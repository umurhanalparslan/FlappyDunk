using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize;

    private List<GameObject> pool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        // gerekirse havuzu genişlet
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }


}
