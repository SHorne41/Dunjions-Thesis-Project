using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour
{
    StatsManager statsManagerRef;
    WriteData writeDataRef;

    // Game Objects
    Transform spriteTransform;
    public Rigidbody rigidBody;

    string monsterType;

    public GameObject groundWeb;
    public GameObject Knight;


    // Movement Variables
    float speedforce;
    Vector3 direction;
    int timer;
    float final;


    // Spider Attributes
    public float health;
    public float speed;
    public int maxWebs;
    public float stickiness;
    bool canDropWeb;
    bool canBeAttacked;
    bool canRedirect;
    float scaleStep;
    float dropWebFrequency;


    public float dirNum;
    GameObject AIController;

    GameObject LevelTrigger;


    int webCountMax;
    int webCount;
    int lastCount;
    int lastWebCount;
    float healthMax;
    GameObject[] webs;

    [SerializeField]
    GameObject healthUI;

    bool notDead;

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
        webCountMax = 15;
        webs = new GameObject[webCountMax + 1];
      
        webCount = 0;
        lastWebCount = 3;

        notDead = true;

        canRedirect = true;
        canDropWeb = true;
        canBeAttacked = true;
        stickiness = 5;
        maxWebs = 5;
        monsterType = "spider";
        timer = 0;
        spriteTransform = this.gameObject.transform.GetChild(0);
        setDirection(transform.forward, 20f);

		health = statsManagerRef.getSpiderBaseHealth() + (35f * GameMaster.gameMaster.spiderHealthMultiplier);
        healthMax = health;
		speedforce = GameMaster.gameMaster.spiderSpeedMultiplier * statsManagerRef.getSpiderBaseSpeed();
		dropWebFrequency = 2f;
    }



    void Update()
    {
        if (notDead)
        {
            healthCheck();
            movement();
            webKit();

        }
     


    }

    void webKit()
    { 
        foreach (GameObject G in webs)
        {
            if (G != null)
            {
                if (G.transform.localScale.y < 0.145)
                {
                    G.transform.localScale = Vector3.MoveTowards(G.transform.localScale, new Vector3(5.0f, 0.15f, 5.0f), 12f * Time.deltaTime);
                }
                else if (G.transform.localScale.y > 0.1f && G.transform.localScale.x > 0.12f)
                {
                    G.transform.localScale = Vector3.MoveTowards(G.transform.localScale, new Vector3(0.1f, 0.15f, 0.1f), 1.5f * Time.deltaTime);
                }
                else
                {
                    Destroy(G);
                }
            }
        }
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




    void movement()
    {


        spriteRedirect();

        Vector3 thisDir = directionTo(Knight.transform.position + transform.forward*3, this.transform.position);
        float final = Mathf.Rad2Deg * (Mathf.Atan2(thisDir.x, thisDir.z));

        if (this.GetComponent<Rigidbody>().velocity.magnitude == 0)
        {

            if (timer > 100)
            {

                Vector2 vector2 = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z).normalized;

                final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector2.x, -1 * vector2.y));
                spriteTransform.rotation = Quaternion.Euler(new Vector3(90, 0, -final));


                timer = 0;
            }
        }


        if (this.GetComponent<Rigidbody>().velocity.magnitude < speedforce * 0.65 && !(this.GetComponent<Rigidbody>().velocity.magnitude > speedforce * 0.66))
        {
            this.GetComponent<Rigidbody>().AddForce(direction * speedforce);

        }

        if (Vector3.Distance(this.transform.position, Knight.transform.position) >= 3.2f && Vector3.Distance(this.transform.position, Knight.transform.position) <= 9.8f)
        {
            final = Mathf.Rad2Deg * (Mathf.Atan2(thisDir.x, thisDir.z));
            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90, final + 180, 0));
            GetComponent<Rigidbody>().AddForce(thisDir * (speedforce - 4.1f));
        }


        Vector3 toTarget = (transform.position - Knight.transform.position).normalized;

        if (Vector3.Distance(this.transform.position, Knight.transform.position) >= 2.1f && Vector3.Distance(this.transform.position, Knight.transform.position) <= 7.5f && Vector3.Dot(transform.forward, toTarget) > 0)
        {
            dropWeb();
        }

        Vector2 vector = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z).normalized;
        final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector.x, -1 * vector.y));
        spriteTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, final));
    }




    void healthCheck()
    {
        // destroy if health below 0
        if (health <= 0)
        {

            StartCoroutine(destroy(0.1f));
        }
    }


    IEnumerator destroy(float time)
    {
        notDead = false;
        foreach (GameObject G in webs)
        {
            if (G != null)
            {
                G.SetActive(false);
            }
        }
        yield return new WaitForSeconds(time);
        writeDataRef.addStatGiven("spider", 0, true);
        LevelTrigger.GetComponent<LevelTrigger>().setDead(gameObject.tag);
        gameObject.SetActive(false);
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        canDropWeb = true;
        yield return null;
    }

    void dropWeb()
    {
        if (canDropWeb)
        {
            lastWebCount = webCount;

            canDropWeb = false;

            webCount++;
            if (webCount > webCountMax-1)
            {
                webCount = 0;
            }

            if (webs[webCount] != null)
            {
                Destroy(webs[webCount]);
            }

            if (webs[webCount] == null)
            {
                webs[webCount] = Instantiate(groundWeb, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), transform.rotation);
            }
        
            StartCoroutine(ExecuteAfterTime(Random.Range(Mathf.Abs(dropWebFrequency-1f), dropWebFrequency + 2f)));
        }
    }

   
  
    //These represent the little hearts above the monster's head
    public bool setHealth(float healthAdjust)
    {
        if (canBeAttacked)
        {
      
            StartCoroutine(canBeAttackedTimer(0.05f));
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
            //Debug.Log(gameObject + " direction after normalize: " + direction);

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
              //  Debug.Log(gameObject + " direction after upper: " + direction);

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


        spriteTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, final));

    }




    void OnCollisionEnter(Collision col)
    {

        rigidBody.velocity = Vector3.zero;

        Vector3 CollisionNormal = col.contacts[0].normal;

        direction = Vector3.Reflect(direction, CollisionNormal);

        setDirection(direction, 20f);


    }
}