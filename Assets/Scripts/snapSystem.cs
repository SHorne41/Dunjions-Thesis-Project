using System.Collections;
using System.Collections.Generic;
using UnityEngine;






[ExecuteInEditMode]
public class snapSystem : MonoBehaviour {

    [SerializeField]
    bool movable = true;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // Debug.Log("size.x: " + transform.GetComponent<Collider>().bounds.size.x + "size.z: " + transform.GetComponent<Collider>().bounds.size.z);
        if (movable)
        {
            snap();
        }
    }

    void snap() {
        if (transform.gameObject.tag == "squareWall") {
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x)+0.5f, (transform.GetComponent<Collider>().bounds.size.y - (transform.GetComponent<Collider>().bounds.size.y * 0.5f)), Mathf.FloorToInt(transform.position.z) + 0.5f);
        }
        else if (transform.gameObject.tag == "roof")
        {
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x), 3.5f, Mathf.FloorToInt(transform.position.z));
        }
         else
        {
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x), transform.GetComponent<Collider>().bounds.size.y - (transform.GetComponent<Collider>().bounds.size.y * 0.5f), Mathf.FloorToInt(transform.position.z));
        }
    }
}
