using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtificialIntelligence : MonoBehaviour
{

    private static ArtificialIntelligence instanceRef;

    uiSwitchScript uiRef;
    WriteData writeDataRef;
    StatsManager statsManagerRef;
    Stats statsRef;
    
    public int consecutiveDeathCount;
    public int consecutiveWin;

    public float lastDeathProgress;
    public float currentDeathProgress;

    public float HPOnWin;

    [SerializeField]
    GameObject[] knightSpawnPosition;

    // MOB DAMAGES
    float SkeletonADPD;

    float AiAttackPowerMultiplier;

    [SerializeField]
    GameObject[] levelTriggers;

    int x;
    
    int stage;
    
    
    //Variables used for the purpose of modifying the value of monsters' parameters
    float minorChangeAll;
    float moderateChangeAll;
    float majorChangeAll;
    float winMajorChangeMultiplier;
    float winModerateChangeMultiplier;
    float winMinorChangeMultiplier;

    float minLimit;
    float reStat;
    float maxLimit;

    bool thereIsProgress;

    float hpValue;

    int consDeathAdjustSkeleton;
    int consDeathAdjustRed;
    int consDeathAdjustGreen;
    int consDeathAdjustSpider;
    int adjustmentType;
    
    
    GameObject Kn;
    void Awake()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();
        statsRef = FindObjectOfType<Stats>();
        statsRef.spawnPositions = new Vector3[knightSpawnPosition.Length];
        foreach (GameObject G in knightSpawnPosition)
        {
            statsRef.spawnPositions[x] = G.transform.position;
            x++;
        }

        if (instanceRef == null)
        {

            instanceRef = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            DestroyImmediate(gameObject);
            DestroyImmediate(this);
        }

    }

    // Use this for initialization
    void Start()
    {
        uiRef = FindObjectOfType<uiSwitchScript>();
        
        writeDataRef = FindObjectOfType<WriteData>();

        
        writeDataRef = FindObjectOfType<WriteData>();

        

        int x = 0;
        


        consDeathAdjustSkeleton = 0;
        consDeathAdjustRed = 0;
        consDeathAdjustGreen = 0;
        consDeathAdjustSpider = 0;
        adjustmentType = 0;
        
        Kn = GameObject.Find("Knight");
        Kn.SetActive(true);

        thereIsProgress = false;
        
        consecutiveDeathCount = GameMaster.gameMaster.consecutiveDeathCount;
        consecutiveWin = 0;

        lastDeathProgress = 0;
        currentDeathProgress = 0;

        HPOnWin = 0;

        stage = 0;

        hpValue = 1f;

        AiAttackPowerMultiplier = 1f;

        minorChangeAll = 1.01f;
        moderateChangeAll = 1.03f;
        majorChangeAll = 1.07f;

        winMajorChangeMultiplier = 1.10f;
        winModerateChangeMultiplier = 1.03f;
        winMinorChangeMultiplier = 1.01f;

        minLimit = 0.4f;
        reStat = 0.55f;
}

    public void minAdjust()
    {
        statsManagerRef.saveLastStatsToGameMaster();
        // type: Skeleton 0, Red 1, Green 2, Spider 3
        if (GameMaster.gameMaster.skeletonHealthMultiplier < minLimit)
            GameMaster.gameMaster.skeletonHealthMultiplier = reStat;

        if (GameMaster.gameMaster.skeletonSpeedMultiplier < minLimit)
            GameMaster.gameMaster.skeletonSpeedMultiplier = reStat;

        if (GameMaster.gameMaster.skeletonAttackPowerMultiplier < minLimit)
            GameMaster.gameMaster.skeletonAttackPowerMultiplier = reStat;

        if (GameMaster.gameMaster.skeletonAttackRangeMultiplier < minLimit)
            GameMaster.gameMaster.skeletonAttackRangeMultiplier = reStat;

        if (GameMaster.gameMaster.redMonsterHealthMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterHealthMultiplier = reStat;

            if (GameMaster.gameMaster.redMonsterSpeedMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterSpeedMultiplier = reStat;

        if (GameMaster.gameMaster.redMonsterAttackPowerMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterAttackPowerMultiplier = reStat;

        if (GameMaster.gameMaster.redMonsterAttackRangeMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterAttackRangeMultiplier = reStat;

        if (GameMaster.gameMaster.redMonsterAttackSpeedMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterAttackSpeedMultiplier = reStat;

        if (GameMaster.gameMaster.redMonsterFireballSpeedMultiplier < minLimit)
            GameMaster.gameMaster.redMonsterFireballSpeedMultiplier = reStat;

        if (GameMaster.gameMaster.greenMonsterHealthMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterHealthMultiplier = reStat;

            if (GameMaster.gameMaster.greenMonsterSpeedMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterSpeedMultiplier = reStat;

            if (GameMaster.gameMaster.greenMonsterAttackPowerMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterAttackPowerMultiplier = reStat;

            if (GameMaster.gameMaster.greenMonsterAttackRangeMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterAttackRangeMultiplier = reStat;

            if (GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier = reStat;

            if (GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier < minLimit)
                GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier = reStat;

            if (GameMaster.gameMaster.spiderHealthMultiplier < minLimit)
                GameMaster.gameMaster.spiderHealthMultiplier = reStat;

            if (GameMaster.gameMaster.spiderAttackPowerMultiplier < minLimit)
                GameMaster.gameMaster.spiderAttackPowerMultiplier = reStat;

            if (GameMaster.gameMaster.spiderAttackSpeedMultiplier < minLimit)
                GameMaster.gameMaster.spiderAttackSpeedMultiplier = reStat;
    }
    
    public void win()
    {
        
        minAdjust();
        
        Debug.Log(HPOnWin);
        consecutiveWin++;
        consecutiveDeathCount = 0;
        GameMaster.gameMaster.consecutiveDeathCount = consecutiveDeathCount;
    }

    public void death()
    {
            consecutiveWin = 0;
            consecutiveDeathCount++;
        GameMaster.gameMaster.consecutiveDeathCount = consecutiveDeathCount;
    }
 
    void adjustment(float adjustment, int type)
    {
        Debug.Log("Adjustment Method called. Adjustments being made");
            // type: Skeleton 0, Red 1, Green 2, Spider 3
        if (type == 0)  //If skeletons killed the knight last...
        {
            if (consDeathAdjustSkeleton == 0)
            {
                GameMaster.gameMaster.skeletonAttackPowerMultiplier *= adjustment;
            }
            else if (consDeathAdjustSkeleton == 1)
            {
                GameMaster.gameMaster.skeletonHealthMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonAttackPowerMultiplier *= adjustment;
            }
            else if (consDeathAdjustSkeleton == 2)
            {
                GameMaster.gameMaster.skeletonAttackPowerMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonAttackRangeMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonAttackSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonSpeedMultiplier *= adjustment;
                consDeathAdjustSkeleton = 0;
            }
            if (consDeathAdjustSkeleton < 2)
            {
                consDeathAdjustSkeleton ++;
            }
            GameMaster.gameMaster.consecutiveSkeletonDeaths = consDeathAdjustSkeleton;
        }
            else if (type == 1)     //If red monsters killed the knight last...
            {
                if (consDeathAdjustRed == 0)
                {
                    GameMaster.gameMaster.redMonsterHealthMultiplier *= adjustment;
                }
                else if (consDeathAdjustRed == 1)
                {
                    GameMaster.gameMaster.redMonsterFireballSpeedMultiplier *= adjustment;
                    GameMaster.gameMaster.redMonsterSpeedMultiplier *= adjustment;
                }
                else if (consDeathAdjustRed == 2)
                {
                    GameMaster.gameMaster.redMonsterAttackPowerMultiplier *= adjustment;
                    GameMaster.gameMaster.redMonsterAttackRangeMultiplier *= adjustment;
                consDeathAdjustRed = 0;
                }
            if (consDeathAdjustRed < 2)
            {
                consDeathAdjustRed += 1;
            }
            GameMaster.gameMaster.consecutiveRedDeaths = consDeathAdjustRed;
            }
            else if (type == 2)     //If green monsters killed the knight last...
            {
                if (consDeathAdjustGreen == 0)
                {

                GameMaster.gameMaster.greenMonsterHealthMultiplier *= adjustment;
                }
                else if (consDeathAdjustGreen == 1)
                {

                    GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier *= adjustment;
                    GameMaster.gameMaster.greenMonsterSpeedMultiplier *= adjustment;

                }
                else if (consDeathAdjustGreen == 2)
                {
                    consDeathAdjustGreen = 0;
                    GameMaster.gameMaster.greenMonsterAttackPowerMultiplier *= adjustment;
                    GameMaster.gameMaster.greenMonsterAttackRangeMultiplier *= adjustment;
                }
                if (consDeathAdjustGreen < 2)
            {
                consDeathAdjustGreen++;
            }
            GameMaster.gameMaster.consecutiveGreenDeaths = consDeathAdjustGreen;

            }
            else if (type == 3)     //If Spiders killed the knight last...
            {
                if (consDeathAdjustSpider == 0)
                {
                    GameMaster.gameMaster.spiderHealthMultiplier *= adjustment;
                }
                else if (consDeathAdjustSpider == 1)
                {
                    GameMaster.gameMaster.spiderAttackPowerMultiplier *= adjustment;
                }
                else if (consDeathAdjustSpider == 2)
                {
                    consDeathAdjustSpider = 0;
                    GameMaster.gameMaster.spiderAttackPowerMultiplier *= adjustment;
                    GameMaster.gameMaster.spiderAttackSpeedMultiplier *= adjustment;
                }
                if (consDeathAdjustSpider < 2)
            {
                consDeathAdjustSpider++;
            }
            GameMaster.gameMaster.consecutiveSpiderDeaths = consDeathAdjustSpider;
            }
            else if (type == 4)     //Let's modify each parameter for each monster
            {
                GameMaster.gameMaster.skeletonHealthMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonAttackPowerMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.skeletonAttackRangeMultiplier *= adjustment;

                GameMaster.gameMaster.redMonsterHealthMultiplier *= adjustment;
                GameMaster.gameMaster.redMonsterFireballSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.redMonsterSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.redMonsterAttackPowerMultiplier *= adjustment;
                GameMaster.gameMaster.redMonsterAttackRangeMultiplier *= adjustment;


            GameMaster.gameMaster.greenMonsterHealthMultiplier *= adjustment;
                GameMaster.gameMaster.greenMonsterAttackPowerMultiplier *= adjustment;
                GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.greenMonsterSpeedMultiplier *= adjustment;
                GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier *= adjustment;

                GameMaster.gameMaster.spiderHealthMultiplier *= adjustment;
                GameMaster.gameMaster.spiderAttackPowerMultiplier *= adjustment;
            }
    }


    public void AI(bool win)
    {
        if (!win || win)
        {
            Debug.Log("AI is saving last stats to GameMaster");
            statsManagerRef.saveLastStatsToGameMaster();
        }
        Debug.Log("AI has been called to make adjustments to settings");
        float multiplier = 1.03f;
            int adjustType = 4;

            for (int i = 0; i < consecutiveDeathCount - 1; i++)
            {
                multiplier = 0.03f;
                if (!thereIsProgress)
                {
                    multiplier = multiplier + (0.015f * i);
                }
            }


            if (multiplier != 1.03f)
            {
                multiplier += 1;
            }
            
            for (int i = 0; i < consecutiveWin - 1; i++)
            {
                multiplier = multiplier * multiplier;
            }
            


            if (consecutiveWin > 2)
            {
                adjustType = 4;
            }

            if (win)
            {
                thereIsProgress = false;
                writeDataRef.deathProgress = "";
                if (consecutiveWin >= 2)        //If the knight makes it to a checkpoint WITH consecutive wins (has made it to 2 or more checkpoints in a row WITHOUT dying)
                {

                    if (HPOnWin > 60)
                    {
                        writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  > 60% - Major Adjustment With Penalty Adjustment factor of: " + majorChangeAll * multiplier * 1.10f;
                        adjustment(majorChangeAll * winMajorChangeMultiplier, adjustType);
                    }
                    else if (HPOnWin >= 20)
                    {
                        writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  >= 20%  && < 60% - Moderate Adjustment With Penalty Adjustment factor of: " + moderateChangeAll * multiplier * 1.07f;
                        adjustment(moderateChangeAll * winModerateChangeMultiplier, adjustType);
                    }
                    else
                    {
                        writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  < 20% - Minor Adjustment With Penalty Adjustment factor of: " + minorChangeAll * multiplier * 1.03f;
                        adjustment(minorChangeAll * winMinorChangeMultiplier, adjustType);
                    }

                }
                else       //If the Knight makes it to a checkpoint WITHOUT consecutive wins (died at least once during while making it to this checkpoint)
                {
                    if (HPOnWin > 60)
                    {
                        writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  > 60% - Moderate Adjustment: " + moderateChangeAll * multiplier * 1.09f;
                        adjustment(moderateChangeAll * multiplier * 1.09f, adjustType);
                    }
                    else if (HPOnWin >= 20)
                    {
                        writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  > 20%  && < 60% - Minor Adjustment: " + minorChangeAll * multiplier * 1.05f;
                        adjustment(minorChangeAll * multiplier * 1.05f, adjustType);
                    }
                else if (HPOnWin < 20)
                {
                    writeDataRef.deathProgress = "Completion Efficiency - Health left on Checkpoint: " + (HPOnWin) + "%  < 20% - Very Minor Adjustment: " + minorChangeAll * 1.02f;
                    adjustment(minorChangeAll * 1.02f, adjustType);
                }
            }

            }
            else // death
            {
            death();
                string type;
                if (furtherDetermination() == 0)
                {
                    type = "skeleton";
                }
                else if (furtherDetermination() == 1)
                {
                    type = "redMonster";
                }
                else if (furtherDetermination() == 1)
                {
                    type = "greenMonster";
                }
                else
                {
                    type = "spider";
                }
                
                writeDataRef.deathProgress = "";
                if (GameMaster.gameMaster.consecutiveDeathCount >= 2)
                {

                    if ((GameMaster.gameMaster.progressActual/100) > .4)        //If the user made at least a 40% increase in progress from his last death on this checkpoint...
                    {

                        thereIsProgress = true;
                        writeDataRef.deathProgress = "Current Completion Progress: " + (GameMaster.gameMaster.progressCurrent * 100f) + " Last Completion Progress:" + (GameMaster.gameMaster.progressLast * 100f) + " Actual Progress:" + (GameMaster.gameMaster.progressActual) + "%" + System.Environment.NewLine + "                  Minor adjustment on " + type + " required: " + 0.95 + " Overall adjustment: 0.96f";
                        adjustment(0.96f, 4);
                        adjustment(0.97f, furtherDetermination());
                    }
                    else if ((GameMaster.gameMaster.progressActual / 100) > 0.18)     //If the user made at least an 18% increase in progress from his last death on this checkpoint...
                    {

                        writeDataRef.deathProgress = "Current Completion Progress: " + (GameMaster.gameMaster.progressCurrent * 100f) + " Last Completion Progress:" + (GameMaster.gameMaster.progressLast * 100f) + " Actual Progress:" + (GameMaster.gameMaster.progressActual)  + "%" + System.Environment.NewLine + "                  Minor adjustment on " + type + " required: " + (1f / (float)(minorChangeAll * multiplier)) + " Overall adjustment: 0.94f";

                        adjustment(0.94f, 4);
                        adjustment(1f / (minorChangeAll * multiplier), furtherDetermination());
                        thereIsProgress = false;

                    }
                    else if ((GameMaster.gameMaster.progressActual / 100) > 0.02)     //If the user made at least a 2% increase in progress from his last death on this checkpoint...
                    {
                        thereIsProgress = false;
                        writeDataRef.deathProgress = "Current Completion Progress: " + (GameMaster.gameMaster.progressCurrent * 100f) + " Last Completion Progress:" + (GameMaster.gameMaster.progressLast * 100f) + " Actual Progress:" + (GameMaster.gameMaster.progressActual) + "%" + System.Environment.NewLine + "                  Moderate adjustment on " + type + " required: " + (1f / (float)(moderateChangeAll * multiplier * 1.04f)) + " Overall adjustment: 0.91f";

                        adjustment(0.91f, 4);
                        adjustment(1f / (moderateChangeAll * multiplier * 1.04f), furtherDetermination());
                    }
                    else                                 //If the user couldn't even make a 2% increase in progress from his last death on this checkpoint...
                    {
                        thereIsProgress = false;
                        writeDataRef.deathProgress = "Current Completion Progress: " + (GameMaster.gameMaster.progressCurrent * 100f) + " Last Completion Progress:" + (GameMaster.gameMaster.progressLast * 100f) + " Actual Progress:" + (GameMaster.gameMaster.progressActual) + "%" + System.Environment.NewLine + "                  Major adjustment on " + type + " required: " + (1f / (float)(majorChangeAll * multiplier * 1.09f)) + " Overall adjustment: 0.85f";

                        adjustment(0.85f, 4);
                        adjustment(1 / (majorChangeAll * multiplier * 1.09f), furtherDetermination());
                    }
                }
                else                       //If this is the first time the user has died on this checkpoint...
                {
                    thereIsProgress = true;
                    writeDataRef.deathProgress = "Death after checkpoint, very minor adjustment on " + type + " required: " + 0.95 + " Overall adjustment: 0.96f";
                    adjustment(0.96f, 4);
                    adjustment(0.97f, furtherDetermination());
                }
            writeDataRef.writeData(true);
        }
        GameMaster.gameMaster.progressLast = GameMaster.gameMaster.progressCurrent;

        
    }
    
    int furtherDetermination()
    {
        int maxReturn = 0;
            float max = writeDataRef.damageReceived[0].damage;


            for (int i = 0; i < 4; i++)
            {
                if (max < writeDataRef.damageReceived[i].damage)
                {
                    max = writeDataRef.damageReceived[i].damage;
                    maxReturn = i;
                Debug.Log("Need to FURTHER ADJUST the " + i);
                }

            }
        return maxReturn;
    }
    
    // ********************* GETTERS ************************ 

    public float getHpValue()
    {
        return hpValue;
    }


    public float getAttack()
    {
        return AiAttackPowerMultiplier;
    }
}