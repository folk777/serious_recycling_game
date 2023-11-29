using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using System;

public class Trash_Spawner : MonoBehaviour, Game_Interface_Data
{

    public GameObject plastic_trash;
    public GameObject paper_trash;
    public GameObject food_trash;
    public GameObject glass_trash;
    public GameObject food_in_box_trash;
    public GameObject strawberry_jam_trash;
    public GameObject liquld_bottle_trash;
    public GameObject Wine_paperbag_trash;

    public LayerMask solid_objects;

    public LayerMask trash_bins;

    public long currentTime;

    public long savedTime;

    public long remainderTime;

    public long spawnAmount;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

        // Get how long it has been since player opened game
        remainderTime = currentTime - savedTime;

        // Current remainder time trash spawn rate is 1 trash per hour
        spawnAmount = Math.Abs(remainderTime / 3600);

        Debug.Log("SAVE TIME: " + savedTime + " // CURRENT TIME: " + currentTime + " // Amount Spawning: " + spawnAmount);

        StartCoroutine(spawn_random_trash());
    }

    IEnumerator spawn_random_trash() {
        // Spawn all the trash that should have been spawned since player closed game (1 trash per minute)
        while (spawnAmount != 0) {
            SpawnTrash();
            spawnAmount--;
            yield return new WaitForSecondsRealtime(0);
        }
        // If spawned all saved time related trash, spawn new trash every 10 seconds
        while (spawnAmount == 0) {
            SpawnTrash();
            yield return new WaitForSecondsRealtime(10);
        }
    }   

    public void SpawnTrash() {
        // Randomly select which trash to spawn 
        int random_num_trash = UnityEngine.Random.Range(0,4);

        // Randomly select position of where trash is spawned
        int random_num_x = UnityEngine.Random.Range(-3, 3);
        int random_num_y = UnityEngine.Random.Range(-3, 3); // (-2, 3)
        Vector3 random_position = new Vector3 (transform.position.x + random_num_x, transform.position.y + random_num_y, transform.position.z);
            
        // Randomly rotate trash around z axis
        float random_num_z = UnityEngine.Random.Range(-90f, 90f);
        Quaternion random_rotation = Quaternion.Euler(0, 0, random_num_z);

        // Check that trash spawn position in suitable (none solid objects or trashcans)
        Collider2D unspawnable_solid_blocks = Physics2D.OverlapCircle(random_position, .2f, solid_objects);
        Collider2D unspawnable_bin_blocks = Physics2D.OverlapCircle(random_position, .2f, trash_bins);

        // If trash position is suitable, spawn it
        if (!unspawnable_solid_blocks && !unspawnable_bin_blocks) {
            // Spawn trash based on number, pos, and rotation
            if (random_num_trash == 0) {
                GameObject trash = GameObject.Instantiate(food_in_box_trash); //plastic_trash
                trash.transform.position = random_position;
                trash.transform.rotation = random_rotation;
            }

            if (random_num_trash == 1) {
                GameObject trash = GameObject.Instantiate(strawberry_jam_trash); //paper_trash
                trash.transform.position = random_position;
                trash.transform.rotation = random_rotation;
            }

            if (random_num_trash == 2) {
                GameObject trash = GameObject.Instantiate(food_trash);
                trash.transform.position = random_position;
                trash.transform.rotation = random_rotation;
            }

            if (random_num_trash == 3) {
                GameObject trash = GameObject.Instantiate(glass_trash);
                trash.transform.position = random_position;
                trash.transform.rotation = random_rotation;
            }
        }       
    }

    public void LoadData(Game_Data data) {
        this.savedTime = data.lastSavedTime;
    }

    public void SaveData(ref Game_Data data){
        // We want to save the latest time before quitting game
        data.lastSavedTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();;

    }
}
