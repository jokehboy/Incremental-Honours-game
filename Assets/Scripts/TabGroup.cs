using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public TabButton selectedTab;

    public bool startup = false;

    public List<GameObject> screenToSwapTo;

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    private void Start()
    {
        if (!startup)
        {
            selectedTab = tabButtons[0];
            startup = true;
            selectedTab.background.color = tabActive;
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;

        int index = button.transform.GetSiblingIndex();

        for(int i =0; i < screenToSwapTo.Count; i++)
        {
            if(i == index)
            {
                CanvasGroup theGroup = screenToSwapTo[i].GetComponent<CanvasGroup>();
                theGroup.alpha = 1;
                theGroup.blocksRaycasts = true;
                theGroup.interactable = true;
            }
            else
            {
                CanvasGroup theGroup = screenToSwapTo[i].GetComponent<CanvasGroup>();
                theGroup.alpha = 0;
                theGroup.blocksRaycasts = false;
                theGroup.interactable = false;
            }
        }

    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }
}
