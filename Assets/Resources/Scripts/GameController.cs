using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using UnityEngine.Analytics;

 



public class GameController : MonoBehaviour
{
    public PlayerData data;
    public BigDouble currencyTemp = 0;

    public TextMeshProUGUI currencyText; //A visual of the users currency;

    public TextMeshProUGUI coinsPerSecond_View_Text;
    public TextMeshProUGUI coinsPerClick_View_Text;

    public GameObject crit;
    public GameObject critSpawn;

    public GameObject productGain;
    public GameObject productGain_spawn;

    public GameObject onClickAnimation;

    public TextMeshProUGUI production_Upgrade_Text;
    public TextMeshProUGUI production_Level_Text;
    public TextMeshProUGUI production_View_Amount_Text;

    //Acheivements
    public GameObject acheivementScreen;
    public List<Acheivement> acheievmentList = new List<Acheivement>();

    public ProductionUpgrades productionUpgradeManager;
    public Upgrade_Standard standardUpgradeManager;

    [SerializeField]
    public OfflineManager offlineManager;
    public SettingsManager settings;
    public TabGroup tabManager;

    private bool counting = true;
    //Progression 
    public int standard_upgrade_progressionTracker = 0;
    [SerializeField]
    public Unlock_standard[] standard_upgrade_Progression_Array = new Unlock_standard[11];

    public int production_upgrade_progressionTracker = 0;
    [SerializeField]
    public Unlock_standard[] production_upgrade_Progression_Array = new Unlock_standard[7];
    [SerializeField]
    public GameObject[] buttonArray = new GameObject[4];

    //public int standard_upgrade_progressionTracker;

    public Dictionary<string, object> parameters = 
        new Dictionary<string, object>();

    private void Awake()
    {
        SaveSystem.LoadPlayer(ref data);
    }

    public TextMeshProUGUI timeAwayText;
    public TextMeshProUGUI coinsEarned;

    public DateTime currentTime;
    public DateTime oldTime;

    //public GameObject offlinePopup;

    public void LoadOffline()
    {

        if (data.offlineProgCheck)
        {
            var tempOfflineTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = DateTime.Now;

            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;

            var effiiciencyMultiplier = 0.05;
            if (data.production_upg_lvl7 > 0) effiiciencyMultiplier *= data.production_upg_lvl7;

            var offlineTime = rawTime * effiiciencyMultiplier; //5% efficiency  

            //offlinePopup.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for\n {timer:dd\\:hh\\:mm\\:ss} ";

            BigDouble offlineGain = TotalCPS() * offlineTime;
            data.currency += offlineGain;

            coinsEarned.text = $"{UpdateNotation(offlineGain, "F2")} coins earned at an efficiency of {effiiciencyMultiplier * 100}%";

        }
    }

    public void Start()
    {

        //Application.targetFrameRate = 60;

        foreach (var obj in acheivementScreen.GetComponentsInChildren<Acheivement>())
        {
            acheievmentList.Add(obj);
        }

        data.addButton_Upgrade = data.standard_Upgrade_lvl_10;

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
        productionUpgradeManager.StartProductionUpgrades();
        standardUpgradeManager.StartStandardUpgrades();
        settings.UpdateNotationText();

        LoadOffline();
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
        data.currency = 0.0;
        data.product = 0.0;
        data.standard_Upgrade_lvl_1 = 0;
        data.standard_Upgrade_lvl_2 = 0;
        data.standard_Upgrade_lvl_3 = 0;
        data.standard_Upgrade_lvl_4 = 0;
        data.standard_Upgrade_lvl_5 = 0;
        data.standard_Upgrade_lvl_6 = 0;
        data.standard_Upgrade_lvl_7 = 0;
        data.standard_Upgrade_lvl_8 = 0;
        data.standard_Upgrade_lvl_9 = 0;
        data.standard_Upgrade_lvl_10 = 1;
        data.standard_Upgrade_lvl_11 = 0;

        for (int i = 0; i < data.button_added.Length; i++)
        {
            data.button_added[i] = false;
        }
        data.button_added[0] = true;

        data.cpc_Upgrade_1_base_amount = 0.5;
        data.cpc_Upgrade_2_base_amount = 12.5;

        data.cps_Upgrade_1_base_amount = 0.2;
        data.cps_Upgrade_2_base_amount = 10;
      
        data.cpsAndCpc_Upgrade_base_amount = 15;
        
        data.productionEarnedOnClick_Upgrade = 50f;
        data.productEarnedPerSec_base_amount = 0.5f;
        
        data.production_upg_lvl1 = 0;
        data.production_upg_lvl2 = 0;
        data.production_upg_lvl3 = 0;
        data.production_upg_lvl4 = 0;
        data.production_upg_lvl5 = 0;
        data.production_upg_lvl6 = 1;
        data.production_upg_lvl7 = 0;
    }

