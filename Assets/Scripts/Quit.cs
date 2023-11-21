using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Attach this script to a GameObject or an empty GameObject in your scene.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {
            Debug.Log("Exiting game");
            Application.Quit(); 
        }
    }
}
