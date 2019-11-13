using UnityEngine;
using System.Collections;

public class KnightAttack : MonoBehaviour {

    WriteData writeDataRef;

    // AI ELEMENTS AT TOP
    float AiAttackPowerMultiplier;    // 1 is default, acts as multiplier

    GameObject AIController;
    public GameObject enemyHit;
    public GameObject grandParent;
    bool canAttack;
    float nextAttack;
    public health attackPower;


    float attackPerk;
    float attackPerkShow;
    float realAttackPerk;

    float time;
    bool canAps;

    float knightAttack;

    public AudioClip[] audioClip;
    private AudioSource audioa;

    // Use this for initialization
    void Start () {

        writeDataRef = FindObjectOfType<WriteData>();

        audioa = GetComponent<AudioSource>();
        knightAttack = 5f;

        attackPerk = 2f;
        attackPerkShow = 100f;
        realAttackPerk = 1f;
        AIController = GameObject.Find("AI");

        AiAttackPowerMultiplier = AIController.GetComponent<ArtificialIntelligence>().getAttack();


        if (AiAttackPowerMultiplier == 0)
        {
            AiAttackPowerMultiplier = 1;
        }

        nextAttack = 0;
        canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
        totalTime();


        if (Input.GetButtonDown("rightBumper")){

            if (attackPerkShow > 25f) {
                aps(2.5f);
                grandParent.transform.GetComponentInParent<Rigidbody>().AddForce(grandParent.transform.GetComponentInParent<Rigidbody>().velocity * 350f);
            }
            

        }

        if ((Input.GetAxis("RSH") > 0.25f || Input.GetAxis("RSH") < -0.25f || Input.GetAxis("RSV") > 0.25f || Input.GetAxis("RSV") < -0.25f) && time >= nextAttack)
        {

           StartCoroutine(soundOne(4));
            aps(0.1f);
        
            Vector2 vec = new Vector2(-Input.GetAxis("RSH"), Input.GetAxis("RSV"));
            float final = Mathf.Rad2Deg * (Mathf.Atan2(vec.x, vec.y));
            grandParent.transform.rotation = Quaternion.Euler(new Vector3(90, final+180, 0));
        }

    }



    void FixedUpdate()
    {
        attackPowerFunc();
    
    }


    void PlaySound(int clip)
    {
      
        audioa.clip = audioClip[clip];
        //audioa.Stop();
        audioa.Play();
    }

    void attackPowerFunc()
    {


        attackPerk += 0.09f;
        attackPerkShow = attackPerk * 10f;


        realAttackPerk = Mathf.Pow(1.205f, attackPerk);

        if (attackPerk > 10)
        {
            attackPerk = 10f;
        }
        if (attackPerkShow > 100)
        {
            attackPerkShow = 100;
        }

        if (attackPerkShow < 10)
        {
            attackPerkShow = 10;
        }
        attackPower.setHealth(attackPerkShow);
    }


    float aps(float inAps)
    {
        float attackP = realAttackPerk;
        attackPerk -= inAps;
        if (attackPerk < 0)
        {
            attackPerk = 0;
        }
        canAps = true;
        if (attackP < 2.5)
        {
            attackP = 2.5f;
        }
        return attackP;
    }



    IEnumerator soundOne(int one)
    {
        
        if (!(audioa.isPlaying && audioa.clip == audioClip[one]))
        {
            PlaySound(one);
        }
      
        yield return null;
      

    }

    IEnumerator soundTime(int one, float time)
    {
        
        yield return new WaitForSeconds(time);
        audioa.PlayOneShot(audioClip[one]);

    }




