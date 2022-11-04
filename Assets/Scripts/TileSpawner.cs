using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : ObjectSpawner
{
    //Pools for the floor tiles
    [SerializeField]
    public ObjectPool floorTilesPool, easyTrapPool, mediumTrapPool, hardTrapPool;

    public override void Initialize()
    {
        for (int i = 0; i < noOfObjectsToSpawn; i++)
        {
            objectsQueued.Add((int)TileTypes.Floor);
        }

        Debug.Log("Initializing floor " + objectsQueued.Count);

        for (int i = 0; i < noOfObjectsToSpawn; i++)
        {
            SpawnNewObject();
        }
    }

    public override void SpawnNewObject()
    {
        GameObject tile;

        if (objectsQueued.Count > 0)
        {
            switch (objectsQueued[0])
            {
                case (int)TileTypes.Floor:
                    tile = floorTilesPool.GetObject();
                    break;

                case (int)TileTypes.EasyTrap:
                    tile = easyTrapPool.GetObject();
                    break;

                case (int)TileTypes.MediumTrap:
                    tile = mediumTrapPool.GetObject();
                    break;

                case (int)TileTypes.HardTrap:
                    tile = hardTrapPool.GetObject();
                    break;

                default:
                    tile = floorTilesPool.GetObject();
                    break;
            }

            tile.SetActive(true);
            tile.transform.position = spawnPoint.transform.TransformPoint(forwardDirection * (objectSize * currentObject));
            currentObject++;

            objectsQueued.RemoveAt(0);
        }
    }

    public override void QueueObject()
    {
        float floorOrTrap = UnityEngine.Random.value;

        if (floorOrTrap >= GameManager.instance.difficulty)
        {
            QueueFloorTile();
        }
        else
        {
            QueueTrapTile();
        }
    }

    //-----functions particular to floors and tiles--------------
    void QueueTrapTile()
    {
        int typeOfTile = RandomizeTrapTile();
        //GameObject trapTile;

        switch (typeOfTile)
        {
            //spawn easy trap tile that is 1 tile long
            case (int)TileTypes.EasyTrap:
                objectsQueued.Add((int)TileTypes.EasyTrap);
                break;

            //spawn medium trap tile that is 2 tiles long
            case (int)TileTypes.MediumTrap:
                for (int i = 0; i < 2; i++)
                    objectsQueued.Add((int)TileTypes.MediumTrap);
                break;

            //spawn hard trap tile that is 3 tiles long
            case (int)TileTypes.HardTrap:
                for (int i = 0; i < 3; i++)
                    objectsQueued.Add((int)TileTypes.HardTrap);
                break;

        }

        objectsQueued.Add((int)TileTypes.Floor);
    }

    void QueueFloorTile()
    {
        objectsQueued.Add((int)TileTypes.Floor);
    }
    int RandomizeTrapTile()
    {
        float randomTile = UnityEngine.Random.value;

        if (randomTile <= 0.5f)
            return 1;

        if (randomTile <= 0.85f)
            return 2;

        return 3;
    }
    //-----------------------------------------------------
}

enum TileTypes { Floor, EasyTrap, MediumTrap, HardTrap };