using UnityEngine;
using System.Collections;

public class greenHornsMonster : MonoBehaviour
{

    StatsManager statsManagerRef;
    WriteData writeDataRef;

    // Game Objects
    public GameObject fireBall;
    Transform spriteTransform;
    public Rigidbody rigidBody;
    public GameObject attackSpot;


    // Movement Variables
    float speedforce;
    Vector3 direction;
    int timer;
    float final;


    public float attackSpeed;
    // Skeleton Attributes
    public float health;
    public float speed;
    public GameObject Knight;
    float time;
    float time2;
	float timer3;
	int time3INT;
    bool canMove;
    bool notAroundCornerYet;
    Vector3 lastPosition;
    bool canBeAttacked;
    bool canRedirect;

    float attackTime;
    float attackDamage;
    Animator anim;

    GameObject LevelTrigger;

    [SerializeField]
    GameObject healthUI;

    bool notDead;

    float healthMax;

    public void setLevel(GameObject LT)
    {
        LevelTrigger = LT;
    }

    void Start()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();
        writeDataRef = FindObjectOfType<WriteData>();
        notDead = true;
        Knight = GameObject.Find("Knight");

        health = statsManagerRef.getGreenMonsterBaseHealth() + (75f * GameMaster.gameMaster.greenMonsterHealthMultiplier);
        healthMax = health;
        speedforce = statsManagerRef.getGreenMonsterBaseSpeed() + (2f * GameMaster.gameMaster.greenMonsterSpeedMultiplier);
        attackDamage = statsManagerRef.getGreenMonsterBaseAttackPower() + (12f * GameMaster.gameMaster.greenMonsterAttackPowerMultiplier);
        statsManagerRef.setGreenMonsterFinalAttack(attackDamage);
        canRedirect = true;
        canBeAttacked = true;
        notAroundCornerYet = true;
        canMove = true;
        time = 0;
        attackTime = 3f;
        anim = GetComponentInChildren<Animator>();
        timer = 0;
        spriteTransform = this.gameObject.transform.GetChild(0);
    }



    void Update()
    {
        if (notDead)
        {
            healthCheck();
            movement();
            attack();
            totalTime();
            getLastPosition();
        }

      //  Debug.Log("child: " + transform.GetChild(0).transform.rotation);
    }


    Vector3 directionTo(Vector3 first, Vector3 second)
    {
        Vector3 newVector;
        newVector = first - second;

        newVector = new Vector3(newVector.x, -0.24f, newVector.z);
        return newVector;
    }


    public float getGreenMonsterAttack()
    {
        return attackDamage;
    }

    void movement()
    {

		if (canMove) {
			if ((time3INT) % 1 == 0) {
				int layerMask = 1 << 8;
				if (!Physics.Linecast (transform.position, Knight.transform.position, ~layerMask)) {
					Vector3 thisDir = directionTo (Knight.transform.position, this.transform.position);

					float final = Mathf.Rad2Deg * (Mathf.Atan2 (thisDir.x, thisDir.z));
					transform.GetChild (0).transform.rotation = Quaternion.Euler (new Vector3 (90, final + 180, 0));

					//keep distance from player if player gets too close
					if (Vector3.Distance (this.transform.position, Knight.transform.position) <= 1.6f) {
						anim.SetBool ("moving", true);
						GetComponent<Rigidbody> ().AddForce (directionTo (this.transform.position, Knight.transform.position) * speedforce);

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
            else if (notAroundCornerYet && Vector3.Distance (this.transform.position, Knight.transform.position) >= 6.2f && Vector3.Distance (this.transform.position, Knight.transform.position) <= 18.5f) {
					GetComponent<Rigidbody> ().AddForce (directionTo (lastPosition, this.transform.position));
					if (lastPosition == transform.position) {
						notAroundCornerYet = false;
					}
				}
			}
			}
		

    }

    void attack()
    {
        int layerMask = 1 << 8;
        if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask))
        {
            if (time > attackTime && Vector3.Distance(this.transform.position, Knight.transform.position) <= 7.5f)
            {
                anim.SetTrigger("attacking");

                Debug.Log("hey EXERCUTE HERE0");
                // Debug.Log("thisPosition: " + this.transform.position + " Knight Position: " + Knight.transform.position + "Vector3 Normalized: " + (this.transform.position - Knight.transform.position).normalized + " EulerNorm: " + Quaternion.Euler((this.transform.position - Knight.transform.position).normalized));

                StartCoroutine(ExecuteAfterTime(0.3f));

                StartCoroutine(ExecuteAfterTime(0.5f));

                StartCoroutine(ExecuteAfterTime(0.7f));

                StartCoroutine(ExecuteAfterTime(0.9f));

                StartCoroutine(ExecuteAfterTime(0.11f));

                Debug.Log("hey EXERCUTE HERE3");

               
              
                    attackTime = 5f;
               

                time = 0;
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        //Debug.Log("hey EXERCUTE HERE1");
        yield return new WaitForSeconds(time);
        GameObject fireball1 = Instantiate(fireBall, attackSpot.transform.position, transform.rotation) as GameObject;

        Vector3 newDirection = (Knight.transform.position - this.transform.position).normalized;

        fireball1.tag = "fireballGreenMonster";

        //Debug.Log("attackDamage 2: " + getGreenMonsterAttack());

        fireball1.GetComponent<fireBall>().setAttack(getGreenMonsterAttack());
       
        fireball1.GetComponent<fireBall>().setDirection(((newDirection) + new Vector3(Random.Range(-0.8f, 0.8f), newDirection.y, (Random.Range(-0.8f, 0.8f)))).normalized, 35f);
        fireball1.GetComponent<fireBall>().destroyTime = 2.2f;
        yield return null;

    }

    void getLastPosition()
    {

        if (time2 > 1.2)
        {
            time2 = 0;
            int layerMask = 1 << 8;

            if (!Physics.Linecast(transform.position, Knight.transform.position, ~layerMask))
            {
                lastPosition = Knight.transform.position;
            }
           // Debug.Log("Last position: " + lastPosition);
        }
    }





    void healthCheck()
    {
        // destroy if health below 0
        if (health <= 0)
        {
            statsManagerRef.setGreenHornsMonsterDead(1);
            StartCoroutine(destroy(0.1f));
        }
    }


    IEnumerator destroy(float time)
    {
        notDead = false;
        yield return new WaitForSeconds(time);
        writeDataRef.addStatGiven("green",0,  true);
        LevelTrigger.GetComponent<LevelTrigger>().setDead(gameObject.tag);
        gameObject.SetActive(false);
    }

    public bool setHealth(float healthAdjust)
    {
        if (canBeAttacked)
        {
            StartCoroutine(canBeAttackedTimer(0.5f));
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
        yield return null;
    }

    public void setDirection(Vector3 inDirection, float boost)
    {

        if (canRedirect)
        {
            StartCoroutine(canRedirectTimer(0.3f));
            direction = inDirection;
           // Debug.Log(gameObject + " direction before normalize: " + direction);

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
                //Debug.Log(gameObject + " direction after upper: " + direction);

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
        yield return null;
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
		timer3 += Time.deltaTime;

		if (timer3 > 0.75 && timer3 < 1.35) {
			time3INT = 1;
		} else {
			time3INT = 0;
		}
		if (timer3 > 2.9f) {
			timer3 = 0;
		}

    }


    void OnCollisionEnter(Collision col)
    {

        rigidBody.velocity = Vector3.zero;

        Vector3 CollisionNormal = col.contacts[0].normal;

        direction = Vector3.Reflect(direction, CollisionNormal);

        setDirection(direction, 20f);

        if (col.transform.tag == "monsterA")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f)) * (speedforce * 100f));
        }

    }
}