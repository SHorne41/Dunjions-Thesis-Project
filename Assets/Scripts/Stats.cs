using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    ArtificialIntelligence aiRef;

    //Slider Attributes
    public float[] skeletonTickMarks;
    
    
    //Progression Stats
    public int redMonsterDead;
    public int greenHornsMonsterDead;
    public int skeletonDead;
    public int spiderDead;
    
    public float progressMax;

    public float progressCurrent;
    public float progressLast;
    public float actualProgress;


    //Knight Stats
    public float speed;
    public float accel;
    public int targetNumber;
    public float hp;            //Users' hp
    public float rate;          //Users' health regeneration rate
    public float resistance;    //Users' damage resistance
    public float power;         //Users' attack power multiplier
    public bool isAlive;
    
    public Vector3[] spawnPositions;
    public int currentSpawn;
    public int previousSpawn;


    // SKELETON ATTRIBUTES
    public float skeletonHealth;
    public float skeletonSpeed;
    public float skeletonAttack;
    public float skeletonAttackSpeed;
    public float skeletonAttackRange;

    // Skeleton Base Attributes
    public float skeletonBaseHealth;
    public float skeletonBaseSpeed;
    public float skeletonBaseAttackPower;
    public float skeletonBaseAttackSpeed;
    public float skeletonBaseAttackRange;

    public float damageDealtSkeletonCL;

    // SPIDER ATTRIBUTES
    public float spiderHealth;
    public float spiderSpeed;
    public float spiderWebFrequency;
    public float spiderWebDamage;

    // Spider Base Attributes
    public float spiderBaseHealth;
    public float spiderBaseSpeed;
    public float spiderBaseWebFrequency;
    public float spiderBaseWebDamage;

    public float damageDealtSpiderCL;

    // RED MONSTER ATTRIBUTES
    public float redMonsterHealth;
    public float redMonsterSpeed;
    public float redMonsterAttackPower;
    public float redMonsterAttackSpeed;
    public float redMonsterAttackRange;
    public float redMonsterFireballSpeed;

    //Red Monster Base Attributes
    public float redMonsterBaseHealth;
    public float redMonsterBaseSpeed;
    public float redMonsterBaseAttackPower;
    public float redMonsterBaseAttackSpeed;
    public float redMonsterBaseAttackRange;
    public float redMonsterBaseFireballSpeed;

    public float damageDealtRedMonsterCL;

    // GREEN HORNS MONSTER ATTRIBUTES
    public float greenHornsMonsterHealth;
    public float greenHornsMonsterSpeed;
    public float greenHornsMonsterAttackPower;
    public float greenHornsMonsterAttackSpeed;
    public float greenHornsMonsterFireballSpeed;
    public float greenHornsMonsterAttackRange;

    //Green Monster Base Attributes
    public float greenHornsMonsterBaseHealth;
    public float greenHornsMonsterBaseSpeed;
    public float greenHornsMonsterBaseAttackPower;
    public float greenHornsMonsterBaseAttackSpeed;
    public float greenHornsMonsterBaseFireballSpeed;
    public float greenHornsMonsterBaseAttackRange;

    public float damageDealtGreenMonsterCL;

    //Final Attributes
    public float skeletonFinalAttack;
    public float redMonsterFinalAttack;
    public float greenMonsterFinalAttack;
    public float spiderFinalAttack;
    
    // Use this for initialization
    void Awake () {
        aiRef = FindObjectOfType<ArtificialIntelligence>();

        //Progression Stats Initialization
        redMonsterDead = 0;
        greenHornsMonsterDead = 0;
        skeletonDead = 0;
        spiderDead = 0;
        progressMax = 1;
        speed = 100;
     accel = 1;
     targetNumber = 1;
     hp = 100;            
     rate = 1;          
     resistance = 1;    
     power = 1;
     isAlive = true;

        if (GameMaster.gameMaster != null)
        {
            currentSpawn = GameMaster.gameMaster.currentCheckpoint;

        }
        else
            currentSpawn = 0;
        previousSpawn = 0;
     

        progressCurrent = GameMaster.gameMaster.progressCurrent;
        progressLast = GameMaster.gameMaster.progressLast;
        actualProgress = GameMaster.gameMaster.progressActual;

        //Initializing array of tick marks for Skeleton
        skeletonTickMarks = new float[9] {283f, 300f, 318f, 335f, 353f, 370f, 388f, 406f, 424f };  
    
     //Initializing Base Stats Skeleton
     skeletonBaseHealth = 10;
     skeletonBaseSpeed = 6.5f;
     skeletonBaseAttackPower = 5;
     skeletonBaseAttackSpeed = 1;
     skeletonBaseAttackRange = 0.6f;
        damageDealtSkeletonCL = 0;

     //Initializing stat modifiers
        skeletonHealth = GameMaster.gameMaster.skeletonHealthMultiplier;
        skeletonSpeed = GameMaster.gameMaster.skeletonSpeedMultiplier;
        skeletonAttack = GameMaster.gameMaster.skeletonAttackPowerMultiplier;
        skeletonAttackSpeed = GameMaster.gameMaster.skeletonAttackSpeedMultiplier;
        skeletonAttackRange = 1;

        //Final stats
        skeletonFinalAttack = 0;
        redMonsterFinalAttack = 0;
        greenMonsterFinalAttack = 0;
        spiderFinalAttack = 0;
        
    
     //Initialization Base Stats Spider   
     spiderBaseHealth = 15;
     spiderBaseSpeed = 6f;
     spiderBaseWebFrequency = 2;
     spiderBaseWebDamage = 1;
        damageDealtSpiderCL = 0;

        //Initializing stat modifiers
        spiderHealth = GameMaster.gameMaster.spiderHealthMultiplier;
        spiderSpeed = GameMaster.gameMaster.spiderSpeedMultiplier;
        spiderWebFrequency = GameMaster.gameMaster.spiderAttackSpeedMultiplier;
        spiderWebDamage = GameMaster.gameMaster.spiderAttackPowerMultiplier;
     
     //Initialization Base Stats Red Monster    
     redMonsterBaseHealth = 25;
     redMonsterBaseSpeed = 5.5f;
     redMonsterBaseAttackPower = 6;
     redMonsterBaseAttackSpeed = 1;
     redMonsterBaseAttackRange = 0.7f;
     redMonsterBaseFireballSpeed = 1;
        damageDealtRedMonsterCL = 0;

        //Initializing stat modifiers
        redMonsterHealth = GameMaster.gameMaster.redMonsterHealthMultiplier;
        redMonsterSpeed = GameMaster.gameMaster.redMonsterSpeedMultiplier;
        redMonsterAttackPower = GameMaster.gameMaster.redMonsterAttackPowerMultiplier;
        redMonsterAttackSpeed = GameMaster.gameMaster.redMonsterAttackSpeedMultiplier;
        redMonsterAttackRange = 1;
        redMonsterFireballSpeed = 1;

        //Initialization Base Stats Green Monster
     greenHornsMonsterBaseHealth = 120;
     greenHornsMonsterBaseSpeed = 1.65f;
     greenHornsMonsterBaseAttackPower = 12;
     greenHornsMonsterBaseAttackSpeed = 1;
     greenHornsMonsterBaseFireballSpeed = 1;
     greenHornsMonsterBaseAttackRange = 1;
        damageDealtGreenMonsterCL = 0;

        //Initialization Stat Modifiers
        greenHornsMonsterHealth = GameMaster.gameMaster.greenMonsterHealthMultiplier;
        greenHornsMonsterSpeed = GameMaster.gameMaster.greenMonsterSpeedMultiplier;
        greenHornsMonsterAttackPower = GameMaster.gameMaster.greenMonsterAttackPowerMultiplier;
        greenHornsMonsterAttackSpeed = GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier;
        greenHornsMonsterFireballSpeed = 1;
        greenHornsMonsterAttackRange = 1;
    }
}
