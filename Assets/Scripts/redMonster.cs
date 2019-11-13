using UnityEngine;
using System.Collections;

public class redMonster: MonoBehaviour
{
    StatsManager statsManagerRef;

    WriteData writeDataRef;

    // Game Objects
    public GameObject fireBall;
    Transform spriteTransform;
    public Rigidbody rigidBody;
    public GameObject attackSpot;

    string monsterType;

    // Movement Variables
    float speedforce;
    Vector3 direction;
    int timer;
    float final;

    bool freak;
    bool inRun;
    Vector3 rando;
    float inRunTime;

    public float attackSpeed;
    // Skeleton Attributes
    public float health;
    public float speed;
    public GameObject Knight;
    float time;
    float time2;
	float time3;
	int time3INT;
    bool canMove;
    bool notAroundCornerYet;
    Vector3 lastPosition;
    bool canBeAttacked;
    bool canRedirect;

    float attackTime;
    float attackDamage;
    Animator anim;
    GameObject AIController;


    public float speedLerp;
    private float startTime;
    private float journeyLength;
    float aiAttackRange;

    bool timeRandom;

    GameObject LevelTrigger;

    [SerializeField]
    GameObject healthUI;

    float healthMax;

    bool notDead;

    public void setLevel(GameObject LT)
    {
        LevelTrigger = LT;
    }
    public GameObject getLevel()
    {
        return LevelTrigger;
    }

    void Awake()
    {
        AIController = GameObject.Find("AI");
        Knight = GameObject.Find("Knight");
        

    }

    void Start()
    {

        statsManagerRef = FindObjectOfType<StatsManager>();
        writeDataRef = FindObjectOfType<WriteData>();

        freak = false;
        inRunTime = 0f;
        rando = new Vector3(-2f, 0f, 2f);
        inRun = false;
       // System.IO.File.AppendAllText("C:/Dunjions/Output/test.txt", System.Environment.NewLine + "This is text that goes into the text file");
     
        lastPosition = transform.position;

        timeRandom = true;
        speedLerp = 1.0F;
     
    
		health = statsManagerRef.getRedMonsterBaseHealth() + (35f * GameMaster.gameMaster.redMonsterHealthMultiplier);
        aiAttackRange = statsManagerRef.getRedMonsterBaseAttackRange() + (0.6f * GameMaster.gameMaster.redMonsterAttackRangeMultiplier );
		speedforce = statsManagerRef.getRedMonsterBaseSpeed() * GameMaster.gameMaster.redMonsterSpeedMultiplier;
		attackDamage = statsManagerRef.getRedMonsterBaseAttackPower() + (13f * GameMaster.gameMaster.redMonsterAttackPowerMultiplier);
        statsManagerRef.setRedMonsterFinalAttack(attackDamage);
        healthMax = health;


       // Debug.Log("attackDamage 2: " + getRedMonsterAttack());

        canRedirect = true;
        canBeAttacked = true;
        monsterType = "redMonster";
        notAroundCornerYet = true;
        canMove = true;
        time = 0;
        attackTime = 3f;
        anim = GetComponentInChildren<Animator>();
      
        timer = 0;
        spriteTransform = this.gameObject.transform.GetChild(0);
        notDead = true;
    }



    void Update()
    {

        if (notDead)
        {
            healthCheck();
            totalTime();
            movement();
            attack();
            getLastPosition();
            
        }
        //  Debug.Log("child: " + transform.GetChild(0).transform.rotation); 
    }

    public void startMoving(bool isM)
    {

        Vector3 rando2 = new Vector3(Random.Range(-100, 100), transform.position.y, Random.Range(-100, 100));
        if (isM)
        {
            direction = directionTo(new Vector3(Random.Range(-1f, 1f), gameObject.transform.position.y, Random.Range(-1f, 1f)), new Vector3());
            canRedirect = true;



            setDirection(direction, 10f);
        }
    }

    public float getRedMonsterAttack()
    {
        return attackDamage;
    }

    Vector3 directionTo(Vector3 first, Vector3 second)
    {
        Vector3 newVector;
        newVector = first - second;

        newVector = new Vector3(newVector.x, -0.24f, newVector.z);
        return newVector;
    }

    public string getMonsterType()
    {
        return monsterType;
    }


    IEnumerator timeRandomTimer(float time)
    {
        timeRandom = false;
        yield return new WaitForSeconds(time);
        timeRandom = true;
    }


    IEnumerator canMoveToggle(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void setDirection(Vector3 inPos)
    {

        GetComponent<Rigidbody>().AddForce(directionTo(this.transform.position, inPos) * speedforce * 0.3f);

        
    }

    void movement() {
	
		if (((int)time3INT) % 1 == 0) {
			int layerMask = 1 << 8;
			if (!Physics.Linecast (transform.position, Knight.transform.position, ~layerMask)) {

				Vector3 thisDir = directionTo (Knight.transform.position, this.transform.position);

				final = Mathf.Rad2Deg * (Mathf.Atan2 (thisDir.x, thisDir.z));

                transform.GetChild(0).transform.rotation = Quaternion.Lerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(new Vector3(90, final + 180, 0)), Time.time * 0.02f);

                if (Vector3.Distance (this.transform.position, Knight.transform.position) <= 2f) {
					anim.SetBool ("moving", true);
                    setDirection(Knight.transform.position);


				}

				if (canMove) {

					float randomR = Random.Range (0f, 3f);
					float randomT = Random.Range (0.5f, 1.0f);

					if (timeRandom) {

                        if (gameObject != null)
                        {
                            StartCoroutine(timeRandomTimer(Random.Range(0.3f, 1.2f)));
                        }

						if (randomR > Random.Range (2.0f, 2.4f)) {
				
							StartCoroutine (timeRandomTimer (randomT));
							StartCoroutine (canMoveToggle (Random.Range (0.2f, 1.2f)));
						}
					}


                    // transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90, final + 180, 0));

                    //keep distance from player if player gets too close

                    if (Vector3.Distance(this.transform.position, Knight.transform.position) <= 3f)
                    {
                        
                        anim.SetBool("moving", true);
                        GetComponent<Rigidbody>().AddForce(directionTo(this.transform.position, Knight.transform.position) * speedforce * 1.15f);



                    }

                    //keep distance from player if player tries  leave
                    else if (Vector3.Distance (this.transform.position, Knight.transform.position) >= 6.2f && Vector3.Distance (this.transform.position, Knight.transform.position) <= 12.5f) {
						anim.SetBool ("moving", true);
						GetComponent<Rigidbody> ().AddForce (thisDir * (speedforce - 2.3f));
					} else {
						anim.SetBool ("moving", false);
					}
				}
            // monster marchs towards the corner the knight just turned. 
            else if (notAroundCornerYet && Vector3.Distance (this.transform.position, Knight.transform.position) >= 6.2f && Vector3.Distance (this.transform.position, Knight.transform.position) <= 25f) {
					GetComponent<Rigidbody> ().AddForce (directionTo (lastPosition, this.transform.position));
					if (lastPosition == transform.position) {
						notAroundCornerYet = false;
					}
				}
			} else {
               

                final = Mathf.Rad2Deg * (Mathf.Atan2(direction.x, direction.z));
                GetComponent<Rigidbody>().AddForce(direction * (speedforce));
                transform.GetChild(0).transform.rotation = Quaternion.Lerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(new Vector3(90, final + 180, 0)), Time.time * 0.02f);
            }
		}
    }





    void attack() {
		if ((time3INT) % 1 == 0) {
			int layerMask = 1 << 8;
            if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask)) {
                if (time > attackTime && Vector3.Distance(this.transform.position, Knight.transform.position) <= 10f * aiAttackRange) {
                    //canMoveToggle(0.9f);
                    anim.SetTrigger("attacking");

                    //Debug.Log("hey EXERCUTE HERE0");
                    // Debug.Log("thisPosition: " + this.transform.position + " Knight Position: " + Knight.transform.position + "Vector3 Normalized: " + (this.transform.position - Knight.transform.position).normalized + " EulerNorm: " + Quaternion.Euler((this.transform.position - Knight.transform.position).normalized));
                    StartCoroutine(ExecuteAfterTime(0.5f));
                    //Debug.Log("hey EXERCUTE HERE3");

                    attackTime = Random.Range(0f, 10f);
                    if (attackTime < 2f) {
                        attackTime = 2.2f;
                    } else if (attackTime < 8f) {
                        attackTime = 5f;
                    } else {
                        attackTime = 3f;
                    }

                    time = 0;
                }
            }
           
		}
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        //Debug.Log("hey EXECUTE HERE1");
        yield return new WaitForSeconds(time);
        GameObject fireball1 = Instantiate(fireBall, attackSpot.transform.position, transform.rotation) as GameObject;
        fireball1.tag = "fireballRedMonster";

        // Debug.Log("attackDamage 2: " + getRedMonsterAttack());

        fireball1.GetComponent<fireBall>().setAttack(getRedMonsterAttack());
        fireball1.GetComponent<fireBall>().setDirection((Knight.transform.position - this.transform.position).normalized, 85f);
    }

    void getLastPosition() {

        if (time2 > 1.2) {
            time2 = 0;
            int layerMask = 1 << 8;

            if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask)) {
                lastPosition = Knight.transform.position;
             //   Debug.Log("Last Position in getLastPosition(): " + lastPosition);
            }
           // Debug.Log("Last position: " + lastPosition);
        }
    }




    void healthCheck()
    {
        // destroy if health below 0
        if (health <= 0)
        {
            
            StartCoroutine(destroy(0.05f));
        }
    }


    IEnumerator destroy(float time)
    {
        notDead = false;
        yield return new WaitForSeconds(time);
        writeDataRef.addStatGiven("red", 0,  true);
        LevelTrigger.GetComponent<LevelTrigger>().setDead(gameObject.tag);
        gameObject.SetActive(false);
    }


  

    public bool setHealth(float healthAdjust)
    {
        if (canBeAttacked) {
          
            attackTime = 2f;
            StartCoroutine(canBeAttackedTimer(0.4f));
            this.health = health + healthAdjust;
            float hp = (health / healthMax) * 100f;

            if (healthUI != null)
            {
                if (hp <= 0f)
                {
                    healthUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                }
                else if (hp < 20f)
                {
                    healthUI.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                }
                else if (hp < 40f)
                {
                    healthUI.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                }
                else if (hp < 60f)
                {
                    healthUI.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    healthUI.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                }
                else if (hp < 80f)
                {
                    healthUI.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                }
            }
            return true;
        }
        return false;
    }

    IEnumerator canBeAttackedTimer(float time)
    {
        canBeAttacked = false;
        yield return new WaitForSeconds(time);
        canBeAttacked = true;
    }


    public void setDirection(Vector3 inDirection, float boost)
    {
        if (canRedirect)
        {
            StartCoroutine(canRedirectTimer(0.3f));
            direction = inDirection;
          //  Debug.Log(gameObject + " direction before normalize: " + direction);

            //Normalize directional vector
            direction.Normalize();
           // Debug.Log(gameObject + " direction after normalize: " + direction);

            //Normalize if both values below 1 or -1; 
            if (direction.x != 0 && direction.z != 0)
            {
                float max = direction.x;
                float maxAbs = Mathf.Abs(direction.x);

                float newValueX = 0;
                float newValueY = 0;

                if (Mathf.Abs(direction.z) > maxAbs)
                {
                    max = direction.z;
                    maxAbs = Mathf.Abs(direction.z);

                    if (max >= 0)
                    {
                        newValueY = 1;
                    }
                    else
                    {
                        newValueY = -1;
                    }

                    newValueX = ((1 / maxAbs) * direction.x);
                    direction = new Vector3(newValueX, -0.4f, newValueY);
                }
                else
                {
                    if (max >= 0)
                    {
                        newValueX = 1;
                    }
                    else
                    {
                        newValueX = -1;
                    }

                    newValueY = ((1 / maxAbs) * direction.z);
                    direction = new Vector3(newValueX, -0.4f, newValueY);
                }
               

            }
            //add force in the direction the ball bounces or starts
            this.GetComponent<Rigidbody>().AddForce(direction * speedforce * boost);
        }
    }

    IEnumerator canRedirectTimer(float time)
    {
        canRedirect = false;
        yield return new WaitForSeconds(time);
        canRedirect = true;
    }

    void spriteRedirect()
    {

 

        Vector2 vector = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z).normalized;

        final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector.x, -1 * vector.y));


        spriteTransform.rotation = Quaternion.Euler(new Vector3(90, 0, -final));

    }

    void totalTime()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;
		time3 += Time.deltaTime;

		if (time3 > 0.75 && time3 < 1.25) {
			time3INT = 1;
		} else {
			time3INT = 0;
		}
		if (timer > 2.4) {
			timer = 0;
		}
    }



    void OnCollisionEnter(Collision col)
    {

        rigidBody.velocity = Vector3.zero;

        Vector3 CollisionNormal = col.contacts[0].normal;
        
        direction = Vector3.Reflect(direction, CollisionNormal);

        setDirection(direction, 20f);

        if (col.transform.tag == "redMonster")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3((transform.position.x - col.transform.position.x), transform.position.y, (transform.position.z - col.transform.position.z)) * (speedforce * 50f));
            
        }

    }


    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            freak = false;
        }

    }

    void OnCollisionStay(Collision col)
    {


        if (time3INT % 1 == 0)
        {

            if (col.gameObject.tag == "wall" || col.gameObject.tag == "squareWall")
            {

                int layerMask = 1 << 8;
                if (Physics.Linecast(transform.position, Knight.transform.position, ~layerMask))
                {

                    direction = directionTo(new Vector3(Random.Range(-1f, 1f), gameObject.transform.position.y, Random.Range(-1f, 1f)), new Vector3());
                    canRedirect = true;

                   

                    setDirection(direction, 10f);
                }
               

            }



            anim.SetBool("moving", true);
            if (col.gameObject.tag == "Player")
            { 
                if (inRunTime < Time.time)
                {
                    inRunTime = Time.time + 0.1f;
                    rando = new Vector3(Random.Range(-1, 1), transform.position.y, Random.Range(-1, 1));
                    freak = true; 
                }

                if (freak)
                {
               
                    col.gameObject.GetComponent<Rigidbody>().AddForce(directionTo(new Vector3(), -rando) *  140.5f);
                    GetComponent<Rigidbody>().AddForce(directionTo(new Vector3(), rando) * speedforce * 90.5f);
                    
                }
            }
        }

    }



}