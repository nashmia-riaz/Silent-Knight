using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class is used to initialize objects in a pool at the start of the game. Can initialize any type of object given a max number and stores their references in a list. 
/// </summary>
public class ObjectPool : MonoBehaviour
{
    //list to keep track of object pool
    public List<GameObject> objectPool = new List<GameObject>();

    //object to initialize
    [SerializeField]
    GameObject objectPrefab;

    //number of objects to initialize
    [SerializeField]
    public int noOfObjectsToPool;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate objects at start and keep them hidden
        for(int i = 0; i < noOfObjectsToPool; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            objectPool.Add(obj);
        }
    }

    /// <summary>
    /// Returns an object not currently active in scene
    /// </summary>
    /// <returns>object unhidden and to be used</returns>
    public GameObject GetObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }
}
