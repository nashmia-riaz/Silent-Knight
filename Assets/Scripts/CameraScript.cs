using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    float distanceFromPlayer;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }

    void CameraFollowPlayer()
    {
        //trignometry to keep camera a certain distance from player
        //hypoteneuse is the distance, angle is 45

        //cos a = b / h
        // b = h x cos a
        float z = distanceFromPlayer * Mathf.Cos(45 * Mathf.Deg2Rad);

        //sin a = p / h
        // p = h x sin a
        float y = distanceFromPlayer * Mathf.Sin(45 * Mathf.Deg2Rad);

        //tan a = p / b
        //b = p / tan a
        float x = z / Mathf.Tan(45 * Mathf.Deg2Rad);


        Vector3 newCameraPosition = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z - z);
        transform.position = newCameraPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with " + other.name);
        if (other.tag == "Floor")
        {
            GameManager.instance.DestroyTile(other.gameObject);
            GameManager.instance.SpawnNewTile();
        } else if (other.tag == "Wall")
        {
            GameManager.instance.DestroyWall(other.gameObject);
            GameManager.instance.SpawnNewWall();
        }
    }
}
