using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ //game manager is a singleton
    public static GameManager instance;

    [SerializeField]
    [Range(0, 1)]
    public float difficulty;

    float waitBeforeStart = 0.5f;
    float timer = 0f;

    public bool hasGameStarted = false;
    bool initializedTiles = false;

    PlayerScript playerScript;

    [SerializeField]
    TileSpawner tileSpawner;

    [SerializeField]
    WallSpawner wallSpawner;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Called when game is started (when user taps the screen). Initializes the UI and player
    /// </summary>
    public void StartGame()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        if (!hasGameStarted)
        {
            hasGameStarted = true;
            playerScript.StartRunning();
            UIHandler.instance.OnGameStart(playerScript.health);
        }
    }

    /// <summary>
    /// Called when camera collider hits game elements. This function 'destroys' or deactivates them from the pool.
    /// </summary>
    /// <param name="tile"></param>
    public void DestroyTile(GameObject tile)
    {
        if (tile.gameObject.GetComponentInChildren<BloodDecal>())
        {
            //reset the tile decal so when it spawns again, there are no decals on it
            tile.gameObject.GetComponentInChildren<BloodDecal>().DecalOff();
        }
        tile.transform.parent.gameObject.SetActive(false);
        tileSpawner.QueueObject();
    }

    /// <summary>
    /// Called when camera collider hits walls. It removes the wall and puts it back in the pool.
    /// </summary>
    public void DestroyWall(GameObject wall)
    {
        wall.transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Spawns a new tile ahead of the player
    /// </summary>
    public void SpawnNewTile()
    {
        tileSpawner.SpawnNewObject();
    }

    /// <summary>
    /// Spawns a new wall ahead of the player
    /// </summary>
    public void SpawnNewWall()
    {
        wallSpawner.SpawnNewObject();
    }

    /// <summary>
    /// called when game over is triggered
    /// </summary>
    public void FailState()
    {
        //fail state stuff
        UIHandler.instance.OnGameOver();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //timer function to wait slightly for object pooling before spawning the tiles for player
        if (timer >= waitBeforeStart && !initializedTiles)
        {
            initializedTiles = true;
            tileSpawner.Initialize();
            wallSpawner.Initialize();
        }
    }

    /// <summary>
    /// called when player loses health
    /// </summary>
    /// <param name="health">current health of player</param>
    public void LoseHealth(int health)
    {
        UIHandler.instance.OnLoseHealth(health);
    }
}
