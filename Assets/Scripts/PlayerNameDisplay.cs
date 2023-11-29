using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    public static string playernamestr;

    public TMP_Text playerName;


    private void Awake()
    {
        // Ensure that playerName is not null before accessing its members
        if (playerName == null)
        {
            Debug.Log("PlayerNameDisplay: playerName reference is not set. Please assign it in the Unity Editor.");
        }
    }

    public void Start()
    {
        // Load the player name from PlayerPrefs
        //string savedPlayerName = PlayerPrefs.GetString("PlayerName");

        // Display the player name in Scene 2
        if (!string.IsNullOrEmpty(playernamestr))
        {
            playerName.text = "Welcome, " + playernamestr + "!";
        }
        else
        {
            playerName.text = "Player Name not set";
        }
    }
}
