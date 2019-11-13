using UnityEngine;
using System.Collections;

public class fireBall: MonoBehaviour
{
    StatsManager statsManagerRef;

    // AI TWEAKABLES
    public float speedforce;      // AI TWEAKABLE
    public float startForce;      // AI TWEAKABLE
    public float destroyTime;     // AI TWEAKABLE


    // Game Objects
    Transform spriteTransform;
    public Rigidbody rigidBody;
    GameObject Knight;
    GameObject AI;

    // Movement Variables
  
    Vector3 direction;
    int timer;
    float final;
    bool live;

    // Fireball Attributes
    public float health;
    public float speed;
    Animator anim;

    float attack;

    Rigidbody rb;

    public AudioClip[] audioClip;
    private AudioSource audioa;


    void Start()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();
        audioa = GetComponent<AudioSource>();
        AI = GameObject.Find("AI");

        rb = this.GetComponent<Rigidbody>();
        if (destroyTime == 0)
        {
            destroyTime = 4f;
        }

        if (gameObject.tag == "fireballRedMonster")
        {
            speedforce = 25f + (45 * statsManagerRef.getRedMonsterFireballSpeed());
            startForce = 30 + (60f * statsManagerRef.getRedMonsterFireballSpeed());
            destroyTime = destroyTime / statsManagerRef.getRedMonsterFireballSpeed();
        }
        else
        {
            speedforce = 15f + (35 * statsManagerRef.getGreenMonsterFireballSpeed());
            startForce = 20 + (55f * statsManagerRef.getGreenMonsterFireballSpeed());
            destroyTime = destroyTime / statsManagerRef.getGreenMonsterFireballSpeed();
        }

        if (destroyTime == 0)
        {
            destroyTime = 4f;
        }

        Knight = GameObject.FindGameObjectWithTag("Player");
        live = true;
        
        anim = GetComponentInChildren<Animator>();
        

        timer = 0;
        spriteTransform = this.gameObject.transform.GetChild(0);


        StartCoroutine(ExecuteAfterTime(destroyTime));


    }



    void Update() {

        movement();
    }


    void PlaySound(int clip)
    {
        audioa.clip = audioClip[clip];
        audioa.Play();
    }



    public void setAttack(float attackArg)
    {
        attack = attackArg;
    }

    public float getAttack()
    {
        return attack;
    }

    void movement()
    {
        
        if (live)
        {
            spriteRedirect();
            if (this.GetComponent<Rigidbody>().velocity.magnitude < speedforce * 0.65)
            {
                this.GetComponent<Rigidbody>().AddForce(direction * speedforce);

            }

            if (this.GetComponent<Rigidbody>().velocity.magnitude == 0)
            {

                if (timer > 100)
                {
                   
                    Vector2 vector = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z).normalized;

                    final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector.x, -1 * vector.y));
                    spriteTransform.rotation = Quaternion.Euler(new Vector3(90, 0, -final));


                    timer = 0;
                }
            }
            direction = (Knight.transform.position - this.transform.position);
            
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        live = false;
        anim.SetTrigger("explode");
        Destroy(this.gameObject, 1f);
        yield return null;
    }

    IEnumerator stopFire(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
        yield return null;
    }


    public void setDirection(Vector3 inDirection, float boost)
    {
       // Debug.Log(getAttack());
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
                direction = new Vector3(newValueX, 2.5f, newValueY);
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
                direction = new Vector3(newValueX, 2.5f, newValueY);
            }
           // Debug.Log(gameObject + " direction after upper: " + direction);

        }
        //add force in the direction the ball bounces or starts
        this.GetComponent<Rigidbody>().AddForce(direction * speedforce * boost);

    }

    void spriteRedirect()
    {

        Vector2 vector = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z).normalized;

        final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector.x, -1 * vector.y));

        spriteTransform.rotation = Quaternion.Euler(new Vector3(90, 0, -final));

    }




    void OnCollisionEnter(Collision col)
    {

        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

     //   PlaySound(0);
      

       live = false;
       anim.SetTrigger("explode");

       rb.velocity = rb.velocity * 0.8f;
       StartCoroutine(stopFire(0.15f));
       Destroy(this.gameObject, 1f);

    }
}