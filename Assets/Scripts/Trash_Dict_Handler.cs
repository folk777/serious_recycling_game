using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Trash_Dict_Handler : MonoBehaviour, Game_Interface_Data
{
    // Start is called before the first frame update
    public GameObject plastic_trash;
    public GameObject paper_trash;
    public GameObject food_trash;
    public GameObject glass_trash;
<<<<<<< Updated upstream

    public GameObject foodbox_trash;
    public GameObject strawberryGlass_trash;
    public GameObject wineBag_trash;
    public GameObject liquidBottle_trash;


=======
>>>>>>> Stashed changes
    public List<string> spawned_item_tags;

    public List<Vector3> spawned_item_pos;

    public List<Quaternion> spawned_item_rot;

    public int spawned_item_count;

    
    void Start()
    {   
        // If trash dict is not empty
        if (spawned_item_count != 0) {
            for (int i = 0; i <= spawned_item_count-1; i++) {
                if (spawned_item_tags[i] == "Paper") {
                    GameObject trash = GameObject.Instantiate(paper_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];
                    //Debug.Log("A PAPER HAS BEEN SPAWNED ");
                }
                if (spawned_item_tags[i] == "Plastic") {
                    GameObject trash = GameObject.Instantiate(plastic_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];
                    //Debug.Log("A PLASTIC HAS BEEN SPAWNED ");
                }
                if (spawned_item_tags[i] == "Food") {
                    GameObject trash = GameObject.Instantiate(food_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];
                    //Debug.Log("A FOOD HAS BEEN SPAWNED ");
                }
                if (spawned_item_tags[i] == "Glass") {
                    GameObject trash = GameObject.Instantiate(glass_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];
                    //Debug.Log("A GLASS HAS BEEN SPAWNED ");
                }
                if (spawned_item_tags[i] == "Food_in_box") {
                    GameObject trash = GameObject.Instantiate(foodbox_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];

                }
                if (spawned_item_tags[i] == "Liquid_bottle") {
                    GameObject trash = GameObject.Instantiate(liquidBottle_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];

                }
                if (spawned_item_tags[i] == "Strawberry_jam") {
                    GameObject trash = GameObject.Instantiate(strawberryGlass_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];

                }
                if (spawned_item_tags[i] == "Wine_paperbag") {
                    GameObject trash = GameObject.Instantiate(wineBag_trash);
                    trash.transform.position = spawned_item_pos[i];
                    trash.transform.rotation = spawned_item_rot[i];

                }
            }
        }
        StartCoroutine(WaitOneFrameScript());
    }

    // Update is called once per frame
    void Update()
    {
        // Update the current item list
        string[] tags = {"Paper", "Plastic", "Food", "Glass", "Food_in_box", "Liquid_bottle", "Strawberry_jam", "Wine_paperbag"};
        if (true) {
            spawned_item_tags.Clear();
            spawned_item_pos.Clear();
            spawned_item_rot.Clear();
            spawned_item_count = 0;
             // Look for trash in each tag
            foreach (string tag in tags) {
                GameObject[] trashObjects = GameObject.FindGameObjectsWithTag(tag);
                // Add trash to the lists
                foreach (GameObject trash in trashObjects) {
                    // Check that the added trash are not part of the original trash
                    if (trash.transform.position != plastic_trash.transform.position && trash.transform.position != paper_trash.transform.position && trash.transform.position != food_trash.transform.position && trash.transform.position != glass_trash.transform.position && trash.transform.position != foodbox_trash.transform.position && trash.transform.position != strawberryGlass_trash.transform.position && trash.transform.position != wineBag_trash.transform.position && trash.transform.position != liquidBottle_trash.transform.position) {
                        spawned_item_tags.Add(trash.tag);
                        spawned_item_pos.Add(trash.transform.position);
                        spawned_item_rot.Add(trash.transform.rotation);
                        spawned_item_count ++;
                    }
                }
                

            }
        }
    }

    IEnumerator WaitOneFrameScript() {
        // Wait for 1 frame so that items from file gets loaded to script
        yield return null;
    }

    public void LoadData(Game_Data data) {
        this.spawned_item_tags = data.currentItemTags;
        this.spawned_item_pos = data.currentItemPos;
        this.spawned_item_rot = data.currentItemRot;
        this.spawned_item_count = data.currentItemCount;
    }

    public void SaveData(ref Game_Data data) {
        data.currentItemTags = this.spawned_item_tags;
        data.currentItemPos = this.spawned_item_pos;
        data.currentItemRot = this.spawned_item_rot;
        data.currentItemCount = this.spawned_item_count;
    }
}
