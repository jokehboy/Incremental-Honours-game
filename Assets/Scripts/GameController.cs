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
    public BigDouble currency;  //The users currecny count
    public BigDouble totalCurrency;

    //public double coinsPerClick_UpgradeCost;
    public BigDouble coinsPerClick_CurrentCPC;
    public BigDouble coinsPerClick_CPC_Amount;
    public BigDouble coinsPerClick_Level;

    public BigDouble coinsPerSecond; //The Coins earned Per Second
    //public double coinsPerSecond_UpgradeCost; //The cost of the next upgrade
    public BigDouble coinsPerSecond_Level; //The level of CPS
    public BigDouble coinsPerSecond_Amount; //The amount that the CPS will go up on purchase
    public BigDouble coinsPerSecond_CurrentCPS; //The current amount of CPS

    public BigDouble production_level;
    public BigDouble production_multiplier;
    public BigDouble production_level_ToGet;

    public BigDouble ach_lvl1;
    public BigDouble ach_lvl2;


    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        currency = 0;
        totalCurrency = 0;
        coinsPerSecond_Level = 0;
        coinsPerSecond_Amount = 0.1;

        coinsPerClick_Level = 0;
        coinsPerClick_CPC_Amount = 0.5;

        production_level = 0;

        ach_lvl1 = 0;
        ach_lvl2 = 0;
            
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




    public bool counting = true;

    public ButtonParticles buttonParticles_script;

    public void Start()
    {
        Application.targetFrameRate = 60;

        foreach (var obj in acheivementScreen.GetComponentsInChildren<Acheivement>())
        {
            acheievmentList.Add(obj);
        }

        SaveSystem.LoadPlayer(ref data);

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


        buyUpgradeMaxCount_CPC.text = "Buy Max (" + BuyUpgradeMaxCount_CPC() + ")";
        buyUpgradeMaxCount_CPS.text = "Buy Max (" + BuyUpgradeMaxCount_CPS() + ")";
    }

    public void GameText_Information_Update() //Set and update what text should appear on constantly changing information
    {
        currencyText.text = "Coins: " + UpdateNotation(data.currency, "F3");
        coinsPerClick_View_Text.text = "CPC: +" + UpdateNotation(data.coinsPerClick_CurrentCPC, "F3");
        coinsPerSecond_View_Text.text = UpdateNotation(data.coinsPerSecond, "F3") + " coins/s";
        production_View_Amount_Text.text = "Production Boost: " + (data.production_multiplier).ToString("F3") + "x";
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

    public void BuyUpgradeTypes(string upgradeID)
    {

        switch (upgradeID)
        {
            case "CPS_Upgrade_1_Main":
                {
                    var theCost_CPS = 10 * Pow(1.07, data.coinsPerSecond_Level);

                    if (data.currency >= theCost_CPS)
                    {
                        data.coinsPerSecond_Level++;
                        data.currency -= theCost_CPS;
                    }
                }
                break;

            case "CPC_Upgrade_1_Main":
                {
                    var theCost_CPC = 10 * Pow(1.1, data.coinsPerClick_Level);
                    if (data.currency >= theCost_CPC)
                    {
                        data.coinsPerClick_Level++;
                        data.currency -= theCost_CPC;
                    }
                }
                break;

            case "CPC_Upgrade_1_BuyMax":
                {
                    var b = 10;
                    var coins = data.currency;
                    var changeInPrice = 1.1;
                    var currentUpgradeLvl = data.coinsPerClick_Level;

                    var n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    var theCost = b * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, n) - 1) / (changeInPrice - 1));

                    if (data.currency >= theCost)
                    {
                        data.coinsPerClick_Level += n;
                        data.currency -= theCost;

                    }
                }
                break;
            case "CPS_Upgrade_1_BuyMax":
                {
                    var b = 10;
                    var coins = data.currency;
                    var changeInPrice = 1.07;
                    var currentUpgradeLvl = data.coinsPerSecond_Level;

                    var n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    var theCost = b * (Pow(changeInPrice, currentUpgradeLvl) * (Pow(changeInPrice, n) - 1) / (changeInPrice - 1));

                    if (data.currency >= theCost)
                    {
                        data.coinsPerSecond_Level += n;
                        data.currency -= theCost;
                    }
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
        data.coinsPerClick_CurrentCPC = data.coinsPerClick_Level * data.coinsPerClick_CPC_Amount;

    }


    public void Update()
    {
        RunAcheivements();

        GameText_Upgrades_Update();
        GameText_Information_Update();

        SmoothNumber(ref currencyTemp, data.currency);
        BigDoubleFillAmount(data.currency, theCost_CPC, coinsPerClick_Bar_BG);
        BigDoubleFillAmount(currencyTemp, theCost_CPC, coinsPerClick_Bar);

        BigDoubleFillAmount(data.currency, theCost_CPS, coinsPerSecond_Bar_BG);
        BigDoubleFillAmount(currencyTemp, theCost_CPS, coinsPerSecond_Bar);



        data.production_level_ToGet = (150 * Sqrt(data.currency / 1e7));
        data.production_multiplier = (data.production_level * 0.05) + 1;

        data.coinsPerSecond_CurrentCPS = (data.coinsPerSecond_Level * data.coinsPerSecond_Amount) * data.production_multiplier;
        data.coinsPerClick_CurrentCPC = ((data.coinsPerClick_Level * data.coinsPerClick_CPC_Amount) * data.production_multiplier) + 0.5f;

        saveTimer += Time.deltaTime;

        if(!(saveTimer >= 15))return;
        SaveSystem.SavePlayer(data);
        saveTimer = 0;

    }

    public float saveTimer;

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
        if(a < 0.001)
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


            data.coinsPerSecond = data.coinsPerSecond_CurrentCPS;
            data.currency += data.coinsPerSecond;
            data.totalCurrency += data.coinsPerSecond;
            yield return new WaitForSeconds(timeBetween);
        }
    }


    public void MainButton_Click()
    {
        data.currency += data.coinsPerClick_CurrentCPC;
        data.totalCurrency += data.coinsPerClick_CurrentCPC;
        buttonParticles_script.ButtonClick();
    }


    public void FullReset()
    {
        data.FullReset();
    }

}
