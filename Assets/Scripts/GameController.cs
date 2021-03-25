using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;


[Serializable]
public class PlayerData 
{
    public bool offlineProgCheck;
    public BigDouble currency;  //The users currecny count
    public BigDouble totalCurrency;



    //public double coinsPerClick_UpgradeCost;
    public BigDouble coinsPerClick_CurrentCPC;
    public BigDouble coinsPerClick_CPC_Amount;
    public int coinsPerClick_Level;

    public BigDouble coinsPerSecond; //The Coins earned Per Second
    //public double coinsPerSecond_UpgradeCost; //The cost of the next upgrade
    public int coinsPerSecond_Level; //The level of CPS
    public BigDouble coinsPerSecond_Amount; //The amount that the CPS will go up on purchase
    public BigDouble coinsPerSecond_CurrentCPS; //The current amount of CPS

    public int production_level;
    public BigDouble production_multiplier;
    public BigDouble production_level_ToGet;

    public BigDouble product;

    public BigDouble ach_lvl1;
    public BigDouble ach_lvl2;

    public int standard_Upgrade_lvl_1;
    public int standard_Upgrade_lvl_2;
    public int standard_Upgrade_lvl_3;
    public int standard_Upgrade_lvl_4;
    public int standard_Upgrade_lvl_5;
    public int standard_Upgrade_lvl_6;
    public int standard_Upgrade_lvl_7;
    public int standard_Upgrade_lvl_8;
    public int standard_Upgrade_lvl_9;
    public int standard_Upgrade_lvl_10;
    public int standard_Upgrade_lvl_11;
    public int standard_Upgrade_lvl_12;
    public int standard_Upgrade_lvl_13;
    public int standard_Upgrade_lvl_14;
    public int standard_Upgrade_lvl_15;
                             

    #region Production upgrades
    public int prestiege_upg_lvl1;
    public int prestiege_upg_lvl2;
    public int prestiege_upg_lvl3;
    public int prestiege_upg_lvl4;
    public int prestiege_upg_lvl5;

    #endregion

    public BigDouble eventTokens;
    public float[] eventCooldown = new float[7];

    //public GameObject[] upgrade_Progression_Array = new GameObject[15];
    public bool[] unlocked = new bool[15];
    


    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        offlineProgCheck = false;
        currency = 0;
        totalCurrency = 0;
        coinsPerSecond_Level = 0;
        coinsPerSecond_Amount = 0.1;

        coinsPerClick_Level = 0;
        coinsPerClick_CPC_Amount = 0.5;

        production_level = 0;

        ach_lvl1 = 0;
        ach_lvl2 = 0;

        eventTokens = 0;

        for (int i = 0; i < eventCooldown.Length - 1; i++)
            eventCooldown[i] = 0;

        for (int i = 0; i < unlocked.Length; i++)
            unlocked[i] = false;

            
    }

}

public class GameController : MonoBehaviour
{
    public PlayerData data;

    public TextMeshProUGUI currencyText; //A visual of the users currency;

    public TextMeshProUGUI coinsPerSecond_View_Text;
    public TextMeshProUGUI coinsPerClick_View_Text;

    public TextMeshProUGUI coinsPerSecond_Upgrade_Text_Cost;
    public TextMeshProUGUI coinsPerSecond_Upgrade_Text_Amount;
    public TextMeshProUGUI coinsPerSecond_Upgrade_Text_CurrentLevel;

    public TextMeshProUGUI coinsPerClick_Upgrade_Text_Cost;
    public TextMeshProUGUI coinsPerClick_Upgrade_Text_Amount;
    public TextMeshProUGUI coinsPerClick_Upgrade_Text_CurrentLevel;



    public Image coinsPerClick_Bar;
    public Image coinsPerClick_Bar_BG;

    public BigDouble currencyTemp;

    public Image coinsPerSecond_Bar;
    public Image coinsPerSecond_Bar_BG;

    public TextMeshProUGUI production_Upgrade_Text;
    public TextMeshProUGUI production_Level_Text;
    public TextMeshProUGUI production_View_Amount_Text;


    public BigDouble theCost_CPS => 10 * Pow(1.07, data.coinsPerSecond_Level);


    public BigDouble theCost_CPC => 10 * Pow(1.1, data.coinsPerClick_Level);


    //Max Buy Count Text

    public TextMeshProUGUI buyUpgradeMaxCount_CPC;
    public TextMeshProUGUI buyUpgradeMaxCount_CPS;

    //Acheivements

