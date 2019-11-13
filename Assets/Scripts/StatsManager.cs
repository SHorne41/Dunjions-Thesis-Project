using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {

    public Stats statsRef;
    public quickSettingsScript quickSettingsRef;
    public updateProgressGraph updateProgressGraphRef;
    

    public UpdateSliders updateSliderRef;

    public LevelTrigger levelTriggerRef;

    public MovingHandles[] someHandles;

    // Use this for initialization
    void Start () {
        
        someHandles = FindObjectsOfType<MovingHandles>();
        updateSliderRef = FindObjectOfType<UpdateSliders>();
        updateProgressGraphRef = FindObjectOfType<updateProgressGraph>();
        statsRef = FindObjectOfType<Stats>();
        levelTriggerRef = FindObjectOfType<LevelTrigger>();
        quickSettingsRef = FindObjectOfType<quickSettingsScript>();
    }
	
    //---------------------------------------------------Getters------------------------------------------------//
    //Progression Getters
    public int getRedMonsterDead() { return statsRef.redMonsterDead; }
    public int getGreenHornsMonsterDead() { return statsRef.greenHornsMonsterDead; }
    public int getSpiderDead() { return statsRef.spiderDead; }
    public int getSkeletonDead() { return statsRef.skeletonDead; }

    //Knight Getters
    public float getKnightHealth() { return statsRef.hp; }
    public float getKnightSpeed() { return statsRef.speed; }
    public float getKnightAcceleration() { return statsRef.accel; }
    public float getKnightHealthRegenerationRate() { return statsRef.rate; }
    public float getKnightDamageResistance() { return statsRef.resistance; }
    public float getKnightAttackPower() { return statsRef.power; }

    public int getKnightTargetNumber() { return statsRef.targetNumber; }

    public int getCurrentSpawnLocation() { return statsRef.currentSpawn; }
    public int getPreviousSpawnLocation() { return statsRef.previousSpawn; }

    public bool getKnightIsAlive() { return statsRef.isAlive; }

    public float getKnightProgressCurrent () { return statsRef.progressCurrent; }
    public float getKnightProgressLast() { return statsRef.progressLast; }
    public float getKnightProgressActual () { return statsRef.actualProgress; }

    public Vector3 getKnightSpawnPosition () { return statsRef.spawnPositions[statsRef.currentSpawn]; }

    //---------------------------------------------------Skeleton Getters------------------------------------------------//
    //Base Stat Getters
    public float getSkeletonBaseHealth() { return statsRef.skeletonBaseHealth; }
    public float getSkeletonBaseSpeed() { return statsRef.skeletonBaseSpeed; }
    public float getSkeletonBaseAttackPower() { return statsRef.skeletonBaseAttackPower; }
    public float getSkeletonBaseAttackSpeed() { return statsRef.skeletonBaseAttackSpeed; }
    public float getSkeletonBaseAttackRange() { return statsRef.skeletonBaseAttackRange; }

    public float getDamageDealtSkeletonCL () { return statsRef.damageDealtSkeletonCL; }

    //Stat modifier Getters
    public float getSkeletonHealth () { return statsRef.skeletonHealth; }
    public float getSkeletonSpeed() { return statsRef.skeletonSpeed; }
    public float getSkeletonAttackPower() { return statsRef.skeletonAttack; }
    public float getSkeletonAttackSpeed() { return statsRef.skeletonAttackSpeed; }
    public float getSkeletonAttackRange() { return statsRef.skeletonAttackRange; }

    //---------------------------------------------------Spider Getters------------------------------------------------//
    //Base Stat Getters
    public float getSpiderBaseHealth() { return statsRef.spiderBaseHealth; }
    public float getSpiderBaseSpeed() { return statsRef.spiderBaseSpeed; }
    public float getSpiderBaseAttackSpeed() { return statsRef.spiderBaseWebFrequency; }
    public float getSpiderBaseAttackPower() { return statsRef.spiderBaseWebDamage; }

    public float getDamageDealtSpiderCL() { return statsRef.damageDealtSpiderCL; }

    //Stat Modifier Getters
    public float getSpiderHealth() { return statsRef.spiderHealth; }
    public float getSpiderSpeed() { return statsRef.spiderSpeed; }
    public float getSpiderAttackSpeed() { return statsRef.spiderWebFrequency; }
    public float getSpiderAttackPower() { return statsRef.spiderWebDamage; }

    //---------------------------------------------------Red Monster Getters------------------------------------------------//
    // Base Stat Getters
    public float getRedMonsterBaseHealth() { return statsRef.redMonsterBaseHealth; }
    public float getRedMonsterBaseSpeed() { return statsRef.redMonsterBaseSpeed; }
    public float getRedMonsterBaseAttackPower() { return statsRef.redMonsterBaseAttackPower; }
    public float getRedMonsterBaseAttackSpeed() { return statsRef.redMonsterBaseAttackSpeed; }
    public float getRedMonsterBaseAttackRange() { return statsRef.redMonsterBaseAttackRange; }
    public float getRedMonsterBaseFireballSpeed() { return statsRef.redMonsterBaseFireballSpeed; }

    public float getDamageDealtRedMonsterCL() { return statsRef.damageDealtRedMonsterCL; }

    //Stat modifier Getters
    public float getRedMonsterHealth() { return statsRef.redMonsterHealth; }
    public float getRedMonsterSpeed() { return statsRef.redMonsterSpeed; }
    public float getRedMonsterAttackPower() { return statsRef.redMonsterAttackPower; }
    public float getRedMonsterAttackSpeed() { return statsRef.redMonsterAttackSpeed; }
    public float getRedMonsterAttackRange() { return statsRef.redMonsterAttackRange; }
    public float getRedMonsterFireballSpeed() { return statsRef.redMonsterFireballSpeed; }


    //---------------------------------------------------Green Monster Getters------------------------------------------------//
    //Base Stat Getters
    public float getGreenMonsterBaseHealth() { return statsRef.greenHornsMonsterBaseHealth; }
    public float getGreenMonsterBaseSpeed() { return statsRef.greenHornsMonsterBaseSpeed; }
    public float getGreenMonsterBaseAttackPower() { return statsRef.greenHornsMonsterBaseAttackPower; }
    public float getGreenMonsterBaseAttackSpeed() { return statsRef.greenHornsMonsterBaseAttackSpeed; }
    public float getGreenMonsterBaseAttackRange() { return statsRef.greenHornsMonsterBaseAttackRange; }
    public float getGreenMonsterBaseFireballSpeed() { return statsRef.greenHornsMonsterBaseFireballSpeed; }

    public float getDamageDealtGreenMonsterCL() { return statsRef.damageDealtGreenMonsterCL; }


    //Green Monster Getters
    public float getGreenMonsterHealth() { return statsRef.greenHornsMonsterHealth; }
    public float getGreenMonsterSpeed() { return statsRef.greenHornsMonsterSpeed; }
    public float getGreenMonsterAttackPower() { return statsRef.greenHornsMonsterAttackPower; }
    public float getGreenMonsterAttackSpeed() { return statsRef.greenHornsMonsterAttackSpeed; }
    public float getGreenMonsterAttackRange() { return statsRef.greenHornsMonsterAttackRange; }
    public float getGreenMonsterFireballSpeed() { return statsRef.greenHornsMonsterFireballSpeed; }

    //Needed for damage calculations
    public float getSkeletonFinalAttack () { return statsRef.skeletonFinalAttack; }
    public void setSkeletonFinalAttack (float attack) { statsRef.skeletonFinalAttack = attack; }

    public float getSpiderFinalAttack() { return statsRef.spiderFinalAttack; }
    public void setSpiderFinalAttack(float attack) { statsRef.spiderFinalAttack = attack; }

    public float getRedMonsterFinalAttack() { return statsRef.redMonsterFinalAttack; }
    public void setRedMonsterFinalAttack(float attack) { statsRef.redMonsterFinalAttack = attack; }

    public float getGreenMonsterFinalAttack() { return statsRef.greenMonsterFinalAttack; }
    public void setGreenMonsterFinalAttack(float attack) { statsRef.greenMonsterFinalAttack = attack; }


    //---------------------------------------------------Setters------------------------------------------------//
    //Progression setters
    public void reportProgression()
    {

        statsRef.progressMax = 0;
        statsRef.progressMax += levelTriggerRef.redMonsterDeadX * 2f;
        statsRef.progressMax += levelTriggerRef.greenHornsMonsterDeadX * 5f;
        statsRef.progressMax += levelTriggerRef.skeletonDeadX;
        statsRef.progressMax += levelTriggerRef.spiderDeadX * 1.5f;
        Debug.Log("Progress Max: " + statsRef.progressMax);

        setKnightProgressCurrent(0);
        setKnightProgressCurrent(getKnightProgressCurrent() + (statsRef.redMonsterDead * 2f));
        setKnightProgressCurrent(getKnightProgressCurrent() + (statsRef.greenHornsMonsterDead * 5f));
        setKnightProgressCurrent(getKnightProgressCurrent() + statsRef.skeletonDead);
        setKnightProgressCurrent(getKnightProgressCurrent() + (statsRef.spiderDead * 1.5f));

        setKnightProgressCurrent(getKnightProgressCurrent() / statsRef.progressMax);

        updateProgressGraphRef.updateBars();

        Debug.Log("Setting progress current: " + getKnightProgressCurrent());
        setProgression(getKnightProgressCurrent());
    }

    public void setRedMonsterDead (int i) {
        if (i != 0 && statsRef.redMonsterDead < levelTriggerRef.redMonsterDeadX)
        {
            statsRef.redMonsterDead += i;
        }
        else if (i == 0)           //The knight has made it to a checkpoint, must reset the number of red monsters killed
        {
            statsRef.redMonsterDead = i;
        }
        else if (statsRef.redMonsterDead == levelTriggerRef.redMonsterDeadX)
        {
            //Do nothing
        } 
    }

    public void setGreenHornsMonsterDead (int i) {
        if ( i != 0 && statsRef.greenHornsMonsterDead < levelTriggerRef.greenHornsMonsterDeadX)
        {
            statsRef.greenHornsMonsterDead += i;
        }
        else if (i == 0)          //The knight has made it to a checkpoint, must reset the number of green monsters killed
        {
            statsRef.greenHornsMonsterDead = i;
        }
        else if (statsRef.greenHornsMonsterDead == levelTriggerRef.greenHornsMonsterDeadX)
        {
            //Do nothing
        }

    }


    public void setSpiderDead (int i) {

        if (i != 0 && statsRef.spiderDead < levelTriggerRef.spiderDeadX)
        {
            statsRef.spiderDead += i;
        }
        else if (i==0)          //The knight has made it to a checkpoint, must reset the number of spiders killed
        {
            statsRef.spiderDead = i;
        }
        else if (statsRef.spiderDead == levelTriggerRef.spiderDeadX)
        {
            Debug.Log("Uh oh");
            Debug.Log("Number of Spiders required to be killed: " + levelTriggerRef.spiderDeadX);
            Debug.Log("Number of Spiders killed: " + statsRef.spiderDead);
            //Do nothing
        }

    }


    public void setSkeletonDead(int i) {
        if (i != 0 && statsRef.skeletonDead < levelTriggerRef.skeletonDeadX)
        {
            statsRef.skeletonDead += i;
        }
        else if (i == 0)        //The knight has made it to a checkpoint, must reset the number of skeletons killed
        {
            statsRef.skeletonDead = i;
        }
        else if (statsRef.skeletonDead == levelTriggerRef.skeletonDeadX)
        {
            //Do nothing
        }
       
    }

    //Knight Setters
    public void setKnightHealth(float hp) { statsRef.hp = hp; }
    public void setKnightSpeed(float speed) { statsRef.speed = speed; }
    public void setKnightAcceleration(float acc) { statsRef.accel = acc; }
    public void setKnightHealthRegenerationRate(float rate) { statsRef.rate = rate; }
    public void setKnightDamageResistance(float resistance) { statsRef.resistance = resistance; }
    public void setKnightAttackPower(float damage) { statsRef.power = damage; }
    public void setKnightTargetNumber(int target) { statsRef.targetNumber = target; }

    public void setCurrentSpawnLocation(int location) { statsRef.currentSpawn = location; GameMaster.gameMaster.currentCheckpoint = statsRef.currentSpawn; }
    public void setPreviousSpawnLocation(int location) { statsRef.previousSpawn = location; }

    public void setKnightIsAlive (bool a) { statsRef.isAlive = a; }

    public void setKnightProgressCurrent (float progress) {
        statsRef.progressCurrent = progress;
        GameMaster.gameMaster.progressCurrent = progress;
    }
    public void setKnightProgressLast (float progress) {
        statsRef.progressLast = progress;
    }
    public void setKnightActualProgress (float progress) {
        statsRef.actualProgress = progress;
        GameMaster.gameMaster.progressActual = progress;
    }


    public void setProgression(float progressIn)
    {
        
            if (getKnightProgressLast() != 0)
            {
                setKnightActualProgress((getKnightProgressCurrent() - getKnightProgressLast()) * 100);
            }
            else
            {
                setKnightActualProgress(getKnightProgressCurrent() * 100);
            }
        
        
        setKnightProgressLast(getKnightProgressCurrent());
        Debug.Log("progressLast = progress: " + getKnightProgressLast());
        
    }


    public void resetProgress()
    {
        setKnightProgressCurrent(0f);
        setKnightProgressLast(0f);
        setRedMonsterDead(0);
    }

    public void addDamageDealtSkeletonCL (float damage) { statsRef.damageDealtSkeletonCL += damage; }

    public void addDamageDealtSpiderCL(float damage) { statsRef.damageDealtSpiderCL += damage; }

    public void addDamageDealtRedMonsterCL(float damage) { statsRef.damageDealtRedMonsterCL += damage; }

    public void addDamageDealtGreenMonsterCL(float damage) { statsRef.damageDealtGreenMonsterCL += damage; }

    public void saveLastStatsToGameMaster()
    {
        //Saving the current stats under the last stats applied, as they will now be changing
        GameMaster.gameMaster.skeletonHealthLast = GameMaster.gameMaster.skeletonHealthMultiplier;
        GameMaster.gameMaster.skeletonSpeedLast = GameMaster.gameMaster.skeletonSpeedMultiplier;
        GameMaster.gameMaster.skeletonAttackLast = GameMaster.gameMaster.skeletonAttackPowerMultiplier;
        GameMaster.gameMaster.skeletonAttackSpeedLast = GameMaster.gameMaster.skeletonAttackSpeedMultiplier ;
        GameMaster.gameMaster.skeletonAttackRangeLast = GameMaster.gameMaster.skeletonAttackRangeMultiplier;

        GameMaster.gameMaster.spiderHealthLast = GameMaster.gameMaster.spiderHealthMultiplier;
        GameMaster.gameMaster.spiderSpeedLast = GameMaster.gameMaster.spiderSpeedMultiplier;
        GameMaster.gameMaster.spiderWebFrequencyLast = GameMaster.gameMaster.spiderAttackSpeedMultiplier;
        GameMaster.gameMaster.spiderWebDamageLast = GameMaster.gameMaster.spiderAttackPowerMultiplier;

        GameMaster.gameMaster.redMonsterHealthLast = GameMaster.gameMaster.redMonsterHealthMultiplier;
        GameMaster.gameMaster.redMonsterSpeedLast = GameMaster.gameMaster.redMonsterSpeedMultiplier;
        GameMaster.gameMaster.redMonsterAttackPowerLast = GameMaster.gameMaster.redMonsterAttackPowerMultiplier;
        GameMaster.gameMaster.redMonsterAttackSpeedLast = GameMaster.gameMaster.redMonsterAttackSpeedMultiplier;
        GameMaster.gameMaster.redMonsterAttackRangeLast = GameMaster.gameMaster.redMonsterAttackRangeMultiplier;
        GameMaster.gameMaster.redMonsterFireballSpeedLast = GameMaster.gameMaster.redMonsterFireballSpeedMultiplier;

        GameMaster.gameMaster.greenHornsMonsterHealthLast = GameMaster.gameMaster.greenMonsterHealthMultiplier;
        GameMaster.gameMaster.greenHornsMonsterSpeedLast = GameMaster.gameMaster.greenMonsterSpeedMultiplier;
        GameMaster.gameMaster.greenHornsMonsterAttackPowerLast = GameMaster.gameMaster.greenMonsterAttackPowerMultiplier;
        GameMaster.gameMaster.greenHornsMonsterAttackSpeedLast = GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier;
        GameMaster.gameMaster.greenHornsMonsterAttackRangeLast = GameMaster.gameMaster.greenMonsterAttackRangeMultiplier;
        GameMaster.gameMaster.greenHornsMonsterFireballSpeedLast = GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier;
    }

    //This method is called from Knight.cs in the playerDamage() method if the knight has died
    public void applyPendingDeathMods()
    {
        saveLastStatsToGameMaster();
        for (int i = 0; i < someHandles.Length; i++)
        {
            if (someHandles[i].monster == MovingHandles.monsters.skeleton)       //If the someHandles[i] is a skeleton
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.skeletonHealthMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSkeletonHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[0] = 1;
                        Debug.Log("Current Percentage: " + someHandles[i].currentPercentage);
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.skeletonSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSkeletonSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[1] = 1;
                        Debug.Log("Current Percentage: " + someHandles[i].currentPercentage);
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.skeletonAttackPowerMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSkeletonAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[2] = 1;
                        Debug.Log("Current Percentage: " + someHandles[i].currentPercentage);
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.skeletonAttackSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSkeletonAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[3] = 1;
                        Debug.Log("Current Percentage: " + someHandles[i].currentPercentage);
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.spider)       //If the someHandles[i] is a skeleton
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.spiderHealthMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSpiderHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[4] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.spiderSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSpiderSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[5] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.spiderAttackPowerMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSpiderAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[6] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.spiderAttackSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentSpiderAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[7] = 1;
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.redMonster)
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.redMonsterHealthMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentRedMonsterHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[8] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.redMonsterSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentRedMonsterSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[9] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.redMonsterAttackPowerMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentRedMonsterAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[10] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.redMonsterAttackSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentRedMonsterAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[11] = 1;
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.greenMonster)
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.greenMonsterHealthMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentGreenMonsterHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[12] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.greenMonsterSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentGreenMonsterSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[13] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.greenMonsterAttackPowerMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentGreenMonsterAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[14] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].deathSlider.value <= 3 )
                    {
                        GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier = (convertPercentage(someHandles[i].deathSlider.value, i, true));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].deathSlider.value, i, true) * 100);
                        GameMaster.gameMaster.currentGreenMonsterAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[15] = 1;
                    }
                }
            } 
        }
    }

    //This method is used to convert the slider percentages into its' corresponding multiplier
    float convertPercentage(float sliderValue, int handleIndex, bool deathMods)
    {
        float percentage = 0;
        if (deathMods == false)
        {
            if (sliderValue == 5) { percentage = someHandles[handleIndex].currentPercentage + 5; }
            else if (sliderValue == 6) { percentage = someHandles[handleIndex].currentPercentage + 10; }
            else if (sliderValue == 7) { percentage = someHandles[handleIndex].currentPercentage + 15; }
            else if (sliderValue == 8) { percentage = someHandles[handleIndex].currentPercentage + 20; }
            Debug.Log ("Finished converting Percentage for" + someHandles[handleIndex].monster + " " + someHandles[handleIndex].sliderType + ": " + percentage);
        }
        else if (deathMods)
        {
            if (sliderValue == 3) { percentage = someHandles[handleIndex].currentPercentage - 5; }
            else if (sliderValue == 2) { percentage = someHandles[handleIndex].currentPercentage - 10; }
            else if (sliderValue == 1) { percentage = someHandles[handleIndex].currentPercentage - 15; }
            else if (sliderValue == 0) { percentage = someHandles[handleIndex].currentPercentage - 20; }
            Debug.Log("Percentage for " + someHandles[handleIndex].monster+ " " +someHandles[handleIndex].sliderType + ": " + percentage);
            
        }

        if (percentage > 200)
        {
            percentage = 200;
        }
        else if (percentage < 0)
        {
            percentage = 0;
        }
        
        return percentage / 100;
    }

    //This method is called from LevelTrigger.cs in the triggerNextLevel() method when the knight has made it to a new checkpoint
    public void applyPendingCheckpointMods()
    {
        saveLastStatsToGameMaster();
        Debug.Log("Trying to apply checkpoint changes");
        for (int i = 0; i < someHandles.Length; i++)
        {
            if (someHandles[i].monster == MovingHandles.monsters.skeleton)       //If the someHandles[i] is a skeleton
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    Debug.Log(someHandles[i].checkpointSlider.value);
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        Debug.Log("Made it in");
                        GameMaster.gameMaster.skeletonHealthMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSkeletonHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[0] = 1;
                        Debug.Log("Current Percentage: " + GameMaster.gameMaster.currentSkeletonHealth);

                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    Debug.Log(someHandles[i].checkpointSlider.value);
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.skeletonSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSkeletonSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[1] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    Debug.Log(someHandles[i].checkpointSlider.value);
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.skeletonAttackPowerMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSkeletonAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[2] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    Debug.Log(someHandles[i].checkpointSlider.value);
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.skeletonAttackSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSkeletonAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[3] = 1;
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.spider)       //If the someHandles[i] is a spider
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.spiderHealthMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSpiderHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[4] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.spiderSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSpiderSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[5] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.spiderAttackPowerMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSpiderAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[6] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.spiderAttackSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentSpiderAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[7] = 1;
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.redMonster)       //If the someHandles[i] is a redMonster
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.redMonsterHealthMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentRedMonsterHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[8] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.redMonsterSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentRedMonsterSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[9] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.redMonsterAttackPowerMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentRedMonsterAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[10] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.redMonsterAttackSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentRedMonsterAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[11] = 1;
                    }
                }
            }
            else if (someHandles[i].monster == MovingHandles.monsters.greenMonster)       //If the someHandles[i] is a greenMonster
            {
                if (someHandles[i].sliderType == MovingHandles.allSliderTypes.health)    //If it's the health slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.greenMonsterHealthMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentGreenMonsterHealth = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[12] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.speed)   //If it's the speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.greenMonsterSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentGreenMonsterSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[13] = 1;

                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackPower)   //If it's the Attack Power slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.greenMonsterAttackPowerMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentGreenMonsterAttackPower = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[14] = 1;
                    }
                }
                else if (someHandles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)   //If it's the attack speed slider
                {
                    if (someHandles[i].checkpointSlider.value >= 5) {
                        GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier = (convertPercentage(someHandles[i].checkpointSlider.value, i, false));
                        someHandles[i].currentPercentage = (convertPercentage(someHandles[i].checkpointSlider.value, i, false) * 100);
                        GameMaster.gameMaster.currentGreenMonsterAttackSpeed = (int)someHandles[i].currentPercentage;
                        GameMaster.gameMaster.updatedStats[15] = 1;
                    }
                }
            }
        }
        updateSliderRef.updateHandlePercentages();
        GameMaster.gameMaster.pendingCheckpointChanges = false;
    }

    //This resets the array which stores the indices of the handles that have been adjusted
    public void resetUpdatedStats()
    {
        for (int i = 0; i < 16; i++)
        {
            GameMaster.gameMaster.updatedStats[i] = 0;
        }
        GameMaster.gameMaster.dataReset = false;
    }
}
