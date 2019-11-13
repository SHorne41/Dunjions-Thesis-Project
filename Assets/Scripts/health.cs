using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {

    public digits hundreds;
    public digits tens;
    public digits ones;



   public void setHealth(float health)
    {
        int hundredsInt;
        int tensInt;
        int onesInt;

        if (health < 0)
        {
            health = 0;
        }

        

        health = Mathf.Ceil(health);

        // 100s
        if (health < 100)
        {
            hundredsInt = 0;
            hundreds.gameObject.SetActive(false);
        }
        else
        {
            hundredsInt = 1;
            hundreds.gameObject.SetActive(true);
        }

        //10s
        if (health < 10)
        {
            tensInt = 0;
        }
        else
        {
            tensInt = (int)Mathf.Floor(health / 10.0f);
            if (tensInt == 10)
            {
                tensInt = 0;
            }
        }

        if (health <= 0.1)
        {
            hundreds.gameObject.SetActive(false);
        }
      


        onesInt = (int)Mathf.Floor(health - (hundredsInt*100) - (tensInt * 10));


        //Debug.Log("Changing Hundreds: " + hundredsInt);
        hundreds.changeDigit(hundredsInt);
       // Debug.Log("Changing Tens: " + tensInt);
        tens.changeDigit(tensInt);
       // Debug.Log("Changing ones: " + onesInt);
        ones.changeDigit(onesInt);

    }


}
