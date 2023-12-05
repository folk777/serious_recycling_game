using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//public class PlayerNameInput : MonoBehaviour
//{
//    public TMP_InputField playerName;

//    public void EnterName_StartGame()
//    {

//        Debug.Log("valueeee::::" + playerName.text);
//        PlayerNameDisplay.playernamestr = playerName.text;

//        SceneManager.LoadScene("SampleScene");
//    }

//    public void BacktoMainMenu()
//    {
//        SceneManager.LoadScene("MainMenu");
//    }
//}

//using System.IO;

//[System.Serializable]
//public class PlayerData
//{
//    public string playerName;
//}

//public class PlayerNameInput : MonoBehaviour, Game_Interface_Data
//{
//    public TMP_InputField playerNameInput;

//    public void EnterName_StartGame()
//    {
//        // Create a new PlayerData object and set the player name
//        PlayerData playerData = new PlayerData();
//        playerData.playerName = playerNameInput.text;

//        // Convert the PlayerData object to JSON
//        string json = JsonUtility.ToJson(playerData);

//        // Save the JSON data to a file (or PlayerPrefs)
//        //SavePlayerData(json);

//        // Set the playernamestr in PlayerNameDisplay for displaying in the next scene
//        PlayerNameDisplay.playernamestr = playerNameInput.text;

//        // Load the next scene
//        SceneManager.LoadScene("SampleScene");
//    }

//    public void BacktoMainMenu()
//    {
//        SceneManager.LoadScene("MainMenu");
//    }

//    //private void SavePlayerData(string json)
//    //{
//    //    // Save the JSON data to a file (or PlayerPrefs)
//    //    string filePath = Application.persistentDataPath + "/game_data.json";
//    //    File.WriteAllText(filePath, json);

//    //    Debug.Log("Player data saved to: " + filePath);
//    //}

//    public void LoadData(Game_Data data)
//    {
//        //this.savedTime = data.lastSavedTime;
//    }

//    public void SaveData(ref Game_Data data)
//    {
//        // We want to save the latest time before quitting game
//        data.playerName = playerNameInput.text;
//        Debug.Log("Name saved!!");

//    }
//}



public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    public void EnterName_StartGame()
    {
        // Get the player's input
        string playerName = playerNameInput.text;

        // Enforce the character limit (assuming you want to truncate the input)
        if (playerName.Length <= 12 && playerName.Length >3)
        {
            // Log the value (optional)
            Debug.Log("Player Name: " + playerName);

            // Set the player name for display
            PlayerNameDisplay.playernamestr = playerName;

            // Load the next scene
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            // Display an error message or handle the situation where the character limit is exceeded
            Debug.LogWarning("Character limit exceeded. Please enter a name with 14 characters or less.");
        }
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}