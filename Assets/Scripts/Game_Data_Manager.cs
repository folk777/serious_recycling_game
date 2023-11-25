using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game_Data_Manager: MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string file_name;

    private Game_File_Handler game_data_handler;
    public static Game_Data_Manager instance { get; private set;}

    private Game_Data game_data;

    private List<Game_Interface_Data> game_data_objects;


    // For keeping track of the current game data
    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one save state found");
        }
        instance = this;
    }


    private void Start() {
        this.game_data_handler = new Game_File_Handler(Application.persistentDataPath, file_name);
        this.game_data_objects = FindAllGameDataObjects();
        LoadGame();
    }

    public void NewGame() 
    {
        // Initialize the game data 
        this.game_data = new Game_Data();
    }

    public void LoadGame()
    {
        // Load data from JSON file using data handler
        this.game_data = game_data_handler.Load();

        // If no data found, initialize new game data
        if (this.game_data == null){
            Debug.LogError("No game data has been found. New game data will be created");
            NewGame();
        }
        
        // Update all the data from JSON file to all gameobjects
        foreach (Game_Interface_Data game_data_object in game_data_objects) {
            game_data_object.LoadData(game_data);
        }

        Debug.Log("LOAD GAME DATA::::  LastSavedTime: " + game_data.lastSavedTime);
    }

    public void SaveGame()
    {
        // Pass data to other scripts to update them
        foreach (Game_Interface_Data game_data_object in game_data_objects) {
            game_data_object.SaveData(ref game_data);
        }

        Debug.Log(" SAVE GAME DATA::: LastSavedTime: " + game_data.lastSavedTime);

        // Save data to the JSON file using data handler
        game_data_handler.Save(game_data);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<Game_Interface_Data> FindAllGameDataObjects(){
        // Finds all GameObjects with Type MonoBehaviour which uses the Game_Interface_Data
        IEnumerable<Game_Interface_Data> gameDataObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<Game_Interface_Data>();

        return new List<Game_Interface_Data>(gameDataObjects);
    }
}
