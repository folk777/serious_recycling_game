using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;
    public Player_Pickup player_pickup;

    [System.Serializable]
    public class Player
    {
        public string username;
        public int XP;
        public int position;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();

    void Start()
    {
        // if (player_pickup != null)
        // {   
        //     int accumulated_points = playerPickupReference.accumulated_points;
        //     // If it exists, set accumulated_points to its value
        //     Debug.Log("Accumulated Points: " + accumulated_points);
        // }
        // else
        // {
        //     // If it doesn't exist, set accumulated_points to zero
        //     int accumulated_points = 0;
        //     Debug.Log("Player_pickup script not found. Setting points to zero.");
        // }
        
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);

        // Sort the players based on XP in descending order
        myPlayerList.player = myPlayerList.player.OrderByDescending(player => player.XP).ToArray();

        // Set positions after sorting
        for (int i = 0; i < myPlayerList.player.Length; i++)
        {
            myPlayerList.player[i].position = i + 1;
        }
    }

    void OnGUI()
    {
        int fontSize = 40;  // Adjust the font size
        float xOffset = 400.0f;  // Adjust the starting x-coordinate
        float yOffset = 350.0f;  // Adjust the starting y-coordinate
        float spacing = 50.0f;  // Adjust the vertical spacing between labels

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = fontSize;
        style.font = Font.CreateDynamicFontFromOSFont("Courier New", fontSize);

        int maxUsernameLength = GetMaxUsernameLength();
        int maxXPLength = GetMaxXPLength();  // Calculate the maximum XP length

        foreach (Player currentPlayer in myPlayerList.player)
        {
            // Convert XP to a string and pad with leading whitespace to match the max XP length
            string xpString = currentPlayer.XP.ToString().PadLeft(maxXPLength);

            // Construct the playerInfo string
            string playerInfo = $"{currentPlayer.position}             {xpString}           {currentPlayer.username.PadRight(maxUsernameLength)}";

            // Display the playerInfo in GUI
            GUI.Label(new Rect(xOffset, yOffset, 1000, 130), playerInfo, style);

            // Increment yOffset for the next label
            yOffset += spacing;
        }
    }

    int GetMaxUsernameLength()
    {
        int maxUsernameLength = 0;
        foreach (Player player in myPlayerList.player)
        {
            if (player.username.Length > maxUsernameLength)
            {
                maxUsernameLength = player.username.Length;
            }
        }
        return maxUsernameLength;
    }

    int GetMaxXPLength()
    {
        int maxXP = myPlayerList.player.Max(player => player.XP);
        return maxXP.ToString().Length;
    }
}