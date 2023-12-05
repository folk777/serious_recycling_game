using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Manager : MonoBehaviour, Game_Interface_Data
{
    private int level;
    // Start is called before the first frame update

    public GameObject house_0;
    public GameObject house_1;
    public GameObject house_2;
    public GameObject house_3;

    private Vector3 onscreen_pos = new Vector3 (15f, -3f, 3f);

    private Vector3 offscreen_pos = new Vector3 (100f, 100f, 1f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 1) {
            // Keep house sprite
            house_0.transform.position = onscreen_pos;

            house_1.transform.position = offscreen_pos;
            house_2.transform.position = offscreen_pos;
            house_3.transform.position = offscreen_pos;
        }

        if (level == 2) {
            // Change house to lvl 1 sprite
            house_1.transform.position = onscreen_pos;

            house_0.transform.position = offscreen_pos;
            house_2.transform.position = offscreen_pos;
            house_3.transform.position = offscreen_pos;
            
        }

        if (level == 3) {
            // Change house to lvl 2 sprite
            house_2.transform.position = onscreen_pos;

            house_0.transform.position = offscreen_pos;
            house_1.transform.position = offscreen_pos;
            house_3.transform.position = offscreen_pos;
        }

        if (level >= 4) {
            // Change house to final sprite
            house_3.transform.position = onscreen_pos;

            house_0.transform.position = offscreen_pos;
            house_1.transform.position = offscreen_pos;
            house_2.transform.position = offscreen_pos;

        }
    }

    public void LoadData(Game_Data data) {
        this.level = data.playerLevel;
    }

    public void SaveData(ref Game_Data data){
    
    }
}
