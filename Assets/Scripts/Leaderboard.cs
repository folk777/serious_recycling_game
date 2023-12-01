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

    void Start()
    {
        filepath = Path.Combine(Application.persistentDataPath, "leaderboardData.json");

        CreateIfNotExists();

        LoadData();

        if (leaderboardData == null || leaderboardData.Count == 0)
        {
            leaderboardData = new List<NamedInt>
            {
                new NamedInt { Pos = 0, Name = "Dwight", Value = 0 },
                new NamedInt { Pos = 1, Name = "Michael", Value = 0 },
                new NamedInt { Pos = 2, Name = "Pam", Value = 0 },
                new NamedInt { Pos = 3, Name = "Jim", Value = 0 }
            };
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
        }

        initialized = true;

        nextIncreaseTime = Time.time + Random.Range(1f, 1f);

        leaderboardData = leaderboardData.OrderByDescending(x => x.Value).ToList();
        UpdatePositions();
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

            nextIncreaseTime = Time.time + Random.Range(1f, 1f);

            // Update the UI Text component with the leaderboard information
            UpdateLeaderboardText();
        }
    }

    void IncreaseValue(NamedInt namedInt)
    {
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

        Debug.Log(namedInt.Name + ": " + namedInt.Value + " at time: " + Time.time);
    }

    void SaveData()
    {
        LeaderboardWrapper wrapper = new LeaderboardWrapper
        {
            NamedInts = leaderboardData
        };

        string jsonData = JsonUtility.ToJson(wrapper);
        Debug.Log(jsonData);

        File.WriteAllText(filepath, jsonData);
        Debug.Log("Data saved to JSON: " + filepath);
    }

    void LoadData()
    {
        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            Debug.Log(jsonData);

            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(jsonData);
            if (wrapper != null)
            {
                leaderboardData = wrapper.NamedInts;
            }

            Debug.Log("Data loaded from: " + filepath);
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
            // Update the Text component with the leaderboard information
            string leaderboardString = "";

            foreach (var namedInt in leaderboardData)
            {
                leaderboardString += $"      {namedInt.Pos + 1}.                     {namedInt.Value,-5}                 {namedInt.Name}\n";

            }
            leaderboardText.text = leaderboardString;
        }
    }

    void CreateIfNotExists()
    {
        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "");
            Debug.Log("Created leaderboardData.json");
        }
    }
}
