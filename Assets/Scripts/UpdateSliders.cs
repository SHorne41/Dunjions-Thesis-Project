using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSliders : MonoBehaviour {

    StatsManager statsManagerRef;
    MovingHandles[] handles;
    Stats statsRef;

	// Use this for initialization
	void Start () {
        statsManagerRef = FindObjectOfType<StatsManager>();
        handles = FindObjectsOfType<MovingHandles>();
        statsRef = FindObjectOfType<Stats>();
    }

    public void updateLabels (int handleIndex)
    {
        handles [handleIndex].lowerBound.text = (handles [handleIndex].currentPercentage - 20) + "";
        handles [handleIndex].currentSetting.text = handles [handleIndex].currentPercentage + "";
        handles [handleIndex].upperBound.text = (handles [handleIndex].currentPercentage + 20) + "";
    }


    public void updateHandlePercentages()
    {
        for (int i = 0; i < handles.Length; i++)
        {
            //Which handle is it? Is this the skeleton?
            if (handles[i].monster == MovingHandles.monsters.skeleton)
            {
                //Now that we know it's the skeleton, is this the health slider?
                if (handles[i].sliderType == MovingHandles.allSliderTypes.health)
                {
                    //Now that we know it's the health slider, were there any changes made to it?
                    if (handles[i].checkpointSlider.value >=5)
                    {
                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;

                        
                        GameMaster.gameMaster.updatedStats[0] = 1;
                        
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.speed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;

                       
                        GameMaster.gameMaster.updatedStats[1] = 1;
                        
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackPower)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[2] = 1;
                        
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[3] = 1;
                        updateLabels(i);
                    }
                }
            }
            //Which handle is it? Is this the spider?
            else if (handles[i].monster == MovingHandles.monsters.spider)
            {
                //Now that we know it's the spider, is this the health slider?
                if (handles[i].sliderType == MovingHandles.allSliderTypes.health)
                {
                    //Now that we know it's the health slider, were there any changes made to it?
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[4] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.speed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                       
                        GameMaster.gameMaster.updatedStats[5] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackPower)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[6] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[7] = 1;
                        updateLabels(i);
                    }
                }
            }
            //Which handle is it? Is this the Red Monster?
            else if (handles[i].monster == MovingHandles.monsters.redMonster)
            {
                //Now that we know it's the redMonster, is this the health slider?
                if (handles[i].sliderType == MovingHandles.allSliderTypes.health)
                {
                    //Now that we know it's the health slider, were there any changes made to it?
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[8] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.speed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[9] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackPower)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[10] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[11] = 1;
                        updateLabels(i);
                    }
                }
            }
            //Which handle is it? Is this the Green Monster?
            else if (handles[i].monster == MovingHandles.monsters.greenMonster)
            {
                //Now that we know it's the greenMonster, is this the health slider?
                if (handles[i].sliderType == MovingHandles.allSliderTypes.health)
                {
                    //Now that we know it's the health slider, were there any changes made to it?
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        
                        GameMaster.gameMaster.updatedStats[12] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.speed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        GameMaster.gameMaster.updatedStats[13] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackPower)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        GameMaster.gameMaster.updatedStats[14] = 1;
                        updateLabels(i);
                    }
                }
                else if (handles[i].sliderType == MovingHandles.allSliderTypes.attackSpeed)
                {
                    if (handles[i].checkpointSlider.value >= 5)
                    {

                        handles[i].checkpointSlider.value = 4;
                        handles[i].deathSlider.value = 4;
                        GameMaster.gameMaster.updatedStats[15] = 1;
                        updateLabels(i);
                    }
                }
            }
        }
    }
}
