using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
// must do it after user clicks on button
// - Continue/new game —> check if json file exists, if it doesn’t move to dialog_1, if it does skip dialog_1

public class continue_newGame : MonoBehaviour
{
    // Path to the JSON file
    private string filePath;

    public void MoveToScene()
    {
        // Set the file path within the "Resources" folder (adjust the file name as needed)
        filePath = Path.Combine(Application.dataPath, "Resources", "test.json");

        // Check if the JSON file exists
        if (File.Exists(filePath))
        {
            // File exists, skip dialog_1 (you can add your logic here)
            Debug.Log("JSON file exists.");
            SceneManager.LoadScene(0);

        }
        else
        {
            // File doesn't exist, move to dialog_1 (you can add your logic here)
            Debug.Log("JSON file does not exist.");
            SceneManager.LoadScene(2);
            // Create and save a sample JSON file (you can replace this with your actual data)
            SaveSampleJson();
        }
    }

    void Update()
    {
        // Add your update logic here if needed
    }

    // Function to save a sample JSON file
    void SaveSampleJson()
    {
        // Sample JSON data (replace this with your actual data)
        string jsonData = "{ \"key\": \"value\" }";

        // Write the JSON data to the file
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Sample JSON file saved at: " + filePath);
    }
}
