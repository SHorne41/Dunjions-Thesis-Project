using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarChart : MonoBehaviour
{

    public Bar barPrefab;
    Bar[] theBars;

    StatsManager statsManagerRef;

    float[] inputValues;

    Color[] colours;

    float chartHeight;

    bool valuesHaveChanged;

    void Start()
    {
        valuesHaveChanged = false;

        statsManagerRef = FindObjectOfType<StatsManager>();

        theBars = new Bar[4];
        inputValues = new float[4] { 0, 0, 0, 0 };
        colours = new Color[2] { Color.red, Color.cyan };
        chartHeight = GetComponent<RectTransform>().sizeDelta.y - 0.05f; //This line allows us to determine the height of the chart in Pixels relative to the size of the screen
        displayGraph(inputValues);

    }

    void displayGraph(float[] vals)
    {
        float maxValue = vals.Max();     //This line gives the highest value in the array. Need System.Linq for this to work
        for (int i = 0; i < vals.Length; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            theBars[i] = newBar;
            theBars[i].transform.SetParent(transform);
            RectTransform rt = theBars[i].bar.GetComponent<RectTransform>();
            float normalizedValue = ((float)vals[i] / 100) * 0.95f;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (chartHeight * normalizedValue));      //This line of code adjusts the height of the bar
            if (vals[i] == maxValue)
            {
                theBars[i].bar.color = colours[0];
            }
            else
            {
                theBars[i].bar.color = colours[1];
            }
            //If the height of the bar is too small, move the label ABOVE the bar
            if (rt.sizeDelta.y < 30f)
            {
                theBars[i].barValue.rectTransform.pivot = new Vector2(0.5f, 0);
                theBars[i].barValue.rectTransform.anchoredPosition = Vector2.zero;
            }
            theBars[i].barValue.text = "" + inputValues[i];
        }

    }

    void Update()
    {
        checkDamage();
        if (valuesHaveChanged) { updateGraph(inputValues); }
    }

    void checkDamage()
    {
        if (inputValues[0] != statsManagerRef.getDamageDealtSkeletonCL())
        {
            inputValues[0] = statsManagerRef.getDamageDealtSkeletonCL();
            valuesHaveChanged = true;
        }
        if (inputValues[1] != statsManagerRef.getDamageDealtSpiderCL())
        {
            inputValues[1] = statsManagerRef.getDamageDealtSpiderCL();
            valuesHaveChanged = true;
        }
        if (inputValues[2] != statsManagerRef.getDamageDealtRedMonsterCL())
        {
            inputValues[2] = statsManagerRef.getDamageDealtRedMonsterCL();
            valuesHaveChanged = true;
        }
        if (inputValues[3] != statsManagerRef.getDamageDealtGreenMonsterCL())
        {
            inputValues[3] = statsManagerRef.getDamageDealtGreenMonsterCL();
            valuesHaveChanged = true;
        }


    }

    void updateGraph(float[] vals)
    {
        float maxValue = vals.Max();     //This line gives the highest value in the array. Need System.Linq for this to work
        for (int i = 0; i < vals.Length; i++)
        {
            RectTransform rt = theBars[i].bar.GetComponent<RectTransform>();
            float normalizedValue = ((float)vals[i] / Mathf.Max(100, maxValue)) * 0.95f;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (chartHeight * normalizedValue));
            //If the height of the bar is too small, move the label ABOVE the bar
            if (rt.sizeDelta.y > 30f)
            {
                theBars[i].barValue.rectTransform.pivot = new Vector2(0.5f, 0.75f);
                theBars[i].barValue.rectTransform.anchoredPosition = Vector2.zero;
            }
            //Assigning colours to the different bars
            if (vals[i] == maxValue)
            {
                theBars[i].bar.color = colours[0];
            }
            else
            {
                theBars[i].bar.color = colours[1];
            }
            theBars[i].barValue.text = "" + inputValues[i];
        }
        valuesHaveChanged = false;
    }

    
}