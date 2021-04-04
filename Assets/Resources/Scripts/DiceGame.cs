using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class DiceGame : MonoBehaviour
{
    public Dice dice1;
    public Dice dice2;

    public int dice1Face;
    public int dice2Face;
    public int diceTotal;

    public CanvasGroup buttons;
    public bool isRolling;
    public bool shouldCheckBet = false;

    public int previousLevel;

    public GameController game;
    public BigDouble currentBet;

    public TextMeshProUGUI currentBet_Text;
    public TextMeshProUGUI plusBet;
    public TextMeshProUGUI minusBet;

    public string[] betType = new string[15];
    public int[] betReturnMulti = new int[15];
    public TextMeshProUGUI[] betType_Text = new TextMeshProUGUI[15];

    public Button selectedBet_Button;
    public string selectedBet;

    public void OnButtonClick()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        if(go != null)
        {
            go.GetComponent<Button>().Select();
            go.GetComponent<Button>().OnSelect(null);
            selectedBet_Button = go.GetComponent<Button>();
            selectedBet = go.name;
        }
    }

    public void PlaceBet()
    {
        if(selectedBet_Button !=null && game.data.currency >= currentBet)
        {
            game.data.currency -= currentBet;
            dice1.Roll();
            dice2.Roll();
            selectedBet_Button.Select();
            game.data.total_spent_games += currentBet.ToDouble();
            game.data.times_played_dice++;
            isRolling = true;
        }
        
    }
    public void ChangeBet(string buttonPressed)
    {
        switch (buttonPressed)
        {
            case "Plus_Bet":
                {
                    if (currentBet >= game.data.base_bet_amount) return;
                    currentBet += game.data.base_bet_amount / 10;
                }
                break;
            case "Minus_Bet":
                {
                    if (currentBet <= game.data.base_bet_amount / 10) return;
                    currentBet -= game.data.base_bet_amount / 10;
                }
                break;
        }

        UpdateUI();
    }

    public void ButtonsActive(bool acive)
    {
        if (acive)
        {
            buttons.interactable = true;
        }
        else
        {
            buttons.interactable = false;
        }
    }

    public void UpdateUI()
    {

        plusBet.text = $"+{game.data.base_bet_amount / 10}";
        minusBet.text = $"-{game.data.base_bet_amount / 10}";

        currentBet_Text.text = $"Bet {currentBet}";

    }

    // Start is called before the first frame update
    void Start()
    {
        string[] text = new string[] {"Odd\n<size=30>2x</size>", "Even\n<size=30>2x</size>", "Over 7\n<size=30>2x</size>", "Under 7\n<size=30>2x</size>", "2\n<size=30>30x</size>", "3\n<size=30>15x</size>", "4\n<size=30>10x</size>", "5\n<size=30>7x</size>", "6\n<size=30>6x</size>", "7\n<size=30>5x</size>", "8\n<size=30>6x</size>", "9\n<size=30>7x</size>", "10\n<size=30>10x</size>", "11\n<size=30>15x</size>", "12\n<size=30>30x</size>" };

        for(int i = 0; i < betType_Text.Length; i++)
        {
            betType_Text[i].text = text[i];
        }
    }

    public string CheckIfOver()
    {
        if (diceTotal > 7) return "over7";
        if (diceTotal < 7) return "under7";
        return "";
    }
    public string CheckIfEven()
    {
        if (diceTotal % 2 == 0) return "even";
        else return "odd";
    }
    public string CheckValue(int diceTotal)
    {
        return diceTotal.ToString();
    }

    public void  BeegWin()
    {
        var index = 0;
        for(int i = 0; i < betType.Length; i++)
        {
            if (selectedBet == betType[i]) index = i;
        }

        if (selectedBet == CheckIfOver())
        {
            Debug.Log("He Win");
            game.data.currency += currentBet * betReturnMulti[index];
            game.data.totalCurrency += currentBet * betReturnMulti[index];
            game.AnimateClick(currentBet * betReturnMulti[index], GameObject.FindGameObjectWithTag("Games"));
            game.data.total_earned_games += (currentBet * betReturnMulti[index]).ToDouble();
        }
        else if (selectedBet == CheckIfEven())
        {
            Debug.Log("He Win");
            game.data.currency += currentBet * betReturnMulti[index];
            game.data.totalCurrency += currentBet * betReturnMulti[index];
            game.AnimateClick(currentBet * betReturnMulti[index], GameObject.FindGameObjectWithTag("Games"));
            game.data.total_earned_games += (currentBet * betReturnMulti[index]).ToDouble();
        }
        else if (selectedBet == CheckValue(diceTotal))
        {
            Debug.Log("He Win");
            game.data.currency += currentBet * betReturnMulti[index];
            game.data.totalCurrency += currentBet * betReturnMulti[index];
            game.AnimateClick(currentBet * betReturnMulti[index], GameObject.FindGameObjectWithTag("Games"));
            game.data.total_earned_games += (currentBet * betReturnMulti[index]).ToDouble();
        }
        else
        {
            Debug.Log("He Lose");
            game.data.total_lost_games += currentBet.ToDouble();
        }

        Debug.Log(index);

    }

    // Update is called once per frame
    void Update()
    {
        if (isRolling)
        {
            ButtonsActive(false);
        }
        else ButtonsActive(true);

         while (shouldCheckBet == true)
        {
           
            Debug.Log("CheckingBet");
            BeegWin();
            shouldCheckBet = false;
        }

        if (previousLevel != game.data.production_upg_lvl6)
        {
            currentBet = game.data.base_bet_amount / 5;
        }
        previousLevel = game.data.production_upg_lvl6;
        UpdateUI();
    }
}
