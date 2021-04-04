using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class SpinGame : MonoBehaviour
{
    public Transform gameContainer;

    public Transform[] rewards;

    public int previousLevel;

    public float spacing;

    public bool isRolling;

    public float transition;
    public float animTime;

    public AnimationCurve animCurve;

    public string rewardName;

    public GameController game;
    public BigDouble currentBet;

    public TextMeshProUGUI currentBet_Text;
    public TextMeshProUGUI plusBet;
    public TextMeshProUGUI minusBet;

    public CanvasGroup buttons;

    // Start is called before the first frame update
    void Start()
    {
        rewards = new Transform[gameContainer.childCount];
        for(int i = 0; i < gameContainer.childCount;i++)
        {
            rewards[i] = gameContainer.GetChild(i);
        }
        currentBet = game.data.base_bet_amount / 5;
        currentBet_Text.text = $"{currentBet}";

        plusBet.text = $"+{game.data.base_bet_amount / 10}";
        minusBet.text = $"-{game.data.base_bet_amount / 10}";
    }

    public void Spin()
    {
        transition = 0.0f;
        isRolling = true;
        float offset = 0.0f;
        List<int> indexes = new List<int>();
        for(int i = 0; i < rewards.Length; i++)
        {
            indexes.Add(i);
        }
        for (int i = 0; i < rewards.Length; i++)
        { 
            int index = indexes[Random.Range(0, indexes.Count)];
            indexes.Remove(index);
            rewards[index].transform.localPosition = Vector2.right * offset;
            offset += spacing;
            rewardName = rewards[index].name;
        }

    }

    public void ButtonsActive(bool acive)
    {
        if(acive)
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

    public void SpinButton()
    {
        if(game.data.currency >= currentBet)
        {
            game.data.currency -= currentBet;
            game.data.times_played_spinwheel++;
            game.data.total_spent_games += currentBet.ToDouble();
            Spin();
        }
    }

    public void UpdateUI()
    {

        plusBet.text = $"+{game.data.base_bet_amount / 10}";
        minusBet.text = $"-{game.data.base_bet_amount / 10}";

        currentBet_Text.text = $"Bet {currentBet}";

    }

    // Update is called once per frame
    void Update()
    {
        if(isRolling)
        {
            ButtonsActive(false);
            Vector2 endPoint = (-Vector2.right * spacing) * (rewards.Length - 1);
            gameContainer.transform.localPosition = Vector2.Lerp(Vector2.right * spacing, endPoint, animCurve.Evaluate(transition));
            transition += Time.deltaTime / animTime;
            if(transition >1)
            {
                isRolling = false;
                //Give user reward
                switch(rewardName)
                {
                    case "10x":
                        {
                            game.data.currency += currentBet * 10;
                            game.data.totalCurrency += currentBet * 10;
                            game.AnimateClick(currentBet * 10, GameObject.FindGameObjectWithTag("Games"));
                            game.data.total_earned_games += (currentBet * 10).ToDouble();
                        }
                        break;
                    case "5x":
                        {
                            game.data.currency += currentBet * 5;
                            game.data.totalCurrency += currentBet * 5;
                            game.AnimateClick(currentBet * 5, GameObject.FindGameObjectWithTag("Games"));
                            game.data.total_earned_games += (currentBet * 5).ToDouble();
                        }
                        break;
                    case "3x":
                        {
                            game.data.currency += currentBet * 3;
                            game.data.totalCurrency += currentBet * 5;
                            game.AnimateClick(currentBet * 3, GameObject.FindGameObjectWithTag("Games"));
                            game.data.total_earned_games += (currentBet * 3).ToDouble();
                        }
                        break;
                    case "2x":
                        {
                            game.data.currency += currentBet * 2;
                            game.data.totalCurrency += currentBet * 2;
                            game.AnimateClick(currentBet * 2, GameObject.FindGameObjectWithTag("Games"));
                            game.data.total_earned_games += (currentBet * 2).ToDouble();
                        }
                        break;
                    case "0x":
                        {
                            game.data.total_lost_games += currentBet.ToDouble();
                        }
                        break;
                }

                ButtonsActive(true);
            }
        }

        if (previousLevel != game.data.production_upg_lvl6)
        {
            currentBet = game.data.base_bet_amount / 5;
        }
        previousLevel = game.data.production_upg_lvl6;

        UpdateUI();
    }
}
