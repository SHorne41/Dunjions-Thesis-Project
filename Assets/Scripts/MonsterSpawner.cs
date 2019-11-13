using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{

    public GameObject MonsterType;

    // Spawner Attributes
    public float spawnFrequency;
    public int spawnLimit;
    public float monsterHealthFactor;
    public float nextAngle;
    int spawnCount;
    public GameObject knight;
    GameObject floor;

	float timer3;
	int time3INT;


    bool isTriggered = false;

    [SerializeField]
    bool useCollisionTrigger = false;

    [SerializeField]
    bool useAlternateTrigger = false;

    [SerializeField]
    bool moving = false;

    bool isSecondaryTriggered;
    bool isAlternateTriggered;
    bool spawnFirst;
    bool spawnFirst2;

    GameObject level; 
	GameObject[] ObjectPool;

    // A factor of the defafult monster health, 1 being normal,
    //2 being double, 0.5 being half etc.    

    public float monsterSpeedFactor;

    float time;
    // Use this for initialization

    void Awake()
    {

      
        floor = GameObject.Find("Floor");
		spawnFirst2 = true;


		if (useCollisionTrigger)
		{
			isSecondaryTriggered = false;
		}
		else
		{
			isSecondaryTriggered = true;
		}

		if (useAlternateTrigger)
		{
			isAlternateTriggered = false;
		}
		else
		{
			isAlternateTriggered = true;
		}

		spawnCount = 0;
		if (spawnFrequency == 0) { spawnFrequency = 60; }
		if (spawnFrequency < 0) { spawnFrequency = 0; }

		ObjectPool = new GameObject[spawnLimit];
		for (int i = 0; i < spawnLimit; i++){
			GameObject newestMonster = Instantiate(MonsterType, new Vector3(transform.position.x, -0.4f, transform.position.z), transform.rotation) as GameObject;
			if (newestMonster.tag == "redMonster") {
                //newestMonster.GetComponent<redMonster>().Knight = knight;
                //newestMonster.GetComponent<redMonster>().setLevel(level);
                newestMonster.transform.position = new Vector3(transform.position.x, floor.transform.position.y + 1f, transform.position.z);
               
            }
			else if (newestMonster.tag == "greenHornsMonster")
			{
				// newestMonster.GetComponent<greenHornsMonster>().Knight = knight;
				//newestMonster.GetComponent<greenHornsMonster>().setLevel(level);
				newestMonster.transform.position = new Vector3(transform.position.x, floor.transform.position.y + 1.7f, transform.position.z);
			}
			else  if (newestMonster.tag == "skeleton")
			{
				//newestMonster.GetComponent<Skeleton>().Knight = knight;
				//newestMonster.GetComponent<Skeleton>().setLevel(level);

				newestMonster.transform.position = new Vector3(transform.position.x, floor.transform.position.y + 1.2f, transform.position.z);
			}
			else  if (newestMonster.tag == "spider")
			{
				//  newestMonster.GetComponent<Spider>().Knight = knight;
				//newestMonster.GetComponent<Spider>().setLevel(level);
				newestMonster.transform.position = new Vector3(transform.position.x, floor.transform.position.y + 1f, transform.position.z);
			}

			ObjectPool[i] = newestMonster;
			ObjectPool[i].SetActive (false);
		}
		gameObject.SetActive (false); 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 


        timeToTrigger();
        spawnMonster();

		timer3 += Time.deltaTime;

    }

 

  
    public void trigger()
    {
        isTriggered = true;
        spawnFirstFunc();
    }

    public void triggerAlternate()
    {
        isAlternateTriggered = true;
    }


    void spawnFirstFunc()
    {

        if (spawnLimit > 0 && !useAlternateTrigger && spawnFirst2)
        {
            spawnFirst2 = false;
            spawnFirst = true;
        }
        else
        {
            spawnFirst2 = false;
            spawnFirst = false;
        }
    }



    public void setLevel(GameObject levelIn)
    {
        level = levelIn;
    }



    void spawnMonster() {


		if (time > spawnFrequency && spawnFrequency >= 0 && spawnCount < spawnLimit && (isTriggered && isSecondaryTriggered && isAlternateTriggered) || spawnFirst)
        {
            spawnFirst = false;
            
		   
		
			ObjectPool [spawnCount].gameObject.SetActive (true);
            time = 0;
            

			if (ObjectPool[spawnCount].tag == "redMonster") {
			
				ObjectPool[spawnCount].GetComponent<redMonster>().setLevel(level);
                ObjectPool[spawnCount].gameObject.GetComponent<redMonster>().startMoving(moving);
            }
			else if (ObjectPool[spawnCount].tag == "greenHornsMonster")
			{
			
				ObjectPool[spawnCount].GetComponent<greenHornsMonster>().setLevel(level);
			
			}
			else  if (ObjectPool[spawnCount].tag == "skeleton")
			{
				
				ObjectPool[spawnCount].GetComponent<Skeleton>().setLevel(level);
		
			}
			else  if (ObjectPool[spawnCount].tag == "spider")
			{
				ObjectPool[spawnCount].GetComponent<Spider>().setLevel(level);
			
			}
		spawnCount++;
        }

        if (spawnCount >= spawnLimit )
        {
			gameObject.SetActive(false);
        }     

    }

    void timeToTrigger()
    {
		
        time += Time.deltaTime;
      
    }



}
