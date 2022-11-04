using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : ObjectSpawner
{

    //Spawn point for wall tiles
    public GameObject wallSpawn;

    //The number of wall tiles spawned at any time
    [SerializeField]
    int noOfWallsToSpawn;

    //Size of the wall tiles
    [SerializeField]
    float wallSize;

    //Pools for wall objects
    public ObjectPool wallPool, windowPool, paintingPool;

    //Which wall currently being spawned in is
    int currentWall = 0;

    //Queue for the wall tiles
    //[SerializeField]
    //List<int> WallQueued = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //Sets the forward direction according to the spawn point
        forwardDirection = wallSpawn.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initialize()
    {
        //spawns an initial number of wall tiles
        for (int i = 0; i < noOfWallsToSpawn; i++)
        {
            SpawnNewObject();
        }
    }

    public override void SpawnNewObject()
    {
        GameObject wall;
        int typeOfWall = RandomizeWallTile();
        switch (typeOfWall)
        {
            case (int)WallTypes.Window:
                wall = windowPool.GetObject();
                break;
        
            case (int)WallTypes.Wall:
                wall = wallPool.GetObject();
                break;
        
            case (int)WallTypes.Painting:
                wall = paintingPool.GetObject();
                break;

            default:
                wall = wallPool.GetObject();
                break;
        }
        Debug.Log("Spawning" + wall.name);
        wall.SetActive(true);
        wall.transform.position = wallSpawn.transform.TransformPoint(forwardDirection * (wallSize * currentWall));
        currentWall++;
    }

    int RandomizeWallTile()
    {
        float wallRoll = UnityEngine.Random.value;

        if (wallRoll <= 0.3f)
            return 1;
        if (wallRoll > 0.3f && wallRoll <= 0.7f)
            return 2;
        if (wallRoll > 0.7f)
            return 3;

        return 2;
    }
}

enum WallTypes { Wall, Window, Painting};
