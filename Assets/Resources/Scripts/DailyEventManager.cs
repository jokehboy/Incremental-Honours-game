#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;
using System;

public class DailyEventManager : MonoBehaviour
{
    public GameController game;
    public TextMeshProUGUI eventTokensText;

    public GameObject eventRewardPopup;

    public TextMeshProUGUI popupRewardText;

    public BigDouble eventTokenBoost => (game.data.eventTokens / 100)+1 ;
    
    public GameObject[] events = new GameObject[7];
    public GameObject[] eventsUnlocked = new GameObject[7];
    public TextMeshProUGUI[] rewardText = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] currecyText = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] costText = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] startText = new TextMeshProUGUI[7];

    public BigDouble[] reward = new BigDouble[7];
    public BigDouble[] currencies = new BigDouble[7];

    public BigDouble[] costs = new BigDouble[7];
    public int[] levels = new int[7];

    public bool eventActive;
    public int eventActiveID;

    public string DayOfTheWeek()
    {
        //var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 3);
        var dt = DateTime.Now;
        return dt.DayOfWeek.ToString();
    }

    public string previousDayChecked;

    public void Start()
    {
        eventActive = false;
        previousDayChecked = DayOfTheWeek();
    }

    public void Update()
    {
        var data = game.data;

        eventTokensText.text = $"Event Tokens: {game.UpdateNotation(data.eventTokens, "F2")} ({game.UpdateNotation(eventTokenBoost, "F2")}x)";
        reward[0] = BigDouble.Log10(currencies[0] + 1);
        reward[1] = BigDouble.Log10(currencies[1] / 5 + 1);

        for (var i = 0; i < 7; i++)
            costs[i] = 10 * BigDouble.Pow(1.15, levels[i]);
        
        if(previousDayChecked != DayOfTheWeek() & eventActive)
        {
            eventActiveID = 0;
            for (var i = 0; i < 7; i++)
            {
                data.eventCooldown[i] = 0;
            }
        }

        switch(DayOfTheWeek())
        {
            case "Monday":
                RunEventUI(0);
                break;
            case "Tuesday":
                RunEventUI(1);
                break;
            case "Wednesday":
                RunEventUI(2);
                break;
            case "Thursday":
                RunEventUI(3);
                break;
            case "Friday":
                RunEventUI(4);
                break;
            case "Saturday":
                RunEventUI(5);
                break;
            case "Sunday":
                RunEventUI(6);
                break;
            

        }

        if (eventActiveID == 0 & game.data.eventCooldown[CurrentDay()] > 0)
            game.data.eventCooldown[CurrentDay()] -= Time.deltaTime;
        else if (eventActiveID != 0 & game.data.eventCooldown[CurrentDay()] > 0)
            game.data.eventCooldown[CurrentDay()] -= Time.deltaTime;
        else if (eventActiveID != 0 & game.data.eventCooldown[CurrentDay()] <= 0)
            CompleteEvent(CurrentDay());
    }

    public int CurrentDay()
    {
        switch (DayOfTheWeek())
        {
            case "Monday": return 0;
            case "Tuesday": return 1;
            case "Wednesday": return 2;
            case "Thursday": return 3;
            case "Friday": return 4;
            case "Saturday": return 5;
            case "Sunday": return 6;

        }
        return 0;
    }

    public void ToggleEvent(int id)
    {
        var data = game.data;

        var  now = DateTime.Now;

        if (eventActiveID == 0 & data.eventCooldown[id] <= 0 & !(now.Second == 23 & now.Minute >= 55))
        {
            eventActiveID = id;
            data.eventCooldown[id] = 300;
            currencies[id] = 0;
            levels[id] = 0;
        }
        else if (now.Second == 23 & now.Minute >= 55) return;
        else if (data.eventCooldown[id] > 0) return;
        else
        {
            CompleteEvent(id);
        }
    }

    private void CompleteEvent(int id)
    {
        var data = game.data;
        popupRewardText.text = $"+{reward[id]} Event tokens earned.";
        data.eventTokens += reward[id];
        currencies[id] = 0;
        levels[id] = 0;

        eventActiveID = 0;
        data.eventCooldown[id] = 7200;

        eventRewardPopup.SetActive(true);
        
    }

    public void Buy(int id)
    {
        if (currencies[id] >= costs[id]) return;
        currencies[id] -= costs[id];
        levels[id]++;
    }

    public void ClosePopup()
    {
        eventRewardPopup.SetActive(false);
    }

    public void Click(int id)
    {
        switch(id)
        {
            case 1:
                currencies[id] += 1 + levels[id];
                break;
            case 2:
                currencies[id] += 1;
                break;

        }


    }

    private void RunEvent(int id)
    { 
    
    }

    public void RunEventUI(int id)
    {
        var data = game.data;

        for(var i = 0; i < 7; i++) 
            if(i==id)
                events[i].gameObject.SetActive(true);
            else
                events[id].gameObject.SetActive(false);


        if (eventActiveID != id +1) return;
        eventsUnlocked[id].gameObject.SetActive(true);
        rewardText[id].text = $"+{reward[id]} Event Tokens";
        currecyText[id].text = $"{currecyText[id]} Beeg Tokens";
        costText[id].text = $"Cost: {costs[id]}";

        if (eventActiveID == 0)
        {
            var time = TimeSpan.FromSeconds(data.eventCooldown[id]);
            startText[id].text = data.eventCooldown[id] > 0 ? time.ToString(@"hh\:mm\:ss") : "Start Event";
        }
        else
            startText[id].text = "Exit Event";
        
    }
}
#endif 