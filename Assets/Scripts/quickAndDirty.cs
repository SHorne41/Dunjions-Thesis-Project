using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quickAndDirty : MonoBehaviour {

    public Image tick80;
    public Image tick85;
    public Image tick90;
    public Image tick95;
    public Image tick100;
    public Image tick105;
    public Image tick110;
    public Image tick115;
    public Image tick120;

	// Use this for initialization
	void Start () {
        Debug.Log("80: " + tick80.transform.position);
        Debug.Log("85: " + tick85.transform.position);
        Debug.Log("90: " + tick90.transform.position);
        Debug.Log("95: " + tick95.transform.position);
        Debug.Log("100: " + tick100.transform.position);
        Debug.Log("105: " + tick105.transform.position);
        Debug.Log("110: " + tick110.transform.position);
        Debug.Log("115: " + tick115.transform.position);
        Debug.Log("120: " + tick120.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
