using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerNameDisplay : MonoBehaviour , Game_Interface_Data
{
    public static string playernamestr;

    //public string newPlayerName;

    public TMP_Text playerNameDisplay;


    //private void Awake()
    //{
    //    // Ensure that playerName is not null before accessing its members
    //    if (playerNameDisplay == null)
    //    {
    //        Debug.Log("PlayerNameDisplay: playerName reference is not set. Please assign it in the Unity Editor.");
    //    }
    //}

    public void Start()
    {
        // Load the player name from PlayerPrefs
        //string savedPlayerName = PlayerPrefs.GetString("PlayerName");

        // Display the player name in Scene 2
        if (!string.IsNullOrEmpty(playernamestr))
        {
            playerNameDisplay.text = "Welcome, " + playernamestr + "!";

            //Game_Data.newPlayerName = playerNameDisplay.text;
        }
        else
        {
            playerNameDisplay.text = "Player Name not set";
        }
    }

    public void LoadData(Game_Data data)
    {
        //this.savedTime = data.lastSavedTime;
        //this.newPlayerName = data.playerName;
    }

    public void SaveData(ref Game_Data data)
    {
        // We want to save the latest time before quitting game
        data.playerName = playernamestr;
        Debug.Log("Name saved!!");

    }
}


//using System.IO;
//public class PlayerNameDisplay : MonoBehaviour
//{
//    public static string playernamestr;
//    public TMP_Text playerName;

//    private void Start()
//    {
//        // Load the player name from JSON
//        string json = LoadPlayerData();
//        if (!string.IsNullOrEmpty(json))
//        {
//            // Convert the JSON data back to PlayerData object
//            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

//            // Display the player name in Scene 2
//            playerName.text = "Welcome, " + playerData.playerName + "!";
//        }
//        else
//        {
//            playerName.text = "Player Name not set";
//        }
//    }

//    private string LoadPlayerData()
//    {
//        // Load the JSON data from a file (or PlayerPrefs)
//        string filePath = Application.persistentDataPath + "/game_data.json";
//        if (File.Exists(filePath))
//        {
//            return File.ReadAllText(filePath);
//        }

//        return null;
//    }
//}