    void OnTriggerStay(Collider col) {

       

        if (((Input.GetAxis("RSH") > 0.25f || Input.GetAxis("RSH") < -0.25f || Input.GetAxis("RSV") > 0.25f || Input.GetAxis("RSV") < -0.25f) && canAttack) ) {
            canAps = false;

          
         
                float ap;
                if (col.gameObject.tag == "spider")
                {

                    //Debug.Log("Attack");

                    ap = knightAttack * AiAttackPowerMultiplier * aps(1.1f);
                    if (col.gameObject.GetComponent<Spider>().setHealth(-ap)) {
                    StartCoroutine(soundTime(5, 0.1f));
                    PlaySound(1);
                    writeDataRef.addStatGiven("spider", (-ap), false);
                        }
              
                    col.gameObject.GetComponent<Spider>().setDirection(new Vector3 (Input.GetAxis("RSH"), 0f, -Input.GetAxis("RSV")), 120f);


                        if (time >= nextAttack)
                        {
                            Instantiate(enemyHit, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), transform.rotation);
                            nextAttack = time + 0.5f;
                        }
                    }

                    else if (col.gameObject.tag == "skeleton")
                    {

                    ap = knightAttack * AiAttackPowerMultiplier * aps(1.1f);
                    col.gameObject.GetComponent<Skeleton>().setAttackSpeed(1.3f);

               
                        if (col.gameObject.GetComponent<Skeleton>().setHealth(-ap))
                    {
                    StartCoroutine(soundTime(5, 0.001f));
                    writeDataRef.addStatGiven("skeleton", (-ap), false);
                        }
    
                        col.gameObject.GetComponent<Skeleton>().skellyHit();
                        col.gameObject.GetComponent<Skeleton>().newDirectionTime(0.6f);

                        if (time >= nextAttack)
                        {
                   

                            col.gameObject.GetComponent<Skeleton>().setDirection(new Vector3(Input.GetAxis("RSH"), 0f, -Input.GetAxis("RSV")), 200f);
                            grandParent.GetComponentInParent<Rigidbody>().AddForce(new Vector3(-Input.GetAxis("RSH"), 0f, Input.GetAxis("RSV")) * 1200f);
                            Instantiate(enemyHit, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), transform.rotation);
                            nextAttack = time + 0.5f;
                        }
                }

                else if (col.gameObject.tag == "redMonster")
                {

                ap = knightAttack * AiAttackPowerMultiplier * aps(1.1f);
                if (col.gameObject.GetComponent<redMonster>().setHealth(-ap))
                    {
                    StartCoroutine(soundTime(3, 0.001f));


                    writeDataRef.addStatGiven("red", (-ap), false);
                    }

                    col.gameObject.GetComponent<redMonster>().setDirection(new Vector3(Input.GetAxis("RSH"), 0f, -Input.GetAxis("RSV")), 280f);

                    if (time >= nextAttack)
                    {
                        Instantiate(enemyHit, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), transform.rotation);
                        grandParent.GetComponentInParent<Rigidbody>().AddForce(new Vector3(-Input.GetAxis("RSH"), 0f, Input.GetAxis("RSV")) * 1700f);
                        nextAttack = time + 0.5f;
                    }
                }
                else if (col.gameObject.tag == "greenHornsMonster")
                {

                   

                    ap = knightAttack * AiAttackPowerMultiplier * aps(1.1f);
                    if (col.gameObject.GetComponent<greenHornsMonster>().setHealth(-ap)) {
                    StartCoroutine(soundTime(3, 0.001f));


                    writeDataRef.addStatGiven("green", (-ap), false);
                    }

                    col.gameObject.GetComponent<greenHornsMonster>().setDirection(new Vector3(Input.GetAxis("RSH"), 0f, -Input.GetAxis("RSV")), 130f);

                    if (time >= nextAttack)
                    {
                        Instantiate(enemyHit, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), transform.rotation);
                        nextAttack = time + 0.5f;
                    }
                }
                else if (col.gameObject.tag == "cube")
                {
                aps(1.1f);
             

                if (time >= nextAttack)
                    {
                    StartCoroutine(soundTime(2, 0.1f));
                        Instantiate(enemyHit, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), transform.rotation);
                        nextAttack = time + 0.5f;
                        col.gameObject.GetComponent<box>().breakThis();
                    }
                }
        }

    }

    void totalTime()
    {
        time += Time.deltaTime;
        
    }

}
