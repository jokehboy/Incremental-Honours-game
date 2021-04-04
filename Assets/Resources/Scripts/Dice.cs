using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Dice : MonoBehaviour {

    public DiceGame diceGame;
    public int theSide;

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image rend;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<Image>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Sprites/DiceSides/");
	}
	
    // If you left click over the dice then RollTheDice coroutine is started
    public void Roll()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 6);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        diceGame.isRolling = false;
        theSide = finalSide;

        if (this.name == "dice1") diceGame.dice1Face = finalSide;
        if (this.name == "dice2") diceGame.dice2Face = finalSide;

        diceGame.diceTotal = diceGame.dice1Face + diceGame.dice2Face;
        diceGame.shouldCheckBet = true;
        Debug.Log(theSide);
    }
}
