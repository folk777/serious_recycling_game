using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.IO.LowLevel.Unsafe;

public class Game_File_Handler
{
    // File location is: "C:/../../AppData/Locallow/DefaultCompany/My project
    private string data_dir_path = "";

    private string data_filename = "";

    public Game_File_Handler(string data_dir_path, string data_filename) {
        this.data_dir_path = data_dir_path;
        this.data_filename = data_filename;
    }

    public Game_Data Load() {
        string file_path = Path.Combine(data_dir_path, data_filename);
        Game_Data loaded_data = null;
        if (File.Exists(file_path)) {
            try {
                // Load serialized data from file
                string data_to_load = "";
                using (FileStream stream = new FileStream(file_path, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        data_to_load = reader.ReadToEnd();
                    }
                }

                // Deserialize data from JSON to gameobject
                loaded_data = JsonUtility.FromJson<Game_Data>(data_to_load);


            } catch (Exception e) {
                Debug.LogError("Error when trying to load data from file" + file_path + "\n" + e);
            }
        }
        return loaded_data;

    }

    public void  Save(Game_Data data) {
        string file_path = Path.Combine(data_dir_path, data_filename);
        try {

            // Create a directory of the file if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(file_path));

            // Serialize game data for JSON file
            string store_data =JsonUtility.ToJson(data, true);

            // Write game data into JSON file
            using (FileStream stream = new FileStream(file_path, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(store_data);
                }
            }
        }
        catch (Exception e) {
            Debug.LogError ("Error saving game data to file: " + file_path + "\n" + e);
        }

    }
}
