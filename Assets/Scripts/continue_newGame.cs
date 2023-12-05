using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class continue_newGame : MonoBehaviour
{
    private string filepath;
    private string gamedata;

    [System.Serializable]
    public class LeaderboardWrapper
    {
        public List<NamedInt> NamedInts;
    }

    [System.Serializable]
    public class NamedInt  // Make NamedInt class public
    {
        public int Pos;
        public string Name;
        public int Value;
    }

    [System.Serializable]
    public class GameData
    {
        public int correct_trash;
        public int wrong_trash;
    }

    void MoveToScene()
    {
        // Get the path to the persistent data directory
        string persistentDataPath = Application.persistentDataPath;

        // Check if "game_data.json" exists in the specified directory
        string[] files = Directory.GetFiles(persistentDataPath);

        bool gameDataExists = Array.Exists(files, file => file.Contains("game_data.json"));

        if (gameDataExists)
        {
            Debug.Log("continue game");
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.Log("new game");
            SceneManager.LoadScene("EnterName");
            SetLeaderboardValues();
            DeleteTrashCanFile();
        }
    }

    public void SetLeaderboardValues()
    {
        string persistentDataPath = Application.persistentDataPath;
        filepath = Path.Combine(Application.persistentDataPath, "leaderboardData.json");

        // Load existing leaderboard data
        List<NamedInt> existingLeaderboardData = new List<NamedInt>();
        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(jsonData);
            if (wrapper != null)
            {
                existingLeaderboardData = wrapper.NamedInts;
            }
        }

        // Set every value to 0 for every name
        foreach (NamedInt namedInt in existingLeaderboardData)
        {
            namedInt.Value = 0;
        }

        // Save the updated leaderboard data
        LeaderboardWrapper updatedWrapper = new LeaderboardWrapper
        {
            NamedInts = existingLeaderboardData
        };

        string updatedJsonData = JsonUtility.ToJson(updatedWrapper);
        File.WriteAllText(filepath, updatedJsonData);

        Debug.Log("Leaderboard values set to 0 for every name.");
    }

    public void DeleteTrashCanFile()
    {
        filepath = Path.Combine(Application.persistentDataPath, "trash_can_counter.json");

        try
        {
            // Check if the file exists before trying to delete
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                Debug.Log("Trash can file deleted successfully: " + filepath);
            }
            else
            {
                Debug.LogWarning("Trash can file does not exist: " + filepath);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error deleting trash can file: " + e.Message);
        }
    }
}