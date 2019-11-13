using UnityEngine;
using System.Collections;

public class SkeletonNew : MonoBehaviour
{

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


	void Awake()
	{
		AIController = GameObject.Find("AI");
		Knight = GameObject.Find("Knight");
	}

	void Start()
	{

		health = 100f; // * AIController.GetComponent<ArtificialIntelligence>().getSkeletonHealth();
		speedforce = 6f;  // * AIController.GetComponent<ArtificialIntelligence>().getSkeletonSpeed();
		attack = 15f; // * AIController.GetComponent<ArtificialIntelligence>().getSkeletonAttack();
		skeletonAttackMultiplier = 1.5f; // AIController.GetComponent<ArtificialIntelligence>().getSkeletonAttackSpeed();
		skeletonAttackRange = 10f; // AIController.GetComponent<ArtificialIntelligence>().getSkeletonAttackRange();

		child = transform.GetChild (0).gameObject;


	}



	void Update()
	{
	

		timer2 += Time.deltaTime;

	}

    Vector3 directionTo(Vector3 first, Vector3 second)
    {

        Vector3 newVector;
        newVector = first - second;

        newVector = new Vector3(newVector.x, -0.24f, newVector.z);
        return newVector;
    }


    void spriteRedirect()  {
        
                Vector2 vector = new Vector2(rigidBody.velocity.x, rigidBody.velocity.z).normalized;

                final = Mathf.Rad2Deg * (Mathf.Atan2(-1 * vector.x, -1 * vector.y));

                spriteTransform.rotation = Quaternion.Euler(new Vector3(90, 0, -final));
              
    }



    public void setDirection(Vector3 inDirection, float boost)
    {

        if (gameObject != null)
        {

            if (canRedirect)
            {
                spriteRedirect();

                direction = inDirection;
                direction.Normalize();

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
                rigidBody.AddForce(direction * speedforce * boost * speedMultiplier);
            }
        }
    }





}