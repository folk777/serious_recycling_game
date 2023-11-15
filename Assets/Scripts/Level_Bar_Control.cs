using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level_Bar_Manager : MonoBehaviour
{

    public Image levelbar;
    private int level_progress = 0;

    private float level_progress_required = 50f;
    public int level = 1;
    private Player_Pickup player;
    // Start is called before the first frame update
    void Start()
    {   
        // Find Player, and get the player's points
        GameObject player_object = GameObject.Find("Player");
        if (player_object != null) {
            player = player_object.GetComponent<Player_Pickup>();
            player.points = 0;
        }    
        
        else {
            Debug.LogError("Player gameobject is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If player points are updated and level bar is not full, update level bar as well
        if (player != null) {
            if (level_progress != player.points) {
                if (levelbar.fillAmount != 1.0f) {
                    level_progress = player.points;
                    levelbar.fillAmount = level_progress / (level_progress_required * level);
                }
                // If level bar is full, restart bar and update level
                else if (levelbar.fillAmount == 1f) {
                    levelbar.fillAmount = 0f;
                    level_progress = 0;
                    player.points = 0;
                    level ++;
                }
            }
        }
    }
}