    private void FixedUpdate()
    {
        //SaveSystem.SavePlayer(data);
    }

   

    public void UnlockProgression()
    {
        //data.standard_upgrade_Progression_Array[0].GetComponent<Unlock_standard>()
        if(standard_upgrade_progressionTracker < 11)
        {
            standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().active = true;

            if (standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().active && !standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
            {
                standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().Locked();
                standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().unlockText.text = $"{standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock} coins to unlock this upgrade.";
                BigDoubleFillAmount(data.currency, standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock, standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().progressionBar);
            }
            if (data.currency >= standard_upgrade_Progression_Array[standard_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock)
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
        

    }
    public void ProductionUnlockProgression()
    {
        //data.standard_upgrade_Progression_Array[0].GetComponent<Unlock_standard>()

        if(production_upgrade_progressionTracker < 7)
        {
            production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().active = true;

            if (production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().active && !production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().isUnlocked)
            {
                production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().Locked();
                production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().unlockText.text = $"{production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock} product to unlock this upgrade.";
                BigDoubleFillAmount(data.product, production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock, production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().progressionBar);
            }
            if (data.product >= production_upgrade_Progression_Array[production_upgrade_progressionTracker].GetComponent<Unlock_standard>().coinsNeededToUnlock)
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

    }

    public void AddButton()
    {
        for(int i = 0; i < data.button_added.Length; i++)
        {
            if(i < data.standard_Upgrade_lvl_10)
            {
                data.button_added[i] = true;
            }
            else
            {
                data.button_added[i] = false;
            }

            buttonArray[i].SetActive(data.button_added[i]);
        }
        

    }

    public void GameText_Upgrades_Update() //Set and update what text should appear on upgrade buttons
    {
        production_Level_Text.text = "Current Production level: " + Floor(data.production_level).ToString("F0");

        production_Upgrade_Text.text = "Reset progress, Expand production by:\n" + Floor(data.production_level_ToGet).ToString("F0") + " levels";

        //buyUpgradeMaxCount_CPC.text = "Buy Max (" + BuyUpgradeMaxCount_CPC() + ")";
        //buyUpgradeMaxCount_CPS.text = "Buy Max (" + BuyUpgradeMaxCount_CPS() + ")";
    }

    public void GameText_Information_Update() //Set and update what text should appear on constantly changing information
    {
        if(tabManager.selectedTab == tabManager.tabButtons[2])
        {
            currencyText.text = "Product: " + UpdateNotation(data.product, "F4");
            coinsPerClick_View_Text.text = "PPC: +" + UpdateNotation((data.productionEarnedOnClick_Upgrade * data.standard_Upgrade_lvl_7), "F3");
            coinsPerSecond_View_Text.text = "PPS: " + UpdateNotation((data.productEarnedPerSec_base_amount * data.production_upg_lvl2), "F3") + " P/s";
        }
        else
        {
            currencyText.text = "Coins: " + UpdateNotation(data.currency, "F4");
            coinsPerClick_View_Text.text = "CPC: +" + UpdateNotation(TotalCPC(), "F3");
            coinsPerSecond_View_Text.text = "CPS: " + UpdateNotation(TotalCPS(), "F3") + " C/s";
        }
        production_View_Amount_Text.text = "Production Boost: " + (data.production_multiplier + (0.01 * data.production_upg_lvl5)).ToString("F3") + "x";

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
                    var e = Floor(Log10(value));
                    var exponent = 3 * Floor(e / 3);
                    var letterOne = ((char)Math.Floor((exponent.ToDouble() - 3) / 3 % 26 + 97)).ToString();

                    if(exponent.ToDouble() / 3 >= 27)
                    {
                        var letterTwo = ((char)(Math.Floor((exponent.ToDouble() - 3 * 26) / ( 3 * 26)) % 26 + 97)).ToString();
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
    public void BuyUpgradeTypes()
    {
        
    
        var n = Floor(Log(((data.currency * (1.75 - 1)) / (2000 * Pow(1.75, data.production_level))) + 1, 1.75));
    
        var theCost = 2000 * (Pow(1.75, data.production_level) * (Pow(1.75, n) - 1) / (1.75 - 1));
    
        if (data.currency > theCost && n > 0)
        {
            data.production_level += n;
            SetvaluesToDefault();
        }
              
       

    }

    public void Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys)
    {

        if (currency < theCost) return;
        currency -= theCost;
        upgradeLevel += numberOfBuys;
    }

   /* public BigDouble BuyUpgradeMaxCount_CPS()
    {
        var b = 10;
        var coins = data.currency;
        var changeInPrice = 1.07;
        var currentUpgradeLvl = data.coinsPerSecond_Level;

        var n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

        return n;
    }
   */
    #endregion
    
    public BigDouble TotalCPC()
    {
        var temp = data.cpc_Upgrade_1_base_amount;
        if (data.standard_Upgrade_lvl_2 > 0) temp += ((data.standard_Upgrade_lvl_2 * data.cpc_Upgrade_1_base_amount));
        if (data.standard_Upgrade_lvl_8 > 0) temp += ((data.standard_Upgrade_lvl_8 * data.cpc_Upgrade_2_base_amount));
        if (data.standard_Upgrade_lvl_11 > 0) temp += ((data.standard_Upgrade_lvl_11 * data.cpsAndCpc_Upgrade_base_amount));

        var beegMulti = 0.0;
        if (data.production_upg_lvl5 > 0) beegMulti += 0.01 * data.production_upg_lvl5;
        if (data.production_upg_lvl3 > 0) beegMulti += 0.05 * data.production_upg_lvl3;
        temp *= (data.production_multiplier + beegMulti);
       
        return temp;
    }

    public BigDouble TotalCPS()
    {
        var temp = data.cps_Upgrade_1_base_amount;
        if(data.standard_Upgrade_lvl_1 > 0)temp += ((data.standard_Upgrade_lvl_1 * data.cps_Upgrade_1_base_amount));
        if (data.standard_Upgrade_lvl_9 > 0) temp += ((data.standard_Upgrade_lvl_9 * data.cps_Upgrade_2_base_amount));
        if (data.standard_Upgrade_lvl_11 > 0) temp += ((data.standard_Upgrade_lvl_11 * data.cpsAndCpc_Upgrade_base_amount));
  
        var beegMulti = 0.0;
        if (data.production_upg_lvl5 > 0) beegMulti += 0.01 * data.production_upg_lvl5;
        if (data.production_upg_lvl4 > 0) beegMulti += 0.1 * data.production_upg_lvl4;
        temp *= (data.production_multiplier + beegMulti);
        
        return temp;
    }

    private bool hasClicked = false;
    public void AutoClicker()
    {
        if(data.standard_Upgrade_lvl_5 > 0 && hasClicked == false)
        {
            StartCoroutine("AutoClick", data.autoClicker_Upgrade_speed - (0.02f * data.standard_Upgrade_lvl_5));
        }
    }

    IEnumerator AutoClick(float timeToWait)
    {
        hasClicked = true;
        MainButton_Click(false);
        Debug.Log("Clicked");
        yield return new WaitForSeconds(timeToWait);
        hasClicked = false;
    }

    private bool hasTicked = false;
    public void CoinTick()
    {
        if (!hasTicked)
        {
            StartCoroutine("CoinPerSecond");
        }
    }

    IEnumerator CoinPerSecond()
    {
        hasTicked = true;
        data.coinsPerSecond = TotalCPS();
        data.currency += data.coinsPerSecond;
        data.totalCurrency += data.coinsPerSecond;

        data.product += data.productEarnedPerSec_base_amount * data.production_upg_lvl2;
        data.totalProduct += (data.productEarnedPerSec_base_amount * data.production_upg_lvl2) * data.production_multiplier;
        yield return new WaitForSeconds(1f);
        hasTicked = false;
    }

    private bool sent = false;
    public void ActivateEvent()
    {
        if(!sent)
        {
            StartCoroutine("SendEvent");
        }
    }

    /*
     * public double total_spent_games;
    public double total_lost_games;
    public double total_earned_games;
    public double total_currency_overall;

    public int times_played_coinflip;
    public int times_played_spinwheel;
    public int times_played_dice;
     * 
     */
    IEnumerator SendEvent()
    {
        sent = true;
        AnalyticsResult analyticsResult = Analytics.CustomEvent("player_chance_game_stats", new Dictionary<string, object>
             {
                 {"total_spent", data.total_spent_games },
                 {"total_earned", data.total_earned_games},
                 {"tota_lost", data.total_lost_games},
                 {"times_played_total", (data.times_played_coinflip + data.times_played_spinwheel + data.times_played_dice )},
                 {"times_played_coinflip", data.times_played_coinflip},
                 {"times_played_spinwheel", data.times_played_spinwheel},
                 {"times_played_dice", data.times_played_dice},
                 {"total_currency_gained_in_game", data.totalCurrency}
             }
            );
        Debug.Log("analytical result: " + analyticsResult);
        yield return new WaitForSeconds(90f);
        sent = false;

    }
    public void Update()
    {
        RunAcheivements();
        if (standard_upgrade_progressionTracker < 11) UnlockProgression();
        if(production_upgrade_progressionTracker < 7) ProductionUnlockProgression();
        //productionUpgradeManager.Run();
        //standardUpgradeManager.Run();

        GameText_Upgrades_Update();
        GameText_Information_Update();

        SmoothNumber(ref currencyTemp, data.currency);
       
        data.production_level_ToGet = Floor(Log(((data.currency * (1.75 - 1)) / (2000 * Pow(1.75, data.production_level))) + 1, 1.75));
        data.production_multiplier = (data.production_level * 0.05) + 1;
        
        AddButton();
        CoinTick();
        ActivateEvent();
        AutoClicker();

        //data.coinsPerSecond_CurrentCPS = TotalCPS() ;
        //data.coinsPerClick_CurrentCPC = TotalCPC();

        saveTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(!(saveTimer >= 15))return;
        SaveSystem.SavePlayer(data);
        saveTimer = 0;

    }
    public float saveTimer;


    public void SaveOnClick()
    {
        SaveSystem.SavePlayer(data);
    }
    #region Acheivements----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static string[] AcheievementStrings => new string[] { "Current Coins", "Total Coins Collected", "Current Product", "Total Product"};
    private BigDouble[] AcheivementNumbers => new BigDouble[] {data.currency, data.totalCurrency, data.product, data.totalProduct};

    private void RunAcheivements()
    {
        UpdateAcheivements(AcheievementStrings[0], AcheivementNumbers[0], ref data.ach_lvl1, ref acheievmentList[0].fill, ref acheievmentList[0].titles, ref acheievmentList[0].progress, 10);
        UpdateAcheivements(AcheievementStrings[1], AcheivementNumbers[1], ref data.ach_lvl2, ref acheievmentList[1].fill, ref acheievmentList[1].titles, ref acheievmentList[1].progress, 10);
        UpdateAcheivements(AcheievementStrings[2], AcheivementNumbers[2], ref data.ach_lvl3, ref acheievmentList[2].fill, ref acheievmentList[2].titles, ref acheievmentList[2].progress, 5);
        UpdateAcheivements(AcheievementStrings[3], AcheivementNumbers[3], ref data.ach_lvl4, ref acheievmentList[3].fill, ref acheievmentList[3].titles, ref acheievmentList[3].progress, 5);
    } 

    private void UpdateAcheivements(string name, BigDouble number, ref BigDouble level, ref Image fill, ref TextMeshProUGUI title, ref TextMeshProUGUI progress, float power)
    {
        var cap = BigDouble.Pow(power, level);

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
        if(tempVar > actualVar & actualVar == 0)
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


    public void MainButton_Click(bool isHuman)
    {
        
        if(isHuman)
        {
            float critMultiForProduct = 1.0f;
            BigDouble clickTotal = 0.0;
            if (data.standard_Upgrade_lvl_3 > 0)
            {
                int critical = UnityEngine.Random.Range(1, 1000);
                if (critical > 1000 - data.standard_Upgrade_lvl_3)
                {
                    float criticalMultiplierBase = UnityEngine.Random.Range(1.0f, 50.0f);
                    if (data.standard_Upgrade_lvl_4 > 0)
                    {
                        criticalMultiplierBase *= (1.0f + (0.05f * data.standard_Upgrade_lvl_4));
                        data.currency += TotalCPC() * criticalMultiplierBase;
                        clickTotal = TotalCPC() * criticalMultiplierBase;
                    }
                    else
                    {
                        clickTotal = TotalCPC() * criticalMultiplierBase;
                        data.currency += TotalCPC() * criticalMultiplierBase;
                    }
                    InstantiateCriticalClick(criticalMultiplierBase, GameObject.FindGameObjectWithTag("MainPanel"));
                    critMultiForProduct = criticalMultiplierBase;
                }
                else
                {
                    clickTotal = TotalCPC();
                    data.currency += TotalCPC();
                    data.totalCurrency += TotalCPC();
                }
            }
            else
            {
                clickTotal = TotalCPC();
                data.currency += TotalCPC();
                data.totalCurrency += TotalCPC();
            }



            AnimateClick(clickTotal,GameObject.FindGameObjectWithTag("MainPanel"));

            ProductOnClick(critMultiForProduct);
        }
        else
        {
            data.currency += TotalCPC() * data.standard_Upgrade_lvl_10;
            data.totalCurrency += TotalCPC() * data.standard_Upgrade_lvl_10;
            AnimateAutoClick(TotalCPC() * data.standard_Upgrade_lvl_10);
            //Debug.Log($"Total {TotalCPC() * data.standard_Upgrade_lvl_10}");
        }
        //buttonParticles_script.ButtonClick();
    }

    public void AnimateClick(BigDouble total, GameObject parent)
    {
        //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 10;
        var spawn = Instantiate(onClickAnimation, Input.mousePosition, Quaternion.identity, parent.transform);
        spawn.GetComponentInChildren<OnClickAnim>().Play();
        spawn.GetComponentInChildren<TextMeshProUGUI>().text = $"+{UpdateNotation(total, "F3")}";
    }
    public void InstantiateCriticalClick(float multiplier, GameObject parent)
    {
        var spawn = Instantiate(crit, Input.mousePosition, Quaternion.identity, parent.transform);
        //spawn.transform.position = mousePos;
        spawn.GetComponentInChildren<CriticalClick>().Play();
        spawn.GetComponentInChildren<CriticalClick>().ChangeText(multiplier);
    }

    public void InstantiateProductClick(BigDouble productAmount, GameObject parent)
    {
        //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 10;
        var spawn = Instantiate(productGain, Input.mousePosition, Quaternion.identity, parent.transform);
        spawn.GetComponentInChildren<ProductGain>().Play();
        spawn.GetComponentInChildren<TextMeshProUGUI>().text = $"+{UpdateNotation(productAmount, "F3")}";
    } 

    public void AnimateAutoClick(BigDouble total)
    {
        var _canvas = GameObject.FindGameObjectWithTag("MainPanel");
        //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = 10;
        var spawn = Instantiate(onClickAnimation, critSpawn.transform.position, Quaternion.identity, _canvas.transform);
        spawn.GetComponentInChildren<OnClickAnim>().Play();
        spawn.GetComponentInChildren<TextMeshProUGUI>().text = $"+{UpdateNotation(total, "F3")}";
    }

    public void ProductOnClick(float multiplier)
    {
        if (data.standard_Upgrade_lvl_6 > 0)
        {
            var productChance = UnityEngine.Random.Range(1, 1000);
            if (productChance > 1000 - data.standard_Upgrade_lvl_6)
            {
                BigDouble productGained = data.productionEarnedOnClick_Upgrade;
                if (data.standard_Upgrade_lvl_7 > 0)
                {
                    productGained *= data.standard_Upgrade_lvl_7;
                    productGained *= data.production_multiplier;
                }
                productGained *= multiplier;
                data.product += productGained;
                data.totalProduct += productGained;

                InstantiateProductClick(productGained, GameObject.FindGameObjectWithTag("MainPanel"));
            }
        }
    }


    public void FullReset()
    {
        data.FullReset();
    }

}
