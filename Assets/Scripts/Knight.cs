using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    GameObject gameMasterRef;
    ArtificialIntelligence AIRef;

    StatsManager statsManagerRef;

    WriteData writeDataRef;

    uiSwitchScript uiSwitchRef;

    respawnScreen respawnScriptRef;

    Stats statsRef;

    public health healthUI;

    private Vector2 spawnLocation;

    public GameObject Target;
    public GameObject[] TargetList;

    private Vector2 angle;

    private float currentAngle;
    public bool canMove;
    float time;

    private Rigidbody rb;
    private float auto;

    public GameObject damaged;

    //Because the call the write data is in isALive(), which happens to be called from FixedUpdate()
    //This is needed so that we only write the data once
    bool wroteData;

    //private Player thisPlayer;

    Animator anim;
    Animator anim2;
    Animator anim3;

    GameObject AIController;
    GameObject LT;

    float skeleAttack;
    float hpValue;

    float nextAttack;

    public AudioClip[] audioClip;
    private AudioSource audioa;


    float attackPerk;

    void Start()
    {

        gameMasterRef = GameObject.Find("GameMaster");

        respawnScriptRef = FindObjectOfType<respawnScreen>();

        writeDataRef = FindObjectOfType<WriteData>();

        AIRef = FindObjectOfType<ArtificialIntelligence>();

        statsManagerRef = FindObjectOfType<StatsManager>();
        statsRef = FindObjectOfType<Stats>();
        uiSwitchRef = FindObjectOfType<uiSwitchScript>();
        audioa = GetComponent<AudioSource>();

        attackPerk = 100;
        audioa = GetComponent<AudioSource>();
        skeleAttack = 0.7f;
        AIController = FindObjectOfType<ArtificialIntelligence>().gameObject;

        hpValue = 7f;

        statsManagerRef.setKnightHealth(100);

        statsManagerRef.setKnightDamageResistance(1);
        statsManagerRef.setKnightAttackPower(1);

        time = 0;
        nextAttack = 0;
        anim = GetComponentInChildren<Animator>();
        anim2 = damaged.GetComponent<Animator>();

        wroteData = false;

        canMove = true;
        //thisPlayer = GetComponent<Player>();

        rb = GetComponent<Rigidbody>();
        currentAngle = (Mathf.Rad2Deg * ((-Mathf.Atan2(Input.GetAxis("RSH"), Input.GetAxis("RSV")))));
        statsManagerRef.setKnightAcceleration(95); // allows for normalized 1-10


        transform.position = statsManagerRef.getKnightSpawnPosition();
        spawnLocation = this.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        totalTime();
        meleeAttack();
    }


    void FixedUpdate()
    {
        isAlive();
        playerOrientation();
        playerMove();
    }



    void PlaySound(int clip)
    {
        if (!(audioa.isPlaying && audioa.clip == audioClip[clip]))
        {
            audioa.clip = audioClip[clip];
            audioa.Play();
        }
    }

    IEnumerator respawnScreenDelay()
    {
        Debug.Log("Starting the waiting period");
        yield return new WaitForSeconds(5);
        Time.timeScale = 1f;
        Debug.Log("Waited for 5 Seconds, now what");
        loadLevel();

    }

    void isAlive()
    {
        healthUI.setHealth(statsManagerRef.getKnightHealth());
        if (statsManagerRef.getKnightIsAlive() == false)
        {
            if (!wroteData)
            {
                statsManagerRef.reportProgression();
                if (uiSwitchRef.uiSwitch.value == 0)
                {
                    AIController.GetComponent<ArtificialIntelligence>().AI(false);
                }
                else if (uiSwitchRef.uiSwitch.value == 1)
                {
                    if (GameMaster.gameMaster.pendingDeathChanges && !wroteData)
                    {
                        statsManagerRef.applyPendingDeathMods();

                        writeDataRef.writeData(true);
                    }
                }
                writeDataRef.resetData();
                wroteData = true;
            }

            Time.timeScale = 0f;

            //Before the level gets reloaded, we want to display the respawn screen
            respawnScriptRef.determineFadeDir();
        }
    }



    public void loadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);

        Destroy(this);
    }

    public void setLT(GameObject LTIN)
    {
        LT = LTIN;
    }


    void meleeAttack()
    {

        //  if ((Input.GetButtonDown("attack"))) { 
        if ((Input.GetAxis("RSH") > 0.25f || Input.GetAxis("RSH") < -0.25f || Input.GetAxis("RSV") > 0.25f || Input.GetAxis("RSV") < -0.25f) && time >= nextAttack)
        {


            // Debug.Log("knightMeleeAttack");
            anim.SetTrigger("attack"); ;
            nextAttack = time + 0.5f;
        }

    }

    void playerMove()
    {
        if (rb.velocity.magnitude < statsManagerRef.getKnightSpeed())
        {
            Vector2 vec = new Vector2(Input.GetAxis("LSH"), Input.GetAxis("LSV"));

            if (Vector2.Distance(vec, Vector2.zero) > 0.25f)
            {
                PlaySound(1);
                rb.AddForce(new Vector3(vec.x, 0, vec.y) * statsManagerRef.getKnightAcceleration());
            }
            else { audioa.Stop(); }

        }

    }

    void playerDamage(float damage, int monster)
    {
        if (monster == 1) { statsManagerRef.addDamageDealtSkeletonCL(damage); }
        else if (monster == 2) { statsManagerRef.addDamageDealtSpiderCL(damage); }
        else if (monster == 3) { statsManagerRef.addDamageDealtRedMonsterCL(damage); }
        else if (monster == 4) { statsManagerRef.addDamageDealtGreenMonsterCL(damage); }

        statsManagerRef.setKnightHealth(statsManagerRef.getKnightHealth() - damage);
        AIRef.HPOnWin = statsManagerRef.getKnightHealth();


        if (statsManagerRef.getKnightHealth() > 100)
        {
            statsManagerRef.setKnightHealth(100);
            AIRef.HPOnWin = 100;
        }
        if (statsManagerRef.getKnightHealth() <= 0)
        {
            statsManagerRef.setKnightIsAlive(false);
            AIRef.HPOnWin = 0;
        }
    }

    void targetSwitch()
    {
        if (statsManagerRef.getKnightTargetNumber() < TargetList.Length - 1)
        {
            statsManagerRef.setKnightTargetNumber(statsManagerRef.getKnightTargetNumber() + 1);
        }
        else
        {
            statsManagerRef.setKnightTargetNumber(0);
        }
        Target = TargetList[statsManagerRef.getKnightTargetNumber()];
    }


    public void restart()
    {
        angle = new Vector2(1f, 0f);
        gameObject.transform.position = spawnLocation;
    }


    void playerOrientation()
    {
        if (time >= nextAttack)
        {
            Vector2 vec = new Vector2(-Input.GetAxis("LSH"), Input.GetAxis("LSV"));

            float final = Mathf.Rad2Deg * (-Mathf.Atan2(vec.x, vec.y));
            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90, final, 0));
        }
    }

    void totalTime()
    {
        time += Time.deltaTime;

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "fireballRedMonster")
        {
            writeDataRef.addStatReceived("red", col.gameObject.GetComponent<fireBall>().getAttack(), 1);
            audioa.PlayOneShot(audioClip[0]);
            anim2.SetTrigger("this");
            playerDamage(statsManagerRef.getRedMonsterFinalAttack(), 3);
            col.gameObject.GetComponent<fireBall>().setAttack(0);
        }
        else if (col.transform.tag == "fireballGreenMonster")
        {
            writeDataRef.addStatReceived("green", col.gameObject.GetComponent<fireBall>().getAttack(), 1);
            audioa.PlayOneShot(audioClip[0]);
            anim2.SetTrigger("this");
            playerDamage(statsManagerRef.getGreenMonsterFinalAttack(), 4);
        }
        else if (col.transform.tag == "health")
        {
            audioa.PlayOneShot(audioClip[2]);

            if (statsManagerRef.getKnightHealth() != 100)
            {
                writeDataRef.addStatReceived("health", -hpValue, 1);
            }

            playerDamage(-hpValue, 0);

            col.gameObject.SetActive(false);
        }

    }

    void OnCollisionStay(Collision col)
    {

        if (col.transform.tag == "skeleton")
        {
            if (skeleAttack < time)
            {
                audioa.PlayOneShot(audioClip[0]);
                skeleAttack = time + 0.5f;
                anim2.SetTrigger("this");
                writeDataRef.addStatReceived("skeleton", col.gameObject.GetComponent<Skeleton>().getSkeletonAttack(), col.gameObject.GetComponent<Skeleton>().getId());

                playerDamage(statsManagerRef.getSkeletonFinalAttack(), 1);
            }

        }
    }


    IEnumerator webSpeed()
    {
        statsManagerRef.setKnightSpeed(45f);
        statsManagerRef.setKnightAcceleration(45f);
        yield return new WaitForSeconds(3f);
        statsManagerRef.setKnightSpeed(100f);
        statsManagerRef.setKnightAcceleration(100f);
    }

    void OnTriggerEnter(Collider col)
    {


        if (col.transform.tag == "groundWeb")
        {
            audioa.PlayOneShot(audioClip[0]);
            float webDamage = statsManagerRef.getSpiderBaseAttackPower() * statsManagerRef.getSpiderAttackPower();
            writeDataRef.addStatReceived("spider", webDamage, 1);


            StartCoroutine(webSpeed());

            playerDamage(webDamage, 2);
        }


    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "groundWeb")
        {
            statsManagerRef.setKnightSpeed(100f);
            statsManagerRef.setKnightAcceleration(100f);
        }
    }

}
