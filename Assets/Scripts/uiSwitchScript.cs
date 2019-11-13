using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiSwitchScript : MonoBehaviour {

    public Slider uiSwitch;
    public Text uiOnText;
    public Text uiOffText;

    float switchValue;
    float previousValue;

	// Use this for initialization
	void Start () {
        if (!GameMaster.gameMaster.stateOfUI)
        {
            uiOffText.color = Color.red;
            uiOnText.color = Color.black;
            switchValue = uiSwitch.value;
            previousValue = switchValue;
            uiSwitch.value = 0;
        }
        else if (GameMaster.gameMaster.stateOfUI)
        {
            uiOnText.color = Color.red;
            uiOffText.color = Color.black;
            switchValue = uiSwitch.value;
            previousValue = switchValue;
            uiSwitch.value = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		switchValue = uiSwitch.value;
        if (switchValue != previousValue) { setSwitchColor(); }
    }

    void setSwitchColor ()
    {
        if (switchValue == 0)
        {
            uiOnText.color = Color.black;
            uiOffText.color = Color.red;
            previousValue = 0;
        }
        else if (switchValue == 1)
        {
            uiOnText.color = Color.red;
            uiOffText.color = Color.black;
            previousValue = 1;
        }
    }
}