    public GameObject acheivementScreen;
    public List<Acheivement> acheievmentList = new List<Acheivement>();

    public ProductionUpgrades productionUpgrades;


    public bool counting = true;

    public ButtonParticles buttonParticles_script;

    //Progression 
    public int standard_progressionTracker;
    public GameObject upgradeScreen;
    public Unlock_standard[] upgrade_Progression_Array = new Unlock_standard[15];

    public int[] standard_Upgrade_Levels;
    public BigDouble[] standard_Upgrade_BaseCost;
    public float[] standard_Upgrade_ChangeInPrice;

    //public int standard_progressionTracker;

    public void Start()
    {
        Application.targetFrameRate = 60;

        foreach (var obj in acheivementScreen.GetComponentsInChildren<Acheivement>())
        {
            acheievmentList.Add(obj);
        }

        //data.upgrade_Progression_Array = upgradeScreen.GetComponentsInChildren<Unlock_standard>()

        SaveSystem.LoadPlayer(ref data);

        productionUpgrades.StartProductionUpgrades();
        //upgrade_Progression_Array = GameObject.FindGameObjectsWithTag("Upgrade_Standard");


        for (int i =0; i < upgrade_Progression_Array.Length; i++)
        {
            upgrade_Progression_Array[i].isUnlocked = data.unlocked[i];
            Debug.Log(upgrade_Progression_Array[i].isUnlocked);
        }

        StartCoroutine(CoinsPerSecond(1.0f));


    }

    public void CanvasGroupActive(bool active, CanvasGroup theGroup)
    {
        if (active)
        {
            theGroup.alpha = 1;
            theGroup.interactable = true;
            theGroup.blocksRaycasts = true;
            return;
        }

        theGroup.alpha = 0;
        theGroup.interactable = false;
        theGroup.blocksRaycasts = false;

    }



    public void SetvaluesToDefault() //Every single time a new upgrade is created, remember to add it here!!!
    {
        data.currency = 0;

        //coinsPerSecond_UpgradeCost = 25;
        data.coinsPerSecond_Amount = 0.1f;
        data.coinsPerSecond_Level = 0;

        //coinsPerClick_UpgradeCost = 10f;
        data.coinsPerClick_CPC_Amount = 0.2f;
        data.coinsPerClick_CurrentCPC = 0.2f;
        data.coinsPerClick_Level = 0;
    }

    public void Prestige() //User can spend currecny to reset all production and gain a multiplier for everything overall
    {

    }


    private void FixedUpdate()
    {
        //SaveSystem.SavePlayer(data);
    }

    public void UnlockProgression()
    {
        //data.upgrade_Progression_Array[0].GetComponent<Unlock_standard>()
        upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().active = true;

        if(upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().active && !upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().Locked();
            upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().unlockText.text = $"{upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock} coins to unlock this upgrade.";
            BigDoubleFillAmount(data.currency, upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock, upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().progressionBar);
        }
        if(data.currency >= upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock)
        {
            upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().isUnlocked = true;
            data.unlocked[standard_progressionTracker] = upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().isUnlocked;
        }
        if (upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            upgrade_Progression_Array[standard_progressionTracker].GetComponent<Unlock_standard>().Unlocked();
            standard_progressionTracker++;
        }

    }


    public void GameText_Upgrades_Update() //Set and update what text should appear on upgrade buttons
    {

        coinsPerSecond_Upgrade_Text_Cost.text = UpdateNotation(theCost_CPS, "F0");
        coinsPerSecond_Upgrade_Text_Amount.text = "+" + (data.coinsPerSecond_Amount * data.production_multiplier).ToString("F3") + " c/s";
        coinsPerSecond_Upgrade_Text_CurrentLevel.text = "LVL: " + data.coinsPerSecond_Level.ToString();

        coinsPerClick_Upgrade_Text_Cost.text = UpdateNotation(theCost_CPC, "F0");
        coinsPerClick_Upgrade_Text_Amount.text = "+" + (data.coinsPerClick_CPC_Amount * data.production_multiplier).ToString("F3");
        coinsPerClick_Upgrade_Text_CurrentLevel.text = "LVL: " + data.coinsPerClick_Level.ToString();

        production_Level_Text.text = "Current Production level: " + Floor(data.production_level).ToString("F0");

        production_Upgrade_Text.text = "Expand production by:\n" + Floor(data.production_level_ToGet).ToString("F0") + " levels";


        //buyUpgradeMaxCount_CPC.text = "Buy Max (" + BuyUpgradeMaxCount_CPC() + ")";
        //buyUpgradeMaxCount_CPS.text = "Buy Max (" + BuyUpgradeMaxCount_CPS() + ")";
    }

