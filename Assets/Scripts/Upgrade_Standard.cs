using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using TMPro;
using System;

public class Upgrade_Standard : MonoBehaviour
{
    public GameController gameController;

    public TextMeshProUGUI[] costText = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[15];
    public TextMeshProUGUI[] upgradeDescText = new TextMeshProUGUI[15];

    public Image[] costBars = new Image[15];
    public Image[] costBarSmooth = new Image[15];


    public string[] upgradeDesc;

    public BigDouble[] costs;
    public int[] levels;

    public BigDouble[] basePrice;
    public float[] changeInPrice;

    private BigDouble cost_1 => basePrice[0] * BigDouble.Pow(changeInPrice[0], gameController.data.standard_Upgrade_lvl_1);
    private BigDouble cost_2 => basePrice[1] * BigDouble.Pow(changeInPrice[1], gameController.data.standard_Upgrade_lvl_2);
    private BigDouble cost_3 => basePrice[2] * BigDouble.Pow(changeInPrice[2], gameController.data.standard_Upgrade_lvl_3);
    private BigDouble cost_4 => basePrice[3] * BigDouble.Pow(changeInPrice[3], gameController.data.standard_Upgrade_lvl_4);
    private BigDouble cost_5 => basePrice[4] * BigDouble.Pow(changeInPrice[4], gameController.data.standard_Upgrade_lvl_5);
    private BigDouble cost_6 => basePrice[5] * BigDouble.Pow(changeInPrice[5], gameController.data.standard_Upgrade_lvl_6);
    private BigDouble cost_7 => basePrice[6] * BigDouble.Pow(changeInPrice[6], gameController.data.standard_Upgrade_lvl_7);
    private BigDouble cost_8 => basePrice[7] * BigDouble.Pow(changeInPrice[7], gameController.data.standard_Upgrade_lvl_8);
    private BigDouble cost_9 => basePrice[8] * BigDouble.Pow(changeInPrice[8], gameController.data.standard_Upgrade_lvl_9);
    private BigDouble cost_10 => basePrice[9] * BigDouble.Pow(changeInPrice[9], gameController.data.standard_Upgrade_lvl_10);
    private BigDouble cost_11 => basePrice[10] * BigDouble.Pow(changeInPrice[10], gameController.data.standard_Upgrade_lvl_11);
    private BigDouble cost_12 => basePrice[11] * BigDouble.Pow(changeInPrice[11], gameController.data.standard_Upgrade_lvl_12);
    private BigDouble cost_13 => basePrice[12] * BigDouble.Pow(changeInPrice[12], gameController.data.standard_Upgrade_lvl_13);
    private BigDouble cost_14 => basePrice[13] * BigDouble.Pow(changeInPrice[13], gameController.data.standard_Upgrade_lvl_14);
    private BigDouble cost_15 => basePrice[14] * BigDouble.Pow(changeInPrice[14], gameController.data.standard_Upgrade_lvl_15);


    public void ArrayManager()
    {
        var data = gameController.data;

        costs[0] = cost_1;
        costs[1] = cost_2;
        costs[2] = cost_3;
        costs[3] = cost_4;
        costs[4] = cost_5;
        costs[5] = cost_6;
        costs[6] = cost_7;
        costs[7] = cost_8;
        costs[8] = cost_9;
        costs[9] = cost_10;
        costs[10] = cost_11;
        costs[11] = cost_12;
        costs[12] = cost_13;
        costs[13] = cost_14;
        costs[14] = cost_15;

        levels[0] = data.standard_Upgrade_lvl_1;
        levels[1] = data.standard_Upgrade_lvl_2;
        levels[2] = data.standard_Upgrade_lvl_3;
        levels[3] = data.standard_Upgrade_lvl_4;
        levels[4] = data.standard_Upgrade_lvl_5;
        levels[5] = data.standard_Upgrade_lvl_6;
        levels[6] = data.standard_Upgrade_lvl_7;
        levels[7] = data.standard_Upgrade_lvl_8;
        levels[8] = data.standard_Upgrade_lvl_9;
        levels[9] = data.standard_Upgrade_lvl_10;
        levels[10] = data.standard_Upgrade_lvl_11;
        levels[11] = data.standard_Upgrade_lvl_12;
        levels[12] = data.standard_Upgrade_lvl_13;
        levels[13] = data.standard_Upgrade_lvl_14;
        levels[14] = data.standard_Upgrade_lvl_15;

    }

