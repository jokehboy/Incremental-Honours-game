using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Dice : MonoBehaviour {

    public DiceGame diceGame;
    public int theSide;

    //Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    //Reference to sprite
    private Image rend;

	private void Start () {

        //Assign Renderer
        rend = GetComponent<Image>();

        //Load dice sides sprites to array 
        diceSides = Resources.LoadAll<Sprite>("Sprites/DiceSides/");
	}
	
    public void Roll()
    {
        StartCoroutine("RollTheDice");
    }


    private IEnumerator RollTheDice()
    {

        int randomDiceSide = 0;
        int finalSide = 0;

        //Loop to switch dice sides ramdomly, 20 itterations.
        for (int i = 0; i <= 20; i++)
        {
            //Pick up random value
            randomDiceSide = Random.Range(0, 6);

            //Set sprite according to random value
            rend.sprite = diceSides[randomDiceSide];

            //Pause before running again
            yield return new WaitForSeconds(0.05f);
        }

        //Assigning final side
        finalSide = randomDiceSide + 1;

        //Show dice value in Console
        diceGame.isRolling = false;
        theSide = finalSide;

        //Assign dice value to diceface in dice game
        if (this.name == "dice1") diceGame.dice1Face = finalSide;
        if (this.name == "dice2") diceGame.dice2Face = finalSide;

        //Set total to both dice values
        diceGame.diceTotal = diceGame.dice1Face + diceGame.dice2Face;
        diceGame.shouldCheckBet = true;
        Debug.Log(theSide);
    }
}
