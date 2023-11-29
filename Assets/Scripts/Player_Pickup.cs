using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Pickup : MonoBehaviour, Game_Interface_Data
{

    public Transform hold_spot;
    public LayerMask pickup_mask;
    public LayerMask paper_bin_mask;
    public LayerMask plastic_bin_mask;
    public LayerMask glass_bin_mask;
    public LayerMask food_bin_mask;
    public LayerMask sink_mask;

    public int points;

    // For leaderboard (potentially)
    public int accumulated_points;

    public Vector3 direction {get ; set; }

    private GameObject held_item;
    
    public GameObject paper_bin_holder;
    public GameObject plastic_bin_holder;
    public GameObject glass_bin_holder;
    public GameObject food_bin_holder;

    public GameObject sink_object;

    public GameObject plastic_object;
    public GameObject glass_object;
    public GameObject food_object;
    public GameObject paper_object;


    // Update is called once per frame
    void Start() {
        //points = 0;
        //accumulated_points = 0;
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
                Collider2D sink = Physics2D.OverlapCircle(transform.position + direction, .4f, sink_mask);
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
                if (sink) {
                    //For tier 1 trash
                    if (held_item.tag == "Paper" || held_item.tag == "Food" || held_item.tag == "Plastic" || held_item.tag == "Glass") {
                        held_item.transform.parent = null;
                        held_item.transform.position = new Vector3 (sink_object.transform.position.x - 1f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);
                        held_item = null;
                        Debug.Log("TEST HELLO");
                        }

                    //For tier 2 trash
                    else if (held_item.tag == "Food_in_box"){
                        Debug.Log("HELLO FOOD IN BOX");
                        points += 20;
                        accumulated_points += 20;

                        GameObject trash = GameObject.Instantiate(food_object);
                        trash.transform.position = new Vector3 (sink_object.transform.position.x - 0f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);

                        GameObject trash2 = GameObject.Instantiate(paper_object);
                        trash2.transform.position = new Vector3 (sink_object.transform.position.x - 1f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);
                        
                        Debug.Log("HELLO");
                        Destroy(held_item);
                        held_item = null;
                    }

                    else if (held_item.tag == "Strawberry_jam"){
                        Debug.Log("STRAWBERRYZ JAM HELLO");
                        points += 20;
                        accumulated_points += 20; 

                        GameObject trash = GameObject.Instantiate(glass_object);
                        trash.transform.position = new Vector3 (sink_object.transform.position.x - 0f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);

                        Destroy(held_item);
                        held_item = null;  
                    }

                    else if (held_item.tag == "Liquid_bottle"){
                        Debug.Log("LIQUID BOTTLE HELLO");
                        points += 20;
                        accumulated_points += 20;

                        GameObject trash = GameObject.Instantiate(plastic_object);
                        trash.transform.position = new Vector3 (sink_object.transform.position.x - 0f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);

                        Destroy(held_item);
                        held_item = null;
                    }

                    else if(held_item.tag == "Wine_paperbag"){
                        Debug.Log("WINEPAPERBAG HELLO");
                        points += 20;
                        accumulated_points += 20; 

                        GameObject trash = GameObject.Instantiate(glass_object);
                        trash.transform.position = new Vector3 (sink_object.transform.position.x - 0f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);

                        GameObject trash2 = GameObject.Instantiate(paper_object);
                        trash2.transform.position = new Vector3 (sink_object.transform.position.x - 1f, sink_object.transform.position.y - 1f, sink_object.transform.position.z);
                        Destroy(held_item);
                        held_item = null;  
                    }
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
                Collider2D sink = Physics2D.OverlapCircle(transform.position + direction, .4f, sink_mask);
                               
                // Check if item is within hitbox and NOT in the trashcan
                if (pick_item && !paper_can && !plastic_can && !glass_can && !food_can && !sink) {
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

    public void LoadData(Game_Data data) {
        this.points = data.playerPoints;
        this.accumulated_points = data.playerTotalPoints;
    }

    public void SaveData(ref Game_Data data){
        data.playerPoints = this.points;
        data.playerTotalPoints = this.accumulated_points;
    }
}
