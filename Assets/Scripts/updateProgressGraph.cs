using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class updateProgressGraph : MonoBehaviour {

    StatsManager statsManagerRef;

    public Bar barPrefab;

    public Bar[] bars;

    public float[] progressVals;
    public int numBars;
    public int livesThisCheckpoint;
    bool overrideVals;

    float chartHeight;

    bool deadKnight;

    void Start()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();

        overrideVals = false;

        livesThisCheckpoint = GameMaster.gameMaster.livesThisCheckpoint;
        numBars = GameMaster.gameMaster.numBars;
        chartHeight = GetComponent<RectTransform>().sizeDelta.y; //This line allows us to determine the height of the chart in Pixels relative to the size of the screen

        for (int i = 0; i < 5; i++)
        {
            progressVals[i] = GameMaster.gameMaster.inputVals[i];
        }

        if (numBars !=0) { createBars(); }

        deadKnight = false;

        displayBarsOnGraph(progressVals);
    }

    void Update()
    {
        
        //If the knight has died, we must save all the values to our static class to preserve them when the new scene loads
        if (!statsManagerRef.getKnightIsAlive()) { saveValuesToGameMaster(); }
    }

    public void updateBars()
    {
        if (!statsManagerRef.getKnightIsAlive() && !deadKnight)
        {
            if (numBars < 5)        //Can we store the value in the next position of the array?
            {
                progressVals[numBars] = (statsManagerRef.getKnightProgressCurrent() * 100);
                numBars++;
            }
            else { overrideVals = true; }       //If not, we're going to have to override some values

            deadKnight = true;
            livesThisCheckpoint++;
        }
    }

    public void saveValuesToGameMaster()
    {
        if (!overrideVals)        //If the knight has yet to have 5 attempts on this checkpoint
        {
            for (int i = 0; i < numBars; i++)
            {
                //Store all the vals in GameMaster
                GameMaster.gameMaster.inputVals[i] = progressVals[i];
            }
        }
        //If the knight has attempted this checkpoint more than 5 times
        else
        {
            for (int i = 0; i < numBars - 1; i++)
            {
                //Storing vals in GameMaster, while also shifting them to the left by 1.
                //This overrides the value stored in progressVals [0], which is no longer need in the graph
                GameMaster.gameMaster.inputVals[i] = progressVals[i + 1];
            }
            //And adding the newest val to the end of the array
            GameMaster.gameMaster.inputVals[4] = (statsManagerRef.getKnightProgressCurrent() * 100);
        }
        
        GameMaster.gameMaster.numBars = numBars;
        GameMaster.gameMaster.livesThisCheckpoint = livesThisCheckpoint;
    }

    //This method is called from the levelTrigger script when the knight makes it to a checkpoint
    public void deleteBars()
    {
        for (int i = 0; i < numBars; i++)
        {
            //The three lines below make the bars invisible until the next spawn
            bars[i].bar.color = Color.clear;
            bars[i].barValue.text = "";
            bars[i].monsterName.text = "";
            //These lines actually delete the bars
            Destroy(bars[i]);
            progressVals[i] = 0;
        }
        numBars = 0;
        livesThisCheckpoint = 0;
        GameMaster.gameMaster.numBars = numBars;
        GameMaster.gameMaster.livesThisCheckpoint = livesThisCheckpoint;
    }


    public void createBars ()
    {
        for (int i = 0; i < numBars; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            bars[i] = newBar;
            bars[i].transform.SetParent(transform);
        }
    }

    public void displayBarsOnGraph (float[] vals)
    {
        for (int i = 0; i < numBars; i++)
        {
            RectTransform rt = bars[i].bar.GetComponent<RectTransform>();
            float normalizedValue = ((float)vals [i] / 100) * 0.95f;

            rt.sizeDelta = new Vector2(rt.sizeDelta.x, chartHeight * normalizedValue);
            //If the height of the bar is too small, move the label ABOVE the bar
            if (rt.sizeDelta.y < 30f)
            {
                bars[i].barValue.rectTransform.pivot = new Vector2(0.5f, 0);
                bars[i].barValue.rectTransform.anchoredPosition = Vector2.zero;
            }
            bars[i].barValue.text = "" + progressVals[i];
            if (i != 0)
            {
                if (progressVals [i] < progressVals [i-1])
                {
                    bars[i].bar.color = Color.red;
                }
                else if (progressVals[i] > progressVals[i - 1])
                {
                    bars[i].bar.color = Color.green;
                }
                else
                {
                    bars[i].bar.color = Color.yellow;
                }
            }
            else
            {
                bars[i].bar.color = Color.white;
            }
        }
        //Adding the label to the bars
        if (livesThisCheckpoint > 5)        //If the knight has attempted this checkpoint more than 5 times
        {
            for (int i = numBars; i > 0; i--)
            {
                //Math required to display values > 5
                bars[numBars - i].monsterName.text = "Life #" + (livesThisCheckpoint - i);
            }
        }
        else if (livesThisCheckpoint <= 5)  //If the knight has attempted this checkpoint 5 times or less
        {
            for (int i = 0; i < numBars; i ++)
            {
                bars[i].monsterName.text = "Life #" + (i+1);
            }
        }
        
    }

}