    public void StartStandardUpgrades()
    {

        costs = new BigDouble[5];
        levels = new int[5];
        upgradeDesc = new[]
        { 
            $"Increase coins per second by +{gameController.data.coinsPerSecond_Amount * gameController.data.production_multiplier}C/s",
            $"Increase coins per click by +{gameController.data.coinsPerClick_CPC_Amount * gameController.data.production_multiplier}",
            $"Increase critical click chance by +0.1%",
            $"Improve base critical click multiplier by +1.5x",
            $"Auto clicker, clicks every ????seconds",
            $"Increase chance of earning product on clicks",
            $"Increase product earned upon click",
            $"Increase coins per click by +????????????????",
            $"Increase coins per second by +?????????????????",
            $"Add another button, auto click applies",
            $"Increase both CPC and CPS by +??????",
            $"",
            $"",
            $"",
            $""
        };

    }

    public void BuyUpgrade(string UpgradeType)
    {
        var data = gameController.data;

        BigDouble basePriceOfUpgrade;
        BigDouble coins;
        float theChangeInPrice;
        int currentUpgradeLvl;

        int numberOfBuys;

        switch (UpgradeType)
        {
            #region Increase Coins Per Second 1---------------------------------------------------------------------------------------------------------------------------------------
            case "CPS_1_Upgrade_1_B1":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_1_Upgrade_1_B5":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_1_Upgrade_1_B10":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins Per Click 2-----------------------------------------------------------------------------------------------------------------------------------
            case "CPC_Upgrade_2_B1":
                {
                    basePriceOfUpgrade = basePrice[1];
                    theChangeInPrice = changeInPrice[1];
                    currentUpgradeLvl = levels[1];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_2, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_Upgrade_2_B5":
                {
                    basePriceOfUpgrade = basePrice[1];
                    theChangeInPrice = changeInPrice[1];
                    currentUpgradeLvl = levels[1];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_2, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_Upgrade_2_B10":
                {
                    basePriceOfUpgrade = basePrice[1];
                    theChangeInPrice = changeInPrice[1];
                    currentUpgradeLvl = levels[1];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_2, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Critical Click chance--------------------------------------------------------------------------------------------------------------------------------
            case "CriticalClick_Upgrade_3_B1":
                {
                    if (data.standard_Upgrade_lvl_3 >= 1000) return;
                    else
                    {
                        basePriceOfUpgrade = basePrice[2];
                        theChangeInPrice = changeInPrice[2];
                        currentUpgradeLvl = levels[2];
                        coins = data.currency;
                        //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                        numberOfBuys = 1;
                        var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));
                        Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                    
                }
                break;
            case "CriticalClick_Upgrade_3_B5":
                {
                    if (data.standard_Upgrade_lvl_3 > 995) return;
                    else
                    {
                        basePriceOfUpgrade = basePrice[2];
                        theChangeInPrice = changeInPrice[2];
                        currentUpgradeLvl = levels[2];
                        coins = data.currency;
                        //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                        numberOfBuys = 1;
                        var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));
                        Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "CriticalClick_Upgrade_3_B10":
                {
                    if (data.standard_Upgrade_lvl_3 > 990) return;
                    else
                    {
                        basePriceOfUpgrade = basePrice[2];
                        theChangeInPrice = changeInPrice[2];
                        currentUpgradeLvl = levels[2];
                        coins = data.currency;
                        //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                        numberOfBuys = 1;
                        var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));
                        Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            #endregion
            #region Base Critical Basic-----------------------------------------------------------------------------------------------------------------------
            case "CriticalBaseMulti_Upgrade_4_B1":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CriticalBaseMulti_Upgrade_4_B5":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CriticalBaseMulti_Upgrade_4_B10":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Auto Clicker----------------------------------------------------------------------------------------------------------------------
            case "AutoClicker_Upgrade_5_B1":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "AutoClicker_Upgrade_5_B5":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "AutoClicker_Upgrade_5_B10":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Chance to get currency on click----------------------------------------------------------------------------------------------------------------------
            case "currencyOnClick_Upgrade_6_B1":
                {
                    basePriceOfUpgrade = basePrice[5];
                    theChangeInPrice = changeInPrice[5];
                    currentUpgradeLvl = levels[5];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "currencyOnClick_Upgrade_6_B5":
                {
                    basePriceOfUpgrade = basePrice[5];
                    theChangeInPrice = changeInPrice[5];
                    currentUpgradeLvl = levels[5];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "currencyOnClick_Upgrade_6_B10":
                {
                    basePriceOfUpgrade = basePrice[5];
                    theChangeInPrice = changeInPrice[5];
                    currentUpgradeLvl = levels[5];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins per click 2----------------------------------------------------------------------------------------------------------------------
            case "CPC_2_Upgrade_7_B1":
                {
                    basePriceOfUpgrade = basePrice[6];
                    theChangeInPrice = changeInPrice[6];
                    currentUpgradeLvl = levels[6];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_2_Upgrade_7_B5":
                {
                    basePriceOfUpgrade = basePrice[6];
                    theChangeInPrice = changeInPrice[6];
                    currentUpgradeLvl = levels[6];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_2_Upgrade_7_B10":
                {
                    basePriceOfUpgrade = basePrice[6];
                    theChangeInPrice = changeInPrice[6];
                    currentUpgradeLvl = levels[6];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins per Second 2----------------------------------------------------------------------------------------------------------------------
            case "CPS_2_Upgrade_8_B1":
                {
                    basePriceOfUpgrade = basePrice[7];
                    theChangeInPrice = changeInPrice[7];
                    currentUpgradeLvl = levels[7];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_2_Upgrade_8_B5":
                {
                    basePriceOfUpgrade = basePrice[7];
                    theChangeInPrice = changeInPrice[7];
                    currentUpgradeLvl = levels[7];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_2_Upgrade_8_B10":
                {
                    basePriceOfUpgrade = basePrice[7];
                    theChangeInPrice = changeInPrice[7];
                    currentUpgradeLvl = levels[7];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Add another button----------------------------------------------------------------------------------------------------------------------
            case "AddButton_Upgrade_9_B1":
                {
                    basePriceOfUpgrade = basePrice[8];
                    theChangeInPrice = changeInPrice[8];
                    currentUpgradeLvl = levels[8];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "AddButton_Upgrade_9_B5":
                {
                    basePriceOfUpgrade = basePrice[8];
                    theChangeInPrice = changeInPrice[8];
                    currentUpgradeLvl = levels[8];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "AddButton_Upgrade_9_B10":
                {
                    basePriceOfUpgrade = basePrice[8];
                    theChangeInPrice = changeInPrice[8];
                    currentUpgradeLvl = levels[8];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region CPC and CPS Upgrade----------------------------------------------------------------------------------------------------------------------
            case "CPCandCPS_Upgrade_10_B1":
                {
                    basePriceOfUpgrade = basePrice[9];
                    theChangeInPrice = changeInPrice[9];
                    currentUpgradeLvl = levels[9];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPCandCPS_Upgrade_10_B5":
                {
                    basePriceOfUpgrade = basePrice[9];
                    theChangeInPrice = changeInPrice[9];
                    currentUpgradeLvl = levels[9];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPCandCPS_Upgrade_10_B10":
                {
                    basePriceOfUpgrade = basePrice[9];
                    theChangeInPrice = changeInPrice[9];
                    currentUpgradeLvl = levels[9];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region currency Per Click----------------------------------------------------------------------------------------------------------------------
            case "currencyPerClick_Upgrade_11_B1":
                {
                    basePriceOfUpgrade = basePrice[10];
                    theChangeInPrice = changeInPrice[10];
                    currentUpgradeLvl = levels[10];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_11, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "currencyPerClick_Upgrade_11_B5":
                {
                    basePriceOfUpgrade = basePrice[10];
                    theChangeInPrice = changeInPrice[10];
                    currentUpgradeLvl = levels[10];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_11, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "currencyPerClick_Upgrade_11_B10":
                {
                    basePriceOfUpgrade = basePrice[10];
                    theChangeInPrice = changeInPrice[10];
                    currentUpgradeLvl = levels[10];
                    coins = data.currency;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.standard_Upgrade_lvl_11, ref data.currency, theCost, numberOfBuys);
                }
                break;
                #endregion
        }




    }

    public void Run()
    {
        ArrayManager();
        UpdateUI();


        void UpdateUI()
        {
            for (int i = 0; i < costText.Length; i++)
            {
                costText[i].text = $"{gameController.UpdateNotation(costs[i], "F0")} coins";
                levelText[i].text = $"{levels[i]}";
                upgradeDescText[i].text = $"{upgradeDesc[i]}";
                //gameController.BigDoubleFillAmount(gameController.data.currency, costs[i], costBars[i]);
                gameController.BigDoubleFillAmount(gameController.data.currency, costs[i], costBarSmooth[i]);
                gameController.BigDoubleFillAmount(gameController.currencyTemp, costs[i], costBars[i]);
            }
        }
    }
     



    public void Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys)
    {

        if (currency < theCost) return;
        currency -= theCost;
        upgradeLevel += numberOfBuys;
    }
}

