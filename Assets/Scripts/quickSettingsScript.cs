using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quickSettingsScript : MonoBehaviour {

    uiSwitchScript uiSwitchref;
    StatsManager statsManagerRef;

    public Toggle deathToggle;
    public Toggle checkpointToggle;

    //Booleans used to show which types of quick settings were chosen
    //Minimum
    bool minPen;        //Are the pending changes minimal?
    bool minDeath;
    bool minCheck;
    
    //Moderate
    bool modPen;        //Are the pending changes moderate?
    bool modDeath;
    bool modCheck;

    //Major
    bool majPen;        //Are the pending changes major?
    bool majDeath;
    bool majCheck;

    //Extreme
    bool extPen;
    bool extDeath;
    bool extCheck;

    public Button minorChanges;
    public Button moderateChanges;
    public Button majorChanges;
    public Button extremeChanges;

    float modifier;

	// Use this for initialization
	void Start () {
        uiSwitchref = FindObjectOfType<uiSwitchScript>();
        statsManagerRef = FindObjectOfType<StatsManager>();
        modifier = 0;
        
       
	}
	
    //This method is called from within the Editor if the minor settings button is pressed
    public void minorQuickSettingsApplied()
    {
        if (deathToggle.isOn && !checkpointToggle.isOn)         //If the death toggle is checked, and the checkpoint toggle is not
        {
            minDeath = true;
            minCheck = false;
        }
        else if (!deathToggle.isOn && checkpointToggle.isOn)    //If the checkpoint toggle is checked, and the death toggle is not
        {
            minDeath = false;
            minCheck = true;
        }
        else if (deathToggle.isOn && checkpointToggle.isOn)     //If both toggles are checked
        {
            minDeath = true;
            minCheck = true;
        }

        minPen = true;      //We have minimal changes pending
        modPen = false;
        majPen = false;
        extPen = false;

        //Make sure to set all other pending mods to false
        majDeath = false;
        majCheck = false;
        modDeath = false;
        modCheck = false;
        extDeath = false;
        extCheck = false;
        applyQuickSettings();
    }

    //This method is called from within the Editor if the moderate settings button is pressed
    public void moderateQuickSettingsApplied ()
    {
        if (deathToggle.isOn && !checkpointToggle.isOn)         //If the death toggle is checked, and the checkpoint toggle is not
        {
            modDeath = true;
            modCheck = false;
        }
        else if (!deathToggle.isOn && checkpointToggle.isOn)    //If the checkpoint toggle is checked, and the death toggle is not
        {
            modDeath = false;
            modCheck = true;
        }
        else if (deathToggle.isOn && checkpointToggle.isOn)     //If both toggles are checked
        {
            modDeath = true;
            modCheck = true;
        }

        minPen = false;
        modPen = true;      //We have moderate changes pending
        majPen = false;
        extPen = false;

        //Make sure to set all other pending mods to false
        minDeath = false;
        minCheck = false;
        majDeath = false;
        majCheck = false;
        extDeath = false;
        extCheck = false;
        applyQuickSettings();
    }

    //This method is called from within the Editor if the major settings button is pressed
    public void majorQuickSettingsApplied ()
    {
        if (deathToggle.isOn && !checkpointToggle.isOn)         //If the death toggle is checked, and the checkpoint toggle is not
        {
            majDeath = true;
            majCheck = false;
        }
        else if (!deathToggle.isOn && checkpointToggle.isOn)    //If the checkpoint toggle is checked, and the death toggle is not
        {
            majDeath = false;
            majCheck = true;
        }
        else if (deathToggle.isOn && checkpointToggle.isOn)     //If both toggles are checked
        {
            majDeath = true;
            majCheck = true;
        }

        minPen = false;
        modPen = false;
        majPen = true;      //We have major changes pending
        extPen = false;

        //Make sure to set all other pending mods to false
        minDeath = false;
        minCheck = false;
        modDeath = false;
        modCheck = false;
        extDeath = false;
        extCheck = false;
        applyQuickSettings();
    }

    public void extremeQuickSettingsApplied()
    {
        if (deathToggle.isOn && !checkpointToggle.isOn)         //If the death toggle is checked, and the checkpoint toggle is not
        {
            extDeath = true;
            extCheck = false;
        }
        else if (!deathToggle.isOn && checkpointToggle.isOn)    //If the checkpoint toggle is checked, and the death toggle is not
        {
            extDeath = false;
            extCheck = true;
        }
        else if (deathToggle.isOn && checkpointToggle.isOn)     //If both toggles are checked
        {
            extDeath = true;
            extCheck = true;
        }

        minPen = false;
        modPen = false;
        extPen = true;      //We have extreme changes pending
        majPen = false;

        //Make sure to set all other pending mods to false
        minDeath = false;
        minCheck = false;
        modDeath = false;
        modCheck = false;
        majDeath = false;
        majCheck = false;
        applyQuickSettings();
    }

    //This method determines whether or not the buttons should be enabled based on the state of the checkboxes and the UI switch
    void enableButtons ()
    {
        if (uiSwitchref.uiSwitch.value == 0)
        {
            deathToggle.interactable = false;
            checkpointToggle.interactable = false;
            minorChanges.interactable = false;
            moderateChanges.interactable = false;
            majorChanges.interactable = false;
            extremeChanges.interactable = false;
        }
        else
        {
            deathToggle.interactable = true;
            checkpointToggle.interactable = true;
        }
        if (!deathToggle.isOn && !checkpointToggle.isOn)
        { 
            minorChanges.interactable = false;
            moderateChanges.interactable = false;
            majorChanges.interactable = false;
            extremeChanges.interactable = false;
        }
        else
        {
            minorChanges.interactable = true;
            moderateChanges.interactable = true;
            majorChanges.interactable = true;
            extremeChanges.interactable = true;
        }
    }

    void applyQuickSettings ()
    {
        if (minPen)     //If the pending quick settings are minimal
        {
            for (int i = 0; i < statsManagerRef.someHandles.Length; i++)
            {
                if (minDeath)       //If those settings are to be applied on death
                {
                    if (statsManagerRef.someHandles[i].deathSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].deathSlider.value = 3;
                    }
                }
                if (minCheck)       //If those settings are to be applied on check
                {
                    if (statsManagerRef.someHandles[i].checkpointSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].checkpointSlider.value = 5;
                    }
                }
            }
        }
        else if (modPen)        //If the pending quick settings are moderate
        {
            for (int i = 0; i < statsManagerRef.someHandles.Length; i++)
            {
                if (modDeath)       //If those settings are to be applied on death
                {
                    if (statsManagerRef.someHandles[i].deathSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].deathSlider.value = 2;
                    }
                }
                if (modCheck)       //If those settings are to be applied on check
                {
                    if (statsManagerRef.someHandles[i].checkpointSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].checkpointSlider.value = 6;
                    }
                }
            }
        }
        else if (majPen)
        {
            for (int i = 0; i < statsManagerRef.someHandles.Length; i++)
            {
                if (majDeath)       //If those settings are to be applied on death
                {
                    if (statsManagerRef.someHandles[i].deathSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].deathSlider.value = 1;
                    }
                }
                if (majCheck)       //If those settings are to be applied on check
                {
                    if (statsManagerRef.someHandles[i].checkpointSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].checkpointSlider.value = 7;
                    }
                }
            }
        }
        else if (extPen)
        {
            for (int i = 0; i < statsManagerRef.someHandles.Length; i++)
            {
                if (extDeath)       //If those settings are to be applied on death
                {
                    if (statsManagerRef.someHandles[i].deathSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].deathSlider.value = 0;
                    }
                }
                if (extCheck)       //If those settings are to be applied on check
                {
                    if (statsManagerRef.someHandles[i].checkpointSlider.interactable)
                    {
                        statsManagerRef.someHandles[i].checkpointSlider.value = 8;
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        enableButtons();
	}
}
