using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Pickup : MonoBehaviour
{

    public Transform hold_spot;
    public LayerMask pickup_mask;
    public LayerMask paper_bin_mask;
    public LayerMask plastic_bin_mask;
    public LayerMask glass_bin_mask;
    public LayerMask food_bin_mask;

    public int points;

    // For leaderboard (potentially)
    public int accumulated_points;

    public Vector3 direction {get ; set; }

    private GameObject held_item;
    
    public GameObject paper_bin_holder;
    public GameObject plastic_bin_holder;
    public GameObject glass_bin_holder;
    public GameObject food_bin_holder;

    // Update is called once per frame
    void Start() {
        points = 0;
        accumulated_points = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            // If held item, drop object
            if (held_item){
                Collider2D paper_can = Physics2D.OverlapCircle(transform.position + direction, .4f, paper_bin_mask);
                Collider2D plastic_can = Physics2D.OverlapCircle(transform.position + direction, .4f, plastic_bin_mask);
                Collider2D glass_can = Physics2D.OverlapCircle(transform.position + direction, .4f, glass_bin_mask);
                Collider2D food_can = Physics2D.OverlapCircle(transform.position + direction, .4f, food_bin_mask);
                // Drop object in front of player and clear parent + item from player
                // Check if dropping item into trashcan or not
                if (paper_can || plastic_can || glass_can || food_can) {

                    if (paper_can) {
                        if (held_item.tag == "Paper") {
                            points += 20;
                            accumulated_points += 20;
                        }
                        Destroy(held_item);
                        //held_item.transform.parent = paper_bin_holder.transform;
                    }
                    if (plastic_can) {
                        if (held_item.tag == "Plastic") {
                            points += 20;
                            accumulated_points += 20;
                        }
                        Destroy(held_item);
                        //held_item.transform.parent = plastic_bin_holder.transform;
                    }
                    if (glass_can) {
                        if (held_item.tag == "Glass") {
                            points += 20;
                            accumulated_points += 20;
                        }
                        Destroy(held_item);
                        //held_item.transform.parent = glass_bin_holder.transform;
                    }
                    if (food_can) {
                        if (held_item.tag == "Food") {
                            points += 20;
                            accumulated_points += 20;
                        }
                        Destroy(held_item);
                        //held_item.transform.parent = food_bin_holder.transform;
                    }
                    held_item.transform.position = transform.position + direction;
                    held_item = null;
                }
                // If not dropping into trashcan 
                else {
                    held_item.transform.parent = null;
                    held_item.transform.position = transform.position + direction;
                    held_item = null;
                }
            }
            // If no item currently held, pick up object
            else {
                Collider2D pick_item = Physics2D.OverlapCircle(transform.position + direction, .4f, pickup_mask);
                Collider2D paper_can = Physics2D.OverlapCircle(transform.position + direction, .4f, paper_bin_mask);
                Collider2D plastic_can = Physics2D.OverlapCircle(transform.position + direction, .4f, plastic_bin_mask);
                Collider2D glass_can = Physics2D.OverlapCircle(transform.position + direction, .4f, glass_bin_mask);
                Collider2D food_can = Physics2D.OverlapCircle(transform.position + direction, .4f, food_bin_mask);
                
                // Check if item is within hitbox and NOT in the trashcan
                if (pick_item && !paper_can && !plastic_can && !glass_can && !food_can) {
                    held_item = pick_item.gameObject;
                    //parent_holder = pick_item.transform.parent.gameObject;
                    // Change item position from floor to player head
                    held_item.transform.position = hold_spot.position;
                    // Change parent of item to player
                    held_item.transform.parent = transform;
                }
            }
        }
    }
}
