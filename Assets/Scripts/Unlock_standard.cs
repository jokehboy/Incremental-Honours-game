using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

[System.Serializable]
public class Unlock_standard : MonoBehaviour
{
    public GameObject LockedPanel;
    public GameObject UnlockedPanel;

    public Image progressionBar;
    public TextMeshProUGUI unlockText;

    public BigDouble coinsNeededToUnlock;

    public bool isUnlocked;

    public bool active;

    public CanvasGroup overallGroup;
    public CanvasGroup lockedCanvasGroup;
    public CanvasGroup unlockedCanvasGroup;


    // Start is called before the first frame update
    void Awake()
    {
        if (!active)
        {
            overallGroup.alpha = 0;
            overallGroup.interactable = false;
            overallGroup.blocksRaycasts = false;
        }

        this.active = false;
        

    }

    public void Locked()
    {
        lockedCanvasGroup.alpha = 1;
        unlockedCanvasGroup.alpha = 0;

        lockedCanvasGroup.interactable = true;
        unlockedCanvasGroup.interactable = false;

        lockedCanvasGroup.blocksRaycasts = true;
        unlockedCanvasGroup.blocksRaycasts = false;

        //unlockText.text = $"{coinsNeededToUnlock} coins to unlock this upgrade.";
    }

    public void Unlocked()
    {
        lockedCanvasGroup.alpha = 0;
        unlockedCanvasGroup.alpha = 1;

        lockedCanvasGroup.interactable = false;
        unlockedCanvasGroup.interactable = true;

        lockedCanvasGroup.blocksRaycasts = false;
        unlockedCanvasGroup.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            overallGroup.alpha = 1;
            overallGroup.interactable = true;
            overallGroup.blocksRaycasts = true;
        }
    }
}
