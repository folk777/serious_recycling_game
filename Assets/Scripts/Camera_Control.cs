using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{   
    public Canvas level_bar;

    public Transform bar_spot;
    public Transform target;
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
        canvas_pos.anchoredPosition = bar_spot.position;

    }
}
