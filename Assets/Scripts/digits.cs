using UnityEngine;
using System.Collections;

public class digits : MonoBehaviour {

    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;
    private Sprite[] digitsArray;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        digitsArray = new Sprite[10];
        digitsArray[0] = zero;
        digitsArray[1] = one;
        digitsArray[2] = two;
        digitsArray[3] = three;
        digitsArray[4] = four;
        digitsArray[5] = five;
        digitsArray[6] = six;
        digitsArray[7] = seven;
        digitsArray[8] = eight;
        digitsArray[9] = nine;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
      

    }


    public void changeDigit(int digit)
    {
        spriteRenderer.sprite = digitsArray[digit];
    }

  

}
