using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplayScript : MonoBehaviour {


    // Use this for initialization
    void Start ()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            Cursor.lockState = CursorLockMode.Confined;


        }
        if (Display.displays.Length > 2)
        {
            Display.displays[2].Activate();
        }
    }
}