    public void GameText_Information_Update() //Set and update what text should appear on constantly changing information
    {
        currencyText.text = "Coins: " + UpdateNotation(data.currency, "F3");
        coinsPerClick_View_Text.text = "CPC: +" + UpdateNotation(TotalCPC(), "F3");
        coinsPerSecond_View_Text.text = UpdateNotation(TotalCPS(), "F3") + " coins/s";
        production_View_Amount_Text.text = "Production Boost: " + (data.production_multiplier).ToString("F3") + "x";

        Debug.Log(TotalCPC());
    }

    public string UpdateNotation(BigDouble value, string stringFormat)
    {
        if (value > 1000)
        {
            var exponent = (Floor(Log10(Abs(value))));
            var mantissa = (value / Pow(10, exponent));
            return mantissa.ToString(format: "F3") + "e" + exponent;
        }
        else
        {
            return value.ToString(stringFormat);
        }
    }

    #region Buy Logic----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void BuyUpgradeTypes(string upgradeID)
    {
        BigDouble basePriceOfUpgrade;
        var coins = data.currency;
        float changeInPrice;
        int currentUpgradeLvl;

        int numberToBuy;

        BigDouble theCost;

        switch (upgradeID)
        {
            case "CPS_Upgrade_1_B1":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[0];
                    changeInPrice = standard_Upgrade_ChangeInPrice[0];
                    currentUpgradeLvl = standard_Upgrade_Levels[0];
                    numberToBuy = 1;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    //Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys)

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;
            case "CPS_Upgrade_1_B5":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[0];
                    changeInPrice = standard_Upgrade_ChangeInPrice[0];
                    currentUpgradeLvl = standard_Upgrade_Levels[0];
                    numberToBuy = 5;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;
            case "CPS_Upgrade_1_B10":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[0];
                    changeInPrice = standard_Upgrade_ChangeInPrice[0];
                    currentUpgradeLvl = standard_Upgrade_Levels[0];
                    numberToBuy = 10;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;

            case "CPC_Upgrade_2_B1":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[1];
                    changeInPrice = standard_Upgrade_ChangeInPrice[1];
                    currentUpgradeLvl = standard_Upgrade_Levels[1];
                    numberToBuy = 1;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;
            case "CPC_Upgrade_2_B5":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[1];
                    changeInPrice = standard_Upgrade_ChangeInPrice[1];
                    currentUpgradeLvl = standard_Upgrade_Levels[1];
                    numberToBuy = 5;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;
            case "CPC_Upgrade_2_B10":
                {
                    basePriceOfUpgrade = standard_Upgrade_BaseCost[1];
                    changeInPrice = standard_Upgrade_ChangeInPrice[1];
                    currentUpgradeLvl = standard_Upgrade_Levels[1];
                    numberToBuy = 10;

                    theCost = basePriceOfUpgrade * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, numberToBuy) - 1) / (changeInPrice - 1)); ;

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberToBuy);
                }
                break;
            case "Production_Level_Up":
                {
                    if (data.currency > 450)
                    {
                        SetvaluesToDefault();

                        data.production_level += data.production_level_ToGet;

                    }
                }
                break;
        }

        data.coinsPerSecond_CurrentCPS = data.coinsPerSecond_Level * data.coinsPerSecond_Amount;
        //data.coinsPerClick_CurrentCPC = TotalCPC();

    }

    public void Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys)
    {

        if (currency < theCost) return;
        currency -= theCost;
        upgradeLevel += numberOfBuys;
    }

    public BigDouble BuyUpgradeMaxCount_CPC()
    {
        var b = 10;
        var coins = data.currency;
        var changeInPrice = 1.1;
        var currentUpgradeLvl = data.coinsPerClick_Level;

        var n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));
        return n;
    }



    public BigDouble BuyUpgradeMaxCount_CPS()
    {
        var b = 10;
        var coins = data.currency;
        var changeInPrice = 1.07;
        var currentUpgradeLvl = data.coinsPerSecond_Level;

        var n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

        return n;
    }
    #endregion
    
    public BigDouble TotalCPC()
    {
        var temp = data.coinsPerClick_CurrentCPC;
        temp += ((data.coinsPerClick_Level * data.coinsPerClick_CPC_Amount) * data.production_multiplier) + 0.5f;
        temp *= BigDouble.Pow(1.05, productionUpgrades.levels[2]);
        return temp;
    }

    public BigDouble TotalCPS()
    {
        var temp = data.coinsPerSecond_Level * data.coinsPerSecond_Amount;
        temp *= data.production_multiplier;
        temp *= BigDouble.Pow(1.1, productionUpgrades.levels[3]);
        return temp;
    }

    public void Update()
    {
        RunAcheivements();
        UnlockProgression();
        productionUpgrades.Run();

        GameText_Upgrades_Update();
        GameText_Information_Update();

        SmoothNumber(ref currencyTemp, data.currency);
        BigDoubleFillAmount(data.currency, theCost_CPC, coinsPerClick_Bar_BG);
        BigDoubleFillAmount(currencyTemp, theCost_CPC, coinsPerClick_Bar);

        BigDoubleFillAmount(data.currency, theCost_CPS, coinsPerSecond_Bar_BG);
        BigDoubleFillAmount(currencyTemp, theCost_CPS, coinsPerSecond_Bar);



        data.production_level_ToGet = (150 * Sqrt(data.currency / 1e7));
        data.production_multiplier = (data.production_level * 0.05) + 1;

        //data.coinsPerSecond_CurrentCPS = TotalCPS() ;
        //data.coinsPerClick_CurrentCPC = TotalCPC();

        saveTimer += Time.deltaTime;

        if(!(saveTimer >= 15))return;
        SaveSystem.SavePlayer(data);
        saveTimer = 0;

    }
    public float saveTimer;

    #region Acheivements----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static string[] AcheievementStrings => new string[] { "Current Coins", "Total Coins Collected" };
    private BigDouble[] AcheivementNumbers => new BigDouble[] { data.currency, data.totalCurrency };

    private void RunAcheivements()
    {
        UpdateAcheivements(AcheievementStrings[0], AcheivementNumbers[0], ref data.ach_lvl1, ref acheievmentList[0].fill, ref acheievmentList[0].titles, ref acheievmentList[0].progress);
        UpdateAcheivements(AcheievementStrings[1], AcheivementNumbers[1], ref data.ach_lvl2, ref acheievmentList[1].fill, ref acheievmentList[1].titles, ref acheievmentList[1].progress);
    } 

    private void UpdateAcheivements(string name, BigDouble number, ref BigDouble level, ref Image fill, ref TextMeshProUGUI title, ref TextMeshProUGUI progress)
    {
        var cap = BigDouble.Pow(10, level);

        title.text = $"{name}\n Current lvl: {level}";

        progress.text = $"{UpdateNotation(number, "F2")} / {UpdateNotation(cap, "F2")}";

        BigDoubleFillAmount(number, cap, fill);

        if (number < cap) return;
        BigDouble levels = 0;
        if (number / cap >= 1)
            levels = Floor(Log10(number / cap)) + 1;
        level += levels;

       
                    
    }
    #endregion




    public void SmoothNumber(ref BigDouble tempVar, BigDouble actualVar)
    {
        if(tempVar > actualVar & actualVar ==0)
        {
            tempVar -= (tempVar - actualVar) / 1 * Time.deltaTime;
        }
        else if(Floor(tempVar) < actualVar)
        {
            tempVar += (actualVar - tempVar) / 1 * Time.deltaTime;
        }
        else if(Floor(tempVar) > actualVar)
        {
            tempVar -= (tempVar - actualVar) / 1 * Time.deltaTime;
        }
        else
        {
            tempVar = actualVar;
        }

        
    }

    public void BigDoubleFillAmount(BigDouble x, BigDouble y, Image fill)
    {
        float z;
        var a = x / y;
        if(a < 0)
        {
            z = 0;
        }
        else if(a > 10)
        {
            z = 1;
        }
        else
        {
            z = (float)a.ToDouble();
            fill.fillAmount = z;
        }
    }


    IEnumerator CoinsPerSecond(float timeBetween) //Tick up the coins every second instead of constantly adding coins, looks a little better
    {
        while(counting == true)
        {


            data.coinsPerSecond = TotalCPS();
            data.currency += data.coinsPerSecond;
            data.totalCurrency += data.coinsPerSecond;
            yield return new WaitForSeconds(timeBetween);
        }
    } 

 


    public void MainButton_Click()
    {
        data.currency += TotalCPC();
        data.totalCurrency += TotalCPC();
        //buttonParticles_script.ButtonClick();
    }


    public void FullReset()
    {
        data.FullReset();
    }

}
