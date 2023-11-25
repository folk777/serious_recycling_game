using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_Num : MonoBehaviour
{
    public TMP_Text level;
    private Level_Bar_Manager level_number;
    // Start is called before the first frame update
    void Start()
    {
        // Get current level
        GameObject level_object = GameObject.Find("LevelBarControl");
        level_number = level_object.GetComponent<Level_Bar_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update level text 
        level.text = "Lvl: " + level_number.level;

    }
}
