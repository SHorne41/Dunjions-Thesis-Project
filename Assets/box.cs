using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {

    [SerializeField]
    GameObject health;


    [SerializeField]
    bool isHeart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void breakThis(){

        if (isHeart)
        {
            health.transform.parent = null;
        }


        gameObject.SetActive(false);

   }



}
