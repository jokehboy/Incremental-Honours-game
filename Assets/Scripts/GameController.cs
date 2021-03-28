using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using UnityEngine.Analytics;
 


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

    public BigDouble production_level;
    public BigDouble production_multiplier;
    public BigDouble production_level_ToGet;

    public BigDouble product;

    public BigDouble ach_lvl1;
    public BigDouble ach_lvl2;

    public int standard_Upgrade_lvl_1; //cpS type 1
    public int standard_Upgrade_lvl_2; //cpC type 1
    public int standard_Upgrade_lvl_3; //critical click chance
    public int standard_Upgrade_lvl_4; //base critical multiplier
    public int standard_Upgrade_lvl_5; //auto clicker time per click
    public int standard_Upgrade_lvl_6; //percentage chance of product on click
    public int standard_Upgrade_lvl_7; //product earned on click
    public int standard_Upgrade_lvl_8; //cpC tupe 2
    public int standard_Upgrade_lvl_9; //cpS type 2
    public int standard_Upgrade_lvl_10; //add button
    public int standard_Upgrade_lvl_11; //increase cpc and cps
    public int standard_Upgrade_lvl_12;
    public int standard_Upgrade_lvl_13;
    public int standard_Upgrade_lvl_14;
    public int standard_Upgrade_lvl_15;

    public BigDouble cpc_Upgrade_1_base_amount; //Coins per click base amount upgrade type 1
    public BigDouble cpc_Upgrade_2_base_amount; //Coins per click base amount upgrade type 2

    public BigDouble cps_Upgrade_1_base_amount; //Coins per second base amount upgrade type 1
    public BigDouble cps_Upgrade_2_base_amount; //Coins per second base amount upgrade type 2

    public BigDouble cpsAndCpc_Upgrade_base_amount; //Upgrade both cpc and cps base amount

    public float criticalClick_Upgrade_chance_base; //Chance of a click being critical | max percentage 
    public float criticalClick_Upgrade_Multiplier; //The smallest possible critical click multiplier

    public float autoClicker_Upgrade_speed; //Speed in which the auto clicker clicks (the time between) 

    public float productionClickChance_Upgrade; //Percentage chance that you will earn product upon click | max percentage 

    public float productionEarnedOnClick_Upgrade; //The base amount of product gained on click | max level:

    public int addButton_Upgrade; //Adds another button to the screen, auto click applies, max 8


    #region Settings
    public short notationType;
    #endregion

    #region Production upgrades
    public int prestiege_upg_lvl1;
    public int prestiege_upg_lvl2;
    public int prestiege_upg_lvl3;
    public int prestiege_upg_lvl4;
    public int prestiege_upg_lvl5;
    public int prestiege_upg_lvl6;
    public int prestiege_upg_lvl7;
    public int prestiege_upg_lvl8;
    public int prestiege_upg_lvl9;
    public int prestiege_upg_lvl10;

    #endregion

    public BigDouble eventTokens;
    public float[] eventCooldown = new float[7];

    //public GameObject[] standard_upgrade_Progression_Array = new GameObject[15];
    public bool[] standard_unlocked = new bool[15];
    public bool[] production_unlocked = new bool[10];



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

        for (int i = 0; i < standard_unlocked.Length; i++)
            standard_unlocked[i] = false;

        for(int i = 0; i < production_unlocked.Length; i++)
            production_unlocked[i] = false;





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

    public GameObject crit;
    public GameObject critSpawn;

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

    public ProductionUpgrades productionUpgradeManager;
    public Upgrade_Standard standardUpgradeManager;
    public OfflineManager offlineManager;


    public bool counting = true;

    public ButtonParticles buttonParticles_script;

    //Progression 
    public int standard_upgrade_progressionTracker;
    public Unlock_standard[] standard_upgrade_Progression_Array = new Unlock_standard[15];

    public int production_upgrade_progressionTracker;
    public Unlock_standard[] production_upgrade_Progression_Array = new Unlock_standard[10];

    public int[] standard_Upgrade_Levels;
    public BigDouble[] standard_Upgrade_BaseCost;
    public float[] standard_Upgrade_ChangeInPrice;

    //public int standard_upgrade_progressionTracker;

    public void Start()
    {
        Application.targetFrameRate = 60;

        foreach (var obj in acheivementScreen.GetComponentsInChildren<Acheivement>())
        {
            acheievmentList.Add(obj);
        }

        //data.standard_upgrade_Progression_Array = upgradeScreen.GetComponentsInChildren<Unlock_standard>()

        SaveSystem.LoadPlayer(ref data);
        
        productionUpgradeManager.StartProductionUpgrades();
        standardUpgradeManager.StartStandardUpgrades();
        //standard_upgrade_Progression_Array = GameObject.FindGameObjectsWithTag("Upgrade_Standard");


        for (int i =0; i < standard_upgrade_Progression_Array.Length; i++)
        {
            standard_upgrade_Progression_Array[i].isUnlocked = data.standard_unlocked[i];
            Debug.Log(standard_upgrade_Progression_Array[i].isUnlocked);
        }
        for (int i = 0; i < production_upgrade_Progression_Array.Length; i++)
        {
            production_upgrade_Progression_Array[i].isUnlocked = data.production_unlocked[i];
            Debug.Log(production_upgrade_Progression_Array[i].isUnlocked);
        }


        TotalCPS();
        offlineManager.LoadOffline();
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
        data.cpc_Upgrade_1_base_amount = 0.5f;
        data.coinsPerClick_CurrentCPC = 0.5f;
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
        //data.standard_upgrade_Progression_Array[0].GetComponent<Unlock_standard>()
        standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().active = true;

        if(standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().active && !standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().Locked();
            standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().unlockText.text = $"{standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock} coins to unlock this upgrade.";
            BigDoubleFillAmount(data.currency, standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock, standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().progressionBar);
        }
        if(data.currency >= standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock)
        {
            standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked = true;
            data.standard_unlocked[standard_upgrade_progressionTracker] = standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked;
        }
        if (standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().Unlocked();
            standard_upgrade_progressionTracker++;
        }

    }
    public void ProductionUnlockProgression()
    {
        //data.standard_upgrade_Progression_Array[0].GetComponent<Unlock_standard>()
        production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().active = true;

        if (production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().active && !production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().Locked();
            production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().unlockText.text = $"{production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock} coins to unlock this upgrade.";
            BigDoubleFillAmount(data.currency, production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock, production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().progressionBar);
        }
        if (data.currency >= production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock)
        {
            production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked = true;
            data.production_unlocked[production_upgrade_progressionTracker] = production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked;
        }
        if (production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
        {
            production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().Unlocked();
            production_upgrade_progressionTracker++;
        }

    }

    public void GameText_Upgrades_Update() //Set and update what text should appear on upgrade buttons
    {

        coinsPerSecond_Upgrade_Text_Cost.text = UpdateNotation(theCost_CPS, "F0");
        coinsPerSecond_Upgrade_Text_Amount.text = "+" + (data.coinsPerSecond_Amount * data.production_multiplier).ToString("F3") + " c/s";
        coinsPerSecond_Upgrade_Text_CurrentLevel.text = "LVL: " + data.standard_Upgrade_lvl_1.ToString();

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

        Debug.Log(TotalCPS());
    }

    public string UpdateNotation(BigDouble value, string stringFormat)
    {
        if (value <= 1000) return value.ToString(stringFormat);

        switch (data.notationType)
        {
            case 0:
                { 
                 var exponent = (Floor(Log10(Abs(value))));
                 var mantissa = (value / Pow(10, exponent));
                 return mantissa.ToString(format: "F3") + "e" + exponent;
                }
            case 1:
                {
                    var exponent = 3*Floor(Floor(Log10(value))/3);
                    var mantissa = (value / Pow(10, exponent));
                    return mantissa.ToString(format: "F3") + "e" + exponent;
                }
            case 2:
                {
                    var exponent = 3 * Floor(Floor(Log10(value)) / 3);
                    var letterOne = ((char)Math.Floor(((exponent.ToDouble() - 3) / 3) % 26 + 97)).ToString();

                    if(exponent.ToDouble() / 3 >= 27)
                    {
                        var letterTwo = ((char)Math.Floor(((exponent.ToDouble() - 3 * 26) / ( 3 * 26)) % 26 + 97)).ToString();
                        return (value / Pow(10, exponent)).ToString(stringFormat) + letterTwo + letterOne;
                    }
                    if(value > 1000)
                        return (value / Pow(10, exponent)).ToString(stringFormat) + letterOne;
                    return value.ToString(stringFormat);
                }
        }
        

        return "";
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

        data.coinsPerSecond_CurrentCPS = TotalCPS();
        data.coinsPerClick_CurrentCPC = TotalCPC();

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
        temp += ((data.standard_Upgrade_lvl_1 * data.cpc_Upgrade_1_base_amount) * data.production_multiplier);
        temp += ((data.standard_Upgrade_lvl_8 * data.cpc_Upgrade_2_base_amount) * data.production_multiplier);
        temp *= BigDouble.Pow(1.05, productionUpgradeManager.levels[2]);
        Debug.Log(temp + 1);
        return temp;
    }

    public BigDouble TotalCPS()
    {
        var temp = (data.standard_Upgrade_lvl_1 * data.coinsPerSecond_Amount) + data.cpc_Upgrade_1_base_amount;
        temp *= data.production_multiplier;
        temp *= BigDouble.Pow(1.1, productionUpgradeManager.levels[3]);
        return temp;
    }

    public void Update()
    {
        RunAcheivements();
        if (standard_upgrade_progressionTracker < 15) UnlockProgression();
        if(production_upgrade_progressionTracker < 10)ProductionUnlockProgression();
        productionUpgradeManager.Run();
        standardUpgradeManager.Run();

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


    public void InstantiateCriticalClick(float multiplier)
    {
        var spawn = Instantiate(crit, critSpawn.transform);
        spawn.GetComponentInChildren<CriticalClick>().Play();
        spawn.GetComponentInChildren<CriticalClick>().ChangeText(multiplier);
    }

    public void MainButton_Click()
    {
        data.currency += TotalCPC();
        data.totalCurrency += TotalCPC();
        if(data.standard_Upgrade_lvl_3 > 0)
        {
            var critical = new System.Random().Next(1, 1000);
            if (critical > 1000 - data.standard_Upgrade_lvl_3)
            {
                float criticalMultiplierBase = UnityEngine.Random.Range(1.0f, 50.0f); 
                if(data.standard_Upgrade_lvl_4 > 0)
                {
                    criticalMultiplierBase *= 1.5f * data.standard_Upgrade_lvl_4;
                    data.currency += TotalCPC() * criticalMultiplierBase;
                }
                else data.currency += TotalCPC() * criticalMultiplierBase;

                InstantiateCriticalClick(criticalMultiplierBase);
            }
        }
        
        //buttonParticles_script.ButtonClick();
    }


    public void FullReset()
    {
        data.FullReset();
    }

}
