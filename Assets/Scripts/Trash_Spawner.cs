using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Trash_Spawner : MonoBehaviour
{

    public GameObject plastic_trash;
    public GameObject paper_trash;
    public GameObject food_trash;
    public GameObject glass_trash;

    public LayerMask solid_objects;

    public LayerMask trash_bins;


    private bool spawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn_random_trash());
    }

    IEnumerator spawn_random_trash() {
        while (spawn) {
            // Randomly select which trash to spawn 
            int random_num_trash = Random.Range(0,4);

            // Randomly select position of where trash is spawned
            int random_num_x = Random.Range(-3, 2);
            int random_num_y = Random.Range(3, 4); // (-2, 3)
            Vector3 random_position = new Vector3 (transform.position.x + random_num_x, transform.position.y + random_num_y, transform.position.z);
            
            // Randomly rotate trash around z axis
            float random_num_z = Random.Range(-90f, 90f);
            Quaternion random_rotation = Quaternion.Euler(0, 0, random_num_z);

            // Check that trash spawn position in suitable (none solid objects or trashcans)
            Collider2D unspawnable_solid_blocks = Physics2D.OverlapCircle(random_position, .2f, solid_objects);
            Collider2D unspawnable_bin_blocks = Physics2D.OverlapCircle(random_position, .2f, trash_bins);

            // If trash position is suitable, spawn it
            if (!unspawnable_solid_blocks && !unspawnable_bin_blocks) {
                // Spawn trash based on number, pos, and rotation
                if (random_num_trash == 0) {
                    GameObject trash = GameObject.Instantiate(plastic_trash);
                    trash.transform.position = random_position;
                    trash.transform.rotation = random_rotation;
                }

                if (random_num_trash == 1) {
                    GameObject trash = GameObject.Instantiate(paper_trash);
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
            // Wait 5 seconds before doing it again
            yield return new WaitForSecondsRealtime(5);
        }
    }   
}
