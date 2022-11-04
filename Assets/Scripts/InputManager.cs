using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.hasGameStarted && Input.GetMouseButtonDown(0))
        {
            GameManager.instance.StartGame();
        }

        else if (Input.GetMouseButtonDown(0) && GameManager.instance.hasGameStarted)
        {
            PlayerScript.instance.Jump();
        }    
    }
}
