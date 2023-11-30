using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{   
    public Canvas level_bar;

    public Transform bar_spot;
    public Transform target;

    public Vector2 anchor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        Update_Bar_Position();
    }

    void Update_Bar_Position() {
        RectTransform canvas_pos = level_bar.GetComponent<RectTransform>();

        // Get screen dimensions & calculate anchor positions
        float screen_width = Screen.width;
        float screen_height = Screen.height;

        float anchor_x_pos = anchor.x * screen_width;
        float anchor_y_pos = anchor.y * screen_height;

        // Set anchor position for level bar
        canvas_pos.anchorMin = new Vector2(1,1);
        canvas_pos.anchorMax = new Vector2(1,1);

        canvas_pos.anchoredPosition = new Vector2(-anchor_x_pos, -anchor_y_pos);

    }
}
