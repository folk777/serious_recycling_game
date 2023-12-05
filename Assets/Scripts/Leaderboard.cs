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
    private class PlayerInfoList
    {
        public List<PlayerInfo> PlayerInfo;
    }

    [System.Serializable]
    private class PlayerInfo
    {
        public string username;
        public int accumulated_points;
    }
    private string playerUsername;
    private int playerAccumulatedPoints;

    void Start()
    {
        string playerInfoPath = "/Users/zac/Documents/GitHub/serious_recycling_game/Assets/Resources/player_info.json";
        filepath = Path.Combine(Application.persistentDataPath, "/Users/zac/Documents/GitHub/serious_recycling_game/Assets/Resources/leaderboardData.json");

        CreateIfNotExists();
        
        // Load and print the content of the player_info.json file
        if (File.Exists(playerInfoPath))
        {
            string playerInfoJson = File.ReadAllText(playerInfoPath);
            Debug.Log("Player Info JSON:\n" + playerInfoJson);

            // Now you can parse the playerInfoJson if needed
            PlayerInfoList playerInfoList = JsonUtility.FromJson<PlayerInfoList>(playerInfoJson);

            // Accessing the first player info (assuming there's only one)
            if (playerInfoList.PlayerInfo.Count > 0)
            {
                PlayerInfo firstPlayerInfo = playerInfoList.PlayerInfo[0];
                playerUsername = firstPlayerInfo.username;
                playerAccumulatedPoints = firstPlayerInfo.accumulated_points;
                // Debug.Log($"Username: {firstPlayerInfo.username}, Accumulated Points: {firstPlayerInfo.accumulated_points}");
            }
        }
        else
        {
            Debug.LogError("Player Info file not found.");
        }

        LoadData();

        if (leaderboardData == null || leaderboardData.Count == 0)
        {
            leaderboardData = new List<NamedInt>
            {
                new NamedInt { Pos = 1, Name = "Dwight", Value = 0 },
                new NamedInt { Pos = 2, Name = "Michael", Value = 0 },
                new NamedInt { Pos = 3, Name = "Pam", Value = 0 },
                new NamedInt { Pos = 4, Name = "Jim", Value = 0 }
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

    if (namedInt.Name.Equals("Phyllis", System.StringComparison.OrdinalIgnoreCase))
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
        // Debug.Log("Data saved to JSON: " + filepath);
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