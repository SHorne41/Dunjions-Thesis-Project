using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    StatsManager statsManagerRef;
    WriteData writeDataRef;

    // Game Objects
    Transform spriteTransform;
    public Rigidbody rigidBody;

    string monsterType;
    public GameObject Knight;



    public static int id;
    public int realId;

    // Movement Variables
    float newDirectionTimer;
    bool newDirectionBool;

    float speedforce;
    Vector3 direction;
    Vector3 currentDirection;

    int timer;
    float timer2;
	float timer3;
	int time3INT;

    float final;
    Vector2 angle;
    bool notHit; 
    float health;
    float speed;
    float attack;
    float speedMultiplier;
    float skeletonAttackMultiplier;
    float skeletonAttackRange;

    //private
    bool canBeAttacked;
    bool canRedirect;
    bool frantic;
    bool canAttack;

    float nextAttack;

    [SerializeField]
    float attackDuration;

    GameObject AIController;

    GameObject LevelTrigger;

	GameObject child;

    [SerializeField]
    GameObject healthUI;


    bool notDead;
    float healthMax;


    public void setLevel(GameObject LT)
    {
        LevelTrigger = LT;
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
        notDead = true;

		health = statsManagerRef.getSkeletonBaseHealth() + (30f * GameMaster.gameMaster.skeletonHealthMultiplier);
        healthMax = health;
		speedforce = statsManagerRef.getSkeletonBaseSpeed() + (4f * GameMaster.gameMaster.skeletonSpeedMultiplier);
        attack = statsManagerRef.getSkeletonBaseAttackPower() + (10f * GameMaster.gameMaster.skeletonAttackPowerMultiplier);
        statsManagerRef.setSkeletonFinalAttack(attack);
        skeletonAttackRange = statsManagerRef.getSkeletonBaseAttackRange();

        skeletonAttackMultiplier = 0.4f + ( 0.4f * GameMaster.gameMaster.skeletonAttackSpeedMultiplier * 0.85f);
		


        if (speedforce > 14)
        {
            speedforce = 14f;
        }


		child = transform.GetChild (0).gameObject;
        if (attackDuration == 0)
        {
            attackDuration = Random.Range(4f, 14f);
        }

        if (Random.Range(0f, 4f) > 3f) {
            frantic = true;
        }
        else {
            frantic = false;
        }

        newDirectionBool = true;
        newDirectionTimer = 0f;

        speedMultiplier = 1f;
        rigidBody = GetComponent<Rigidbody>();
        canRedirect = true;
        canBeAttacked = true;
        notHit = true;    
        monsterType = "skeleton";




        timer = 0;
        timer2 = 0;
        spriteTransform = this.gameObject.transform.GetChild(0);
        setDirection(transform.forward, 40f);

        spriteRedirect();

        canAttack = false;
        nextAttack = 3f;
    }



    void Update()
    {

        if (notDead)
        {
            healthCheck();
            movement();
            attackTime();

        }
        timer2 += Time.deltaTime;
		timer3 += Time.deltaTime;

		if (timer3 > 0.75 && timer3 < 1.25) {
			time3INT = 1;
		} else {
			time3INT = 0;

		}

		if (timer3 > 2.5) {
			timer3 = 0;
		}
    }

    public void setId()
	{
		
        realId = id;
        id++;
    }

    public int getId()
	{
		
        return realId;
    }

    public float getSkeletonAttack() {
		
        return attack;
    }


    public string getMonsterType() {
		
        return monsterType;
    }

    Vector3 directionTo(Vector3 first, Vector3 second)
    {
		
        Vector3 newVector;
        newVector = first - second;

        newVector = new Vector3(newVector.x, -0.24f, newVector.z);
        return newVector;
    }




    void attackTime()
    {
		
		if ((time3INT) % 1 == 0) {
			
			if (timer2 >= nextAttack) {
				
				if (timer2 >= nextAttack + attackDuration) {
					
					float rnd = Random.Range (0.2f, 3f);
					if (rnd <= 1f) {
						
						attackDuration = Random.Range (4f, 14f);
						rnd = Random.Range (0.2f, 3f);
					}

					nextAttack = timer2 + rnd;

					canAttack = false;
				} else {
					
					canAttack = true;
				}
			} else if (timer2 <= nextAttack) {
				
				canAttack = false;

			} else {
				
				canAttack = true;
			}
		}
    }


    IEnumerator ExecuteAfterTime(float time)
    {
		
        notHit = false;
        yield return new WaitForSeconds(time);
        notHit = true;
        yield return null;
    }

    public void skellyHit()
    {
        if (notHit)
        {
            StartCoroutine(ExecuteAfterTime(2.2f));
        }
    }


    public void newDirectionTime(float time)
    {
		
        StartCoroutine(newDirectionTime2(time));       
    }

    public IEnumerator newDirectionTime2(float time)
    {
		
        yield return new WaitForSeconds(time);
        newDirection(true);
        yield return null;
    }


    public void newDirection(bool instant) {
		
		if ((time3INT) % 1 == 0) {
			
			if (newDirectionBool || instant) { 
				
				newDirectionBool = false;
				if (frantic) {
					
					newDirectionTimer = Time.time + Random.Range (1f, 5f);
					speedforce = 12f;
					setDirection (new Vector3 (Random.Range (-1f, 1f), 0f, Random.Range (-1f, 1f)), 30f);
                
				} else {
					
					newDirectionTimer = Time.time + Random.Range (2f, 15f);
					setDirection (new Vector3 (Random.Range (-1f, 1f), 0f, Random.Range (-1f, 1f)), 1f);
				}

			}
			if (newDirectionTimer <= Time.time) {
				
				newDirectionBool = true;
			}
		}
    }


    void movement() {

        timer++;
		
			Vector3 thisDir = direction;
			float final = Mathf.Rad2Deg * (Mathf.Atan2 (thisDir.x, thisDir.z));

			spriteRedirect ();

			
			if (Vector3.Distance (this.transform.position, Knight.transform.position) >= 0.01f && Vector3.Distance (this.transform.position, Knight.transform.position) <= 3.8f * skeletonAttackRange && notHit && canAttack) {
                speedMultiplier = 1f * skeletonAttackMultiplier;


                int layerMask = 1 << 8;
                if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask))
                {
                   
                    thisDir = directionTo(Knight.transform.position, this.transform.position);
                    
                }
              
                speedMultiplier = 1f * skeletonAttackMultiplier;

				rigidBody.velocity = rigidBody.velocity * 1.05f;
            
				final = Mathf.Rad2Deg * (Mathf.Atan2 (thisDir.x, thisDir.z));
				child.transform.rotation = Quaternion.Euler (new Vector3 (90, final + 180, 0));

				if (rigidBody.velocity.magnitude < 7f) {
                 
                    rigidBody.AddForce (thisDir * (speedforce * 3f * speedMultiplier));
				}
           
			} else if (rigidBody.velocity.magnitude < speedforce * 0.25) {

                
                rigidBody.AddForce (direction * speedforce * 3f * speedMultiplier);

			} else {
				
				speedMultiplier = 1f;  
			}

			if (rigidBody.velocity.magnitude == 0) {
				
				if (timer > 100) {
					
					Vector2 vector = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.z).normalized;

					final = Mathf.Rad2Deg * (Mathf.Atan2 (-1 * vector.x, -1 * vector.y));
					spriteTransform.rotation = Quaternion.Euler (new Vector3 (90, 0, -final));


					timer = 0;
				}
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
        writeDataRef.addStatGiven("skeleton", 0 , true);
        LevelTrigger.GetComponent<LevelTrigger>().setDead(gameObject.tag);
        gameObject.SetActive(false);
    }

    public bool setHealth(float healthAdjust)
    {
        if (canBeAttacked)
        {
      
            
            StartCoroutine(canBeAttackedTimer(0.3f));
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

    public void setAttackSpeed(float x)
    {
		
        speedMultiplier = x;
    }

    public void setDirection(Vector3 inDirection, float boost)
    {
		
        if (gameObject != null)
        {
			
			if (time3INT % 1 == 0) {
				
				if (canRedirect) {
					
					//spriteRedirect ();
					//StartCoroutine (canRedirectTimer (0.02f));
					direction = inDirection;


					//Normalize directional vector
					direction.Normalize ();
										
					//Normalize if both values below 1 or -1; 
					if (direction.x != 0 && direction.z != 0) {
						
						float max = direction.x;
						float maxAbs = Mathf.Abs (direction.x);

						float newValueX = 0;
						float newValueY = 0;

						if (Mathf.Abs (direction.z) > maxAbs) {
							
							max = direction.z;
							maxAbs = Mathf.Abs (direction.z);

							if (max >= 0) {
								newValueY = 1;
							} else {
								newValueY = -1;
							}

							newValueX = ((1 / maxAbs) * direction.x);
							direction = new Vector3 (newValueX, -0.4f, newValueY);
						} else {
							
							if (max >= 0) {
								
								newValueX = 1;
							} else {
								
								newValueX = -1;
							}

							newValueY = ((1 / maxAbs) * direction.z);
							direction = new Vector3 (newValueX, -0.4f, newValueY);
						}
					

					}
					//add force in the direction the ball bounces or starts
					rigidBody.AddForce (direction * speedforce * boost * speedMultiplier);
				}
			}
        }
    }


    IEnumerator canRedirectTimer(float time)
    {
		
        canRedirect = false;
        yield return new WaitForSeconds(time);
        canRedirect = true;
		
    }

    void spriteRedirect() {
		
		if (time3INT % 1 == 0) {
			
			if (canRedirect) {          
				
           
				Vector2 vector = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.z).normalized;

				final = Mathf.Rad2Deg * (Mathf.Atan2 (-1 * vector.x, -1 * vector.y));

				spriteTransform.rotation = Quaternion.Euler (new Vector3 (90, 0, -final));
			}
		}
    }

    void OnCollisionStay(Collision col)
    {


        if (time3INT % 1 == 0) {

            if (col.gameObject.tag == "wall" || col.gameObject.tag == "squareWall" || col.gameObject.tag == "cube" ) {

                int layerMask = 1 << 8;
                if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask))
                {
                
                    direction = directionTo(Knight.transform.position, this.transform.position);
                    canRedirect = true;
                    spriteRedirect();
      
                    setDirection(direction, 10f);
                }
                else
                {
                   
                    direction = directionTo(new Vector3(Random.Range(-1f, 1f), gameObject.transform.position.y, Random.Range(-1f, 1f)), new Vector3());

                    canRedirect = true;
                    spriteRedirect();

             
                    setDirection(direction, 10f);

                }
            
			}
		}

    }
}