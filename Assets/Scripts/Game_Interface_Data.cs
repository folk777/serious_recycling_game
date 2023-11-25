using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  Game_Interface_Data 
{
    // Trash_Spawner.cs - for time
    // Level_Bar_Control.cs - for level && barfillamount
    // Player_Pickup.cs - for points
    // 
    
    void LoadData (Game_Data data);

    void SaveData (ref Game_Data data);
}
