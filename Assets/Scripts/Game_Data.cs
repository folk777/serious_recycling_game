using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game_Data  
{

 public int playerLevel;

 public int playerPoints;

 public float barFillAmount;

 public string playerName;

 public long lastSavedTime;

 public List<string> currentItemTags;

 public List<Vector3> currentItemPos;

 public List<Quaternion> currentItemRot;

 public int currentItemCount;



// When game is newly created, this would be the default data for the game
 public Game_Data() 
 {
    this.playerLevel = 1;
    this.playerPoints = 0;
    this.barFillAmount = 0.0f;

    this.currentItemTags = new List<string>();
    this.currentItemPos = new List<Vector3>();
    this.currentItemRot = new List<Quaternion>();
    this.currentItemCount = 0;
    
    
    // Get current time;
    this.lastSavedTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

    // Change this to get the player name and input it as so
    this.playerName = "Player";
 }

}
