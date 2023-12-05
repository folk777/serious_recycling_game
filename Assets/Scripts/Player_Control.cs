using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Control : MonoBehaviour, Game_Interface_Data
{   

    public float move_speed = 5f;
    
    public Transform move_point;

    public LayerMask collision;

    public LayerMask trash_collision;

    public LayerMask sink_collision;

    public Animator animate;

    private Player_Pickup pickup;



    void Start() {
        // Don't comment this or player breaks
        move_point.parent = null;

        animate = GetComponent<Animator>();

        //transform.position = pos;

        // Initiate pickup direction and object
        pickup = gameObject.GetComponent<Player_Pickup>();
        pickup.direction = new Vector2(0,0);
    }

    void Update() {
        
        //Debug.Log("1: " + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, move_point.position, move_speed * Time.deltaTime);

        //Debug.Log("2: " + transform.position);

        Collider2D hitbox_horizontal = Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, collision);

        Collider2D hitbox_vertical = Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, collision);

        Collider2D trashcan_horizontal = Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, trash_collision);

        Collider2D trashcan_vertical = Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, trash_collision);

        Collider2D sink_horizontal = Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, sink_collision);

        Collider2D sink_vertical = Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, sink_collision);


        if (Vector3.Distance(transform.position, move_point.position) <= .05f) {

            animate.SetBool("Is_Moving", false);
            
            // Check for correct movement input
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                //Check for collision before moving
                if (!hitbox_horizontal && !trashcan_horizontal && !sink_horizontal) {
                    // If the pixels to the left / right of player is collision & not trashcan, don't drop item there
                    if (!Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal") + 0.5f, 0f, 0f), .2f, collision) && !Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal") - 0.5f, 0f, 0f), .2f, collision) ) {
                        pickup.direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }
                    // Move player
                    move_point.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    // Set animation
                    animate.SetFloat("Move_X", Input.GetAxisRaw("Horizontal"));
                    animate.SetFloat("Move_Y", Input.GetAxisRaw("Vertical"));

                    animate.SetBool("Is_Moving", true);

                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                if (!hitbox_vertical && !trashcan_vertical && !sink_vertical) {
                    // If position in front/back of player is collision, don't drop item there
                    if (!Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical") + 0.5f, 0f), .2f, collision) && !Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical") - 0.5f, 0f), .2f, collision)) {
                        pickup.direction = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }

                    move_point.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    // Set animation
                    animate.SetFloat("Move_X", Input.GetAxisRaw("Horizontal"));
                    animate.SetFloat("Move_Y", Input.GetAxisRaw("Vertical"));

                    animate.SetBool("Is_Moving", true);
                }
            }
        }
    }
    public void LoadData(Game_Data data) {
        transform.position = data.playerPosition;

    }

    public void SaveData(ref Game_Data data){
        data.playerPosition = transform.position;
    }
}
