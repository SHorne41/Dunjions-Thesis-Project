using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingHandles : MonoBehaviour {

    //References to other scripts
    uiSwitchScript uiSwitchRef;

    StatsManager statsManagerRef;


    public int[] sliderPercentages;

    public Slider deathSlider;
    public Slider checkpointSlider;

    public Text lowerBound;
    public Text currentSetting;
    public Text upperBound;

    //These booleans are used to determine if the handles are trying to cross over each other
    public bool deathCrossing;
    public bool checkCrossing;
    public bool sliderValuesChanged;

    //This bool is used to determine whether or not the UI is active
    public bool uiStatus;

    //This value is used to determine the current setting for the given parameter
    public float currentPercentage;

    //These "previous" values are used in conjunction with the snap to value system, need to be updated right away
    public float previousDeathSliderValue;
    public float previousCheckpointSliderValue;

    //These "previous" values are used in conjunction with the updateHandlePosition script, not to be updated until the handles have been moved
    public float previousCheckpointSetting;
    public float previousDeathSetting;

    //The following enumerators are used to identify the sliders
    public enum monsters : byte
    {
        skeleton,
        spider,
        redMonster,
        greenMonster

    }
    public monsters monster;

    public enum allSliderTypes : byte
    {
        health,
        speed,
        attackPower,
        attackSpeed
    }
    public allSliderTypes sliderType;
    

    // Use this for initialization
    void Start()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();
        //This is required so that once the knight dies, the handle positions reflect the changes that were made by the wizard.
        //Is this the beginning of the game?
        if (!GameMaster.gameMaster.firstSpawn)
        {
            Debug.Log("Trying to update handles");
            if (monster == monsters.skeleton)
            {
                Debug.Log("It's the skeleton");
                if (sliderType == allSliderTypes.health && GameMaster.gameMaster.updatedStats[0] != 0)
                {
                    Debug.Log("Trying to update skeleton health");
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSkeletonHealth;
                    Debug.Log("Current Percentage: " + currentPercentage);
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.speed && GameMaster.gameMaster.updatedStats[1] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSkeletonSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackPower && GameMaster.gameMaster.updatedStats[2] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSkeletonAttackPower;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackSpeed && GameMaster.gameMaster.updatedStats[3] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSkeletonAttackSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
            }
            else if (monster == monsters.spider)
            {
                if (sliderType == allSliderTypes.health && GameMaster.gameMaster.updatedStats[4] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSpiderHealth;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.speed && GameMaster.gameMaster.updatedStats[5] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSpiderSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackPower && GameMaster.gameMaster.updatedStats[6] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSpiderAttackPower;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackSpeed && GameMaster.gameMaster.updatedStats[7] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentSpiderAttackSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
            }
            else if (monster == monsters.redMonster)
            {
                if (sliderType == allSliderTypes.health && GameMaster.gameMaster.updatedStats[8] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentRedMonsterHealth;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.speed && GameMaster.gameMaster.updatedStats[9] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentRedMonsterSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackPower && GameMaster.gameMaster.updatedStats[10] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentRedMonsterAttackPower;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackSpeed && GameMaster.gameMaster.updatedStats[11] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentRedMonsterAttackSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
            }
            else if (monster == monsters.greenMonster)
            {
                if (sliderType == allSliderTypes.health && GameMaster.gameMaster.updatedStats[12] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentGreenMonsterHealth;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.speed && GameMaster.gameMaster.updatedStats[13] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentGreenMonsterSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackPower && GameMaster.gameMaster.updatedStats[14] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentGreenMonsterAttackPower;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
                else if (sliderType == allSliderTypes.attackSpeed && GameMaster.gameMaster.updatedStats[15] != 0)
                {
                    deathSlider.value = 4.5f;
                    checkpointSlider.value = 4.5f;
                    currentPercentage = GameMaster.gameMaster.currentGreenMonsterAttackSpeed;
                    lowerBound.text = (currentPercentage - 20) + "";
                    currentSetting.text = currentPercentage + "";
                    upperBound.text = (currentPercentage + 20) + "";
                }
            }
        }

        //If this is the very first time the player has spawned, set the first spawn boolean to false
        else if (GameMaster.gameMaster.firstSpawn)
        {
            GameMaster.gameMaster.firstSpawn = false;
            Debug.Log("First Spawn set to false");
        }

        uiSwitchRef = FindObjectOfType<uiSwitchScript>();
        

        uiStatus = GameMaster.gameMaster.stateOfUI;

        deathCrossing = false;
        checkCrossing = false;
        sliderValuesChanged = false;

        previousDeathSliderValue = deathSlider.value;
        previousCheckpointSliderValue = checkpointSlider.value;

        previousCheckpointSetting = checkpointSlider.value;
        previousDeathSetting = deathSlider.value;
        
    }

    // Update is called once per frame
    void Update () {
        if (!GameMaster.gameMaster.dataReset)
        {
            statsManagerRef.resetUpdatedStats();
            GameMaster.gameMaster.dataReset = true;
        }
        checkUIStatus();
        checkSliderValues();
        if (deathCrossing || checkCrossing) { stopTheCross(); }
        if (sliderValuesChanged) { snapToValue(); }
    }

    

    //This method stops the handles from being dragged past the current setting
    void stopTheCross()
    {
        if (deathCrossing)
        {
            deathSlider.value = 3.5f;
            previousDeathSliderValue = deathSlider.value;
            deathCrossing = false;
        }
        if (checkCrossing)
        {
            checkpointSlider.value = 4.5f;
            previousCheckpointSliderValue = checkpointSlider.value;
            checkCrossing = false;
        }
    }

    //This method is used to check to see if the sliders have been adjusted
    //If so, bool set to true so that snapToValue() can be called, rather than calling it every frame
    //This method also detects if the handles are trying to be dragged past the current value
    //If so, bool set to true so that stopTheCross() can be called
    void checkSliderValues()
    {
        //Have the values changed? Do we need to call snapToValues?
        if (deathSlider.value != previousDeathSliderValue || checkpointSlider.value != previousCheckpointSliderValue)
        {
            sliderValuesChanged = true;
        }

        //Is the death handle trying to cross over?
        if (4 - deathSlider.value < 0.5)
        {
            deathCrossing = true;
        }

        //Is the checkpoint handle trying to cross over?
        if (checkpointSlider.value - 4 < 0.5)
        {
            checkCrossing = true;
        }
    }

    //In order to create an offset between the handles' default positions, the sliders cannot use Whole Numbers.
    //However, whole numbers are necessary to know which setting to use. As such, this method evaluates the position of the handle,
    //rounds that value to the nearest integer, and then sets the handle to the corresponding position on the slider
    void snapToValue()
    {
        if (deathSlider.value != previousDeathSliderValue)
        {
            deathSlider.value = Mathf.RoundToInt(deathSlider.value);
            previousDeathSliderValue = deathSlider.value;
            GameMaster.gameMaster.pendingDeathChanges = true;
        }

        if (checkpointSlider.value != previousCheckpointSliderValue)
        {
            checkpointSlider.value = Mathf.RoundToInt(checkpointSlider.value);
            previousCheckpointSliderValue = checkpointSlider.value;
            GameMaster.gameMaster.pendingCheckpointChanges = true;
        }
        sliderValuesChanged = false;
    }

    //This method is used to determine which sliders should be active and which ones should be disabled
    //Based on whether or not the UI is enabled and also on whether the settings are at max/min
    void checkUIStatus()
    {
        if (uiSwitchRef.uiSwitch.value == 0)                //If the UI is turned off, disable all the sliders
        {
            checkpointSlider.interactable = false;
            deathSlider.interactable = false;
            uiStatus = false;
            GameMaster.gameMaster.stateOfUI = false;
        }
        else if (uiSwitchRef.uiSwitch.value == 1)           //If the UI is turned on...
        {
            if (GameMaster.gameMaster.handlesDisabled)      //Check to see if any were turned off manually (due to the current setting being max/min)
            {
                if (currentPercentage == 0)                       //Is the current setting min?
                {
                    checkpointSlider.interactable = true;   //Then only activate the checkpoint slider
                }
                else if (currentPercentage == 8)                  //Is the current setting max?
                {
                    deathSlider.interactable = true;
                }
                else                                        //If the setting is neither maxed out or at the minimum
                {
                    checkpointSlider.interactable = true;   //Enable both sliders
                    deathSlider.interactable = true;
                }
            }
            else                                            //If none of the sliders were turned off manually
            {
                checkpointSlider.interactable = true;       //Enable both sliders
                deathSlider.interactable = true;
            }

            //There are no spiders in the first checkpoint, but there are afterwards.
            //Therefore, if the knight hasn't made it past the first checkpoint, ...
            if (GameMaster.gameMaster.currentCheckpoint < 1)
            {
                //And if the current slider belongs to the spider, ...
                if (monster == monsters.spider)
                {
                    //Disable the sliders
                    checkpointSlider.interactable = false;
                    deathSlider.interactable = false;
                }
            }
            //There are no Green Monsters until the last checkpoint.
            //Therefore, if the knight hasn't made it to the last checkpoint, ...
            if (GameMaster.gameMaster.currentCheckpoint < 5)
            {
                //And if the current slider belongs to the green monster, ...
                if (monster == monsters.greenMonster)
                {
                    //Disable the sliders
                    checkpointSlider.interactable = false;
                    deathSlider.interactable = false;
                }
            }
            if (GameMaster.gameMaster.currentCheckpoint == 5)
            {
                checkpointSlider.interactable = false;
            }
            uiStatus = true;
            GameMaster.gameMaster.stateOfUI = true;
        }
    }
}
