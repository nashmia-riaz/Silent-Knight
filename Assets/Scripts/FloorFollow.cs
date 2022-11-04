using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 this script exists as a single floor collider for the player
 this is to avoid the player colliding with individual tiles and bumping up and down.
*/
public class FloorFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = spawnPoint.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
