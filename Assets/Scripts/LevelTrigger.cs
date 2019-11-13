using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTrigger : MonoBehaviour {

    StatsManager statsManagerRef;
    updateProgressGraph updateGraphRef;
    uiSwitchScript uiRef;
    WriteData writeDataRef;
    public Text checkpointNumber;

    bool wroteData;

    //Used for debugging
    //bool newCheck;

    [SerializeField]
    health Skeleton;

    [SerializeField]
    health RedMonster;

    [SerializeField]
    health Spider;


    [SerializeField]
    GameObject[] trigger;

    [SerializeField]
    public int redMonsterDeadX;
    [SerializeField]
    public int greenHornsMonsterDeadX;
    [SerializeField]
    public int skeletonDeadX;
    [SerializeField]
    public int spiderDeadX;


    [SerializeField]
    GameObject NextLevelDoor;

    GameObject AI;

    GameObject Knight;

    bool firstInAlternate;

    [Header("Arrow")]

    [Header("Arrows")]
    [SerializeField]
    GameObject[] arrows;

    [SerializeField]
    public int triggerNumber;

    // Use this for initialization
    void Start () {
        //Used for debugging
        //newCheck = false;
        checkpointNumber.text = (GameMaster.gameMaster.currentCheckpoint + 1) + "";
        statsManagerRef = FindObjectOfType<StatsManager>();
        updateGraphRef = FindObjectOfType<updateProgressGraph>();
        uiRef = FindObjectOfType<uiSwitchScript>();
        writeDataRef = FindObjectOfType<WriteData>();
        statsManagerRef.setKnightProgressCurrent(0);
        firstInAlternate = true;
        Knight = GameObject.Find("Knight");
        AI = GameObject.Find("AI");
        wroteData = false;
    }

    

    public void setDead(string monster)
    {
        if (monster == "redMonster")
        {
            statsManagerRef.setRedMonsterDead(1);
        }
        else if (monster == "greenHornsMonster")
        {
            statsManagerRef.setGreenHornsMonsterDead(1);
        }
        else if (monster == "spider")
        {
            statsManagerRef.setSpiderDead(1);
        }
        else if (monster == "skeleton")
        {
            statsManagerRef.setSkeletonDead (1);
        }
    }

    // Update is called once per frame
    void Update () {
        if (triggerNumber == statsManagerRef.getCurrentSpawnLocation())
        {
            triggerNextLevel();
            accessBoard();
            if (statsManagerRef.levelTriggerRef != this)
            {
                statsManagerRef.levelTriggerRef = this;
                wroteData = false;
            }
        }
    }

    //This is the method used to change the number of monsters remaining in the current checkpoint
    void accessBoard()
    {
        if (AI != null)
        {
                //The following lines of code subtract the number of Monsters that were killed
                //from the number of monsters that are required to be killed in order to proceed
                //To the next checkpoint

                Skeleton.setHealth(skeletonDeadX - statsManagerRef.getSkeletonDead());
                RedMonster.setHealth(redMonsterDeadX - statsManagerRef.getRedMonsterDead());
                Spider.setHealth(spiderDeadX - statsManagerRef.getSpiderDead());
            }
    }


    void triggerNextLevel()
    {
        if (statsManagerRef.getRedMonsterDead() >= redMonsterDeadX && statsManagerRef.getSpiderDead() >= spiderDeadX && statsManagerRef.getSkeletonDead() >= skeletonDeadX)
        {
            //newCheck = true;

            if (NextLevelDoor != null)
            {
                foreach (GameObject G in arrows)
                {
                    G.gameObject.SetActive(true);
                }
                if (triggerNumber != 5)
                {
                    if (!wroteData)
                    {
                        statsManagerRef.setCurrentSpawnLocation(statsManagerRef.getCurrentSpawnLocation() + 1);
                        checkpointNumber.text = (GameMaster.gameMaster.currentCheckpoint + 1) + "";
                        statsManagerRef.setRedMonsterDead(0);
                        statsManagerRef.setSkeletonDead(0);
                        statsManagerRef.setSpiderDead(0);
                        if (uiRef.uiSwitch.value == 0)     //AI has been turned ON and UI is OFF
                        {
                            AI.GetComponent<ArtificialIntelligence>().win();
                            AI.GetComponent<ArtificialIntelligence>().AI(true);
                        }
                        else if (uiRef.uiSwitch.value == 1) //AI has been turned OFF and UI is ON
                        {
                            if (GameMaster.gameMaster.pendingCheckpointChanges)
                            {
                                statsManagerRef.applyPendingCheckpointMods();
                            }
                        }
                        statsManagerRef.resetProgress();
                        writeDataRef.writeData(false);
                        wroteData = true;
                    } 
                }
                updateGraphRef.deleteBars();
                NextLevelDoor.GetComponent<NextLevelDoor>().openDoor();
            }
        }
    }



    IEnumerator delaySpawner(float time, int x)
    {
    
        yield return new WaitForSeconds(time);
        trigger[x].SetActive(true);
        trigger[x].GetComponent<MonsterSpawner>().setLevel(this.gameObject);
        trigger[x].GetComponent<MonsterSpawner>().trigger();

    }

    void OnTriggerEnter(Collider col)
    {
        int i = 0;
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Knight>().setLT(gameObject);
            foreach (GameObject MonsterSpawner in trigger)
            {

                // Debug.Log("TRIGGERED!");
                if (MonsterSpawner != null && col.tag == "Player")
                {
                    StartCoroutine(delaySpawner(i * 0.02f, i));

                }

                i++;

            }
        }
       
    }

}
