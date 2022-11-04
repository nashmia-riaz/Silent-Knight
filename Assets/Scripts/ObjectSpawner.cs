using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script spawns objects into the scene and renders them by taking them from the object pool.
/// Objects exist in the scene inside of the object pool but are disabled by default. 
/// What this script does is:
/// ObjectPool -> Object -> ObjectSpawnerQueue -> Spawn
/// It stores logic for queuing objects into the scene, and then spawns them
/// Queueing is not necessarily needed depending on your logic.
/// </summary>

public class ObjectSpawner : MonoBehaviour
{
    //Spawn point for objects
    [SerializeField]
    protected GameObject spawnPoint;

    //The forward direction of objects
    protected Vector3 forwardDirection;

    //The amount of floor tiles spawned at any time
    [SerializeField]
    protected int noOfObjectsToSpawn;

    //The size of the objects
    [SerializeField]
    protected float objectSize;

    //Which tile currently being spawned in is
    protected int currentObject = 0;

    //Queue for the objects
    [SerializeField]
    protected List<int> objectsQueued = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //Sets the forward direction according to the spawn point
        forwardDirection = spawnPoint.transform.forward;
    }

    /// <summary>
    /// function for intializing scene with objects
    /// </summary>
    public virtual void Initialize()
    {    }
    
    /// <summary>
    /// Function for spawning a new object into the scene
    /// </summary>
    public virtual void SpawnNewObject()
    {    }

    public virtual void QueueObject()
    {
    }
}