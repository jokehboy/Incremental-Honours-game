using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class CoinFlip : MonoBehaviour
{
    public GameController game;
    public int face;

    public int previousLevel;
    public CanvasGroup buttons;

    public TextMeshProUGUI headsText;
    public TextMeshProUGUI tailsText;

    public GameObject headSelected;
    public GameObject tailSelected;

    public int playerSelection;

    public bool playing = false;
    public bool shoulCheckResult = false;

    public float transition;
    public float animTime;
    public AnimationCurve animCurve;

    public TextMeshProUGUI currentBet_Text;
    public TextMeshProUGUI plusBet;
    public TextMeshProUGUI minusBet;

    public BigDouble currentBet;

    public Color OrigionalColor;


    private void Start()
    {
        SelectChoice(1);
        currentBet = game.data.base_bet_amount / 5;
        currentBet_Text.text = $"{currentBet}";

        plusBet.text = $"+{game.data.base_bet_amount / 10}";
        minusBet.text = $"-{game.data.base_bet_amount / 10}";
    }

    public void FlipCoin()
    {
        transition = 0;
        playing = true;
        int number = Random.Range(0, 100);

        if (number < 50) face = 0; //Heads
        else face = 1; //Tails

    }
    public void PlayButton()
    {
        if (game.data.currency >= currentBet)
        {
            game.data.currency -= currentBet;
            game.data.times_played_coinflip++;
            game.data.total_spent_games += currentBet.ToDouble();
            FlipCoin();
        }
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

    public void SelectChoice(int type)
    {
        if(type == 0 && !playing)
        {
            headSelected.SetActive(true);
            tailSelected.SetActive(false);
            playerSelection = 0;
        }
        if (type == 1 && !playing)
        {
            headSelected.SetActive(false);
            tailSelected.SetActive(true);
            playerSelection = 1;
        }
    }

    public void UpdateUI()
    {

        plusBet.text = $"+{game.data.base_bet_amount / 10}";
        minusBet.text = $"-{game.data.base_bet_amount / 10}";

        currentBet_Text.text = $"{currentBet}";

    }

    void CheckIfWon()
    {
        if(face == playerSelection)
        {
            game.data.currency += currentBet * 2;
            game.data.totalCurrency += currentBet * 2;
            game.AnimateClick(currentBet * 2,GameObject.FindGameObjectWithTag("Games"));
            game.data.total_earned_games += (currentBet * 2).ToDouble();
        }
        else
        {
            game.data.total_lost_games += currentBet.ToDouble();
        }
    }

    private void Update()
    {
        
        if (playing)
        {
            transition += Time.deltaTime / animTime;
            if (face == 0)
            {
                Color textColor = tailsText.color;
                var alpha = Mathf.Lerp(1, 0, animCurve.Evaluate(transition));
                tailsText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
                headsText.alpha = 1;

            }
            if (face == 1)
            {
                Color textColor = headsText.color;
                var alpha = Mathf.Lerp(1, 0, animCurve.Evaluate(transition));
                headsText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
                tailsText.alpha = 1;
            }
            if(transition > 1)
            {
                shoulCheckResult = true;
                playing = false;
                tailsText.color = OrigionalColor;
                headsText.color = OrigionalColor;

            }
        }

        if (playing) ButtonsActive(false);
        else ButtonsActive(true);
        
        while (shoulCheckResult == true)
        {

            Debug.Log("CheckingBet");
            CheckIfWon();
            shoulCheckResult = false;
        }

        if (previousLevel != game.data.production_upg_lvl6)
        {
            currentBet = game.data.base_bet_amount / 5;
        }
        previousLevel = game.data.production_upg_lvl6;
        UpdateUI();
    }
}