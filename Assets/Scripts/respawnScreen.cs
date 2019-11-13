using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class respawnScreen : MonoBehaviour {

    public Sprite[] respawnImages;
    public Sprite youWinImage;
    private Image img;

    StatsManager statsManagerRef;

    Knight knightRef;


    private float deathtime = 0;

	// Use this for initialization
	void Start ()
    {
        knightRef = FindObjectOfType<Knight>();
        img = GetComponent<Image>();
        statsManagerRef = FindObjectOfType<StatsManager>();
	}
    private void Update()
    {
            if (deathtime != 0)
            {
            img.color = Color.white;
            img.sprite = respawnImages[Mathf.Clamp(((int)(Time.realtimeSinceStartup - deathtime)), 0, respawnImages.Length - 1)];

            if (Time.realtimeSinceStartup - deathtime >= 5)
                {
                    Time.timeScale = 1f;
                    knightRef.loadLevel();
                }
            }  
            if (statsManagerRef.getGreenHornsMonsterDead() == 1)
        {
            Time.timeScale = 0f;
            img.color = Color.white;
            img.sprite = youWinImage;
        }
    }

    public void determineFadeDir ()
    {
        if (deathtime==0)
        {
            deathtime = Time.realtimeSinceStartup;
        }
        
    }
}
