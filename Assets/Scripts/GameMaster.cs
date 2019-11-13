using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster = null;

    //Variables needed to be stored between lives for File-write
    public string fileName;
    public int trial;
    public int stage;
    public float progressLast;
    public float progressCurrent;
    public float progressActual;

    //Variables needed to be stored between lives for the Progress Graph
    public Bar[] theBars;
    public float[] inputVals;
    public int numBars;
    public int livesThisCheckpoint;


    //Variables needed to be stored for the MovingHandles Script
    public bool dataReset;
    
    //The following variables store the current percentage for each and every slider
    //Skeleton Current Settings
    public float currentSkeletonHealth;
    public float currentSkeletonSpeed;
    public float currentSkeletonAttackPower;
    public float currentSkeletonAttackSpeed;

    //Spider Current Settings
    public float currentSpiderHealth;
    public float currentSpiderSpeed;
    public float currentSpiderAttackPower;
    public float currentSpiderAttackSpeed;

    //Red Monster Current Settings
    public float currentRedMonsterHealth;
    public float currentRedMonsterSpeed;
    public float currentRedMonsterAttackPower;
    public float currentRedMonsterAttackSpeed;

    //Green Monster Current Settings
    public float currentGreenMonsterHealth;
    public float currentGreenMonsterSpeed;
    public float currentGreenMonsterAttackPower;
    public float currentGreenMonsterAttackSpeed;

    //Boolean needed to be sotred in order to know if this is the first spawn or not
    public bool firstSpawn;

    //Array used to store the indices of the sliders that were adjusted (needed in order to determine if manipulateHandles needs to pull the value from here for the handles)
    public int[] updatedStats;

    //-------------------------------------------Variables needed to be stored between lives to update monster stats-------------------------------------------//
    public bool pendingDeathChanges;
    public bool pendingCheckpointChanges;
    public bool handlesDisabled;
    public int numDisabledHandles;

    //-------------------------------------------Variables needed to be stored between lives to for AI-------------------------------------------//
    public int consecutiveDeathCount;
    public int consecutiveSkeletonDeaths;
    public int consecutiveSpiderDeaths;
    public int consecutiveRedDeaths;
    public int consecutiveGreenDeaths;

    //The following variables store the CURRENT MULTIPLIERS that are being applied to the base stats
    //Stats Saved for Skeleton
    public float skeletonHealthMultiplier;
    public float skeletonSpeedMultiplier;
    public float skeletonAttackPowerMultiplier;
    public float skeletonAttackSpeedMultiplier;
    public float skeletonAttackRangeMultiplier;

    //Stats Saved for Spider
    public float spiderHealthMultiplier;
    public float spiderSpeedMultiplier;
    public float spiderAttackPowerMultiplier;
    public float spiderAttackSpeedMultiplier;

    //Stats Saved for Spider
    public float redMonsterHealthMultiplier;
    public float redMonsterSpeedMultiplier;
    public float redMonsterAttackPowerMultiplier;
    public float redMonsterAttackSpeedMultiplier;
    public float redMonsterAttackRangeMultiplier;
    public float redMonsterFireballSpeedMultiplier;

    //Stats Saved for Spider
    public float greenMonsterHealthMultiplier;
    public float greenMonsterSpeedMultiplier;
    public float greenMonsterAttackPowerMultiplier;
    public float greenMonsterAttackSpeedMultiplier;
    public float greenMonsterAttackRangeMultiplier;
    public float greenMonsterFireballSpeedMultiplier;

    //-------------------------------------------Variables needed to be stored between lives to update handle positions-------------------------------------------//


    //Variables needed to be stored in order to write to the txt file for logging purposes
    //The following variables store the LAST MULTIPLIER that was being applied to each base stat
    public float skeletonHealthLast;
    public float skeletonSpeedLast;
    public float skeletonAttackLast;
    public float skeletonAttackSpeedLast;
    public float skeletonAttackRangeLast;

    public float spiderHealthLast;
    public float spiderSpeedLast;
    public float spiderWebFrequencyLast;
    public float spiderWebDamageLast;

    public float redMonsterHealthLast;
    public float redMonsterSpeedLast;
    public float redMonsterAttackPowerLast;
    public float redMonsterAttackSpeedLast;
    public float redMonsterAttackRangeLast;
    public float redMonsterFireballSpeedLast;

    public float greenHornsMonsterHealthLast;
    public float greenHornsMonsterSpeedLast;
    public float greenHornsMonsterAttackPowerLast;
    public float greenHornsMonsterAttackSpeedLast;
    public float greenHornsMonsterFireballSpeedLast;
    public float greenHornsMonsterAttackRangeLast;

    //Storing the current checkpoint 
    public int currentCheckpoint;

    //-------------------Boolean needed to be stored for the state of the UI----------------------------//
    public bool stateOfUI;

    void Awake()
    {
        if (gameMaster == null)
        {
            gameMaster = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}