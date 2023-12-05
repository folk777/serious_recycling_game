using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    [System.Serializable]
    private class NamedInt
    {
        public int Pos;
        public string Name;
        public int Value;
    }

    private List<NamedInt> leaderboardData;
    private bool initialized = false;
    private float nextIncreaseTime;

    private string filepath;
    public Text leaderboardText; // Reference to the UI Text component

    [System.Serializable]
    private class LeaderboardWrapper
    {
        public List<NamedInt> NamedInts;
    }

    [System.Serializable]
    private class GameData
    {
        public int playerLevel;
        public int playerPoints;
        public int playerTotalPoints;
        public float barFillAmount;
        public string playerName;
        public long lastSavedTime;
        // Add other fields from game_data.json as needed
    }

    private string playerUsername;
    private int playerAccumulatedPoints;

    void Start()
    {
        // Get the path to the persistent data directory
        string persistentDataPath = Application.persistentDataPath;
        string gameDataPath = Path.Combine(Application.persistentDataPath, "game_data.json");
        filepath = Path.Combine(Application.persistentDataPath, "leaderboardData.json");

        CreateIfNotExists();

        // Load and print the content of the game_data.json file
        if (File.Exists(gameDataPath))
        {
            string gameDataJson = File.ReadAllText(gameDataPath);
            Debug.Log("Game Data JSON:\n" + gameDataJson);

            // Now you can parse the gameDataJson if needed
            GameData gameData = JsonUtility.FromJson<GameData>(gameDataJson);

            // Assign player data from game_data.json
            playerUsername = gameData.playerName;
            playerAccumulatedPoints = gameData.playerTotalPoints;
        }
        else
        {
            Debug.LogError("Game Data file not found.");
        }

        LoadData();

        if (leaderboardData == null || leaderboardData.Count == 0)
        {
            leaderboardData = new List<NamedInt>
            {
                new NamedInt { Pos = 1, Name = "Dwight", Value = 110 },
                new NamedInt { Pos = 2, Name = "Michael", Value = 220 },
                new NamedInt { Pos = 3, Name = "Pam", Value = 330 },
                new NamedInt { Pos = 4, Name = "Jim", Value = 450 }
            };
            leaderboardData.Add(new NamedInt { Pos = 5, Name = playerUsername, Value = playerAccumulatedPoints });

        }
        else
        {
            foreach (var defaultNamedInt in leaderboardData)
            {
                var loadedNamedInt = leaderboardData.Find(x => x.Name.Equals(defaultNamedInt.Name, System.StringComparison.OrdinalIgnoreCase));
                if (loadedNamedInt != null)
                {
                    defaultNamedInt.Value = loadedNamedInt.Value;
                }
            }
            leaderboardData.Add(new NamedInt { Pos = 5, Name = playerUsername, Value = playerAccumulatedPoints });
        }

        initialized = true;

        nextIncreaseTime = Time.time + 3600; // 3600 seconds in 1 hour

        leaderboardData = leaderboardData.OrderByDescending(x => x.Value).ToList();

        UpdatePositions();

        // Display the leaderboard data when the scene starts
        UpdateLeaderboardText();
    }

    void Update()
    {
        if (Time.time >= nextIncreaseTime)
        {
            foreach (var namedInt in leaderboardData)
            {
                IncreaseValue(namedInt);
            }

            leaderboardData = leaderboardData.OrderByDescending(x => x.Value).ToList();
            UpdatePositions();

            SaveData();

            nextIncreaseTime = Time.time + 3600; // 3600 seconds in 1 hour

            // Update the UI Text component with the leaderboard information
            UpdateLeaderboardText();
        }
    }

    void IncreaseValue(NamedInt namedInt)
    {
        if (namedInt.Name.Equals(playerUsername, System.StringComparison.OrdinalIgnoreCase))
        {
            // Do not increase the player's points
            return;
        }

        if (namedInt.Name.Equals("Jim", System.StringComparison.OrdinalIgnoreCase))
        {
            // Set Phyllis' points to 0
            namedInt.Value = 0;
        }
        else
        {
            // Increment points for other names
            int randomIncrement = Random.Range(1, 100);
            namedInt.Value += randomIncrement;
        }

        // Debug.Log(namedInt.Name + ": " + namedInt.Value + " at time: " + Time.time);
    }

    void SaveData()
    {
        // Filter out the player's information before saving
        List<NamedInt> filteredData = leaderboardData
            .Where(x => !x.Name.Equals(playerUsername, System.StringComparison.OrdinalIgnoreCase))
            .ToList();

        LeaderboardWrapper wrapper = new LeaderboardWrapper
        {
            NamedInts = filteredData
        };

        string jsonData = JsonUtility.ToJson(wrapper);
        File.WriteAllText(filepath, jsonData);
        Debug.Log("Data saved to JSON: " + filepath);
    }

    void LoadData()
    {
        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            // Debug.Log(jsonData);

            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(jsonData);
            if (wrapper != null)
            {
                leaderboardData = wrapper.NamedInts;
            }
            // Debug.Log("Data loaded from: " + filepath);
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < leaderboardData.Count; i++)
        {
            leaderboardData[i].Pos = i;
        }
    }

    void UpdateLeaderboardText()
    {
        if (leaderboardText != null)
        {
            // Find the maximum length of names
            int maxNameLength = leaderboardData.Max(x => x.Name.Length);

            // Update the Text component with the leaderboard information
            string leaderboardString = "";
            foreach (var namedInt in leaderboardData)
            {
                // Pad the name to the maximum length
                string formattedName = namedInt.Name.PadRight(maxNameLength);

                leaderboardString += $"      {namedInt.Pos + 1}                      {namedInt.Value,-5}                 {formattedName}\n";
            }
            leaderboardText.text = leaderboardString;
        }
    }

    void CreateIfNotExists()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "");
            // Debug.Log("Created leaderboardData.json");
        }
    }
}