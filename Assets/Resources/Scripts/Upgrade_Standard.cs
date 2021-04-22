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

    public TextMeshProUGUI[] costText = new TextMeshProUGUI[11];
    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[11];
    public TextMeshProUGUI[] upgradeDescText = new TextMeshProUGUI[11];

    public Image[] costBars = new Image[11];
    public Image[] costBarSmooth = new Image[11];


    private string[] upgradeDesc = new string[11];

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

    }

    public void StartStandardUpgrades()
    {

        costs = new BigDouble[11];
        levels = new int[11];
    }

    


    public void BuyUpgrade(string UpgradeType)
    {
        var data = gameController.data;

        int numberOfBuys;

        switch (UpgradeType)
        {
            #region Increase Coins Per Second 1---------------------------------------------------------------------------------------------------------------------------------------
            case "CPS_1_Upgrade_1_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);
                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_1_Upgrade_1_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);
                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_1_Upgrade_1_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);
                    Buy(ref data.standard_Upgrade_lvl_1, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins Per Click 2-----------------------------------------------------------------------------------------------------------------------------------
            case "CPC_Upgrade_2_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);
                    Buy(ref data.standard_Upgrade_lvl_2, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_Upgrade_2_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);
                    Buy(ref data.standard_Upgrade_lvl_2, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_Upgrade_2_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);

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
                        numberOfBuys = 1;
                        var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1);
                        Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                    
                }
                break;
            case "CriticalClick_Upgrade_3_B5":
                {
                    if (data.standard_Upgrade_lvl_3 > 995) return;
                    else
                    {
                        numberOfBuys = 5;
                        var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1); Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "CriticalClick_Upgrade_3_B10":
                {
                    if (data.standard_Upgrade_lvl_3 > 990) return;
                    else
                    {
                        numberOfBuys = 10;
                        var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1); Buy(ref data.standard_Upgrade_lvl_3, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            #endregion
            #region Base Critical Basic-----------------------------------------------------------------------------------------------------------------------
            case "CriticalBaseMulti_Upgrade_4_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);
                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CriticalBaseMulti_Upgrade_4_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);
                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CriticalBaseMulti_Upgrade_4_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);

                    Buy(ref data.standard_Upgrade_lvl_4, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Auto Clicker----------------------------------------------------------------------------------------------------------------------
            case "AutoClicker_Upgrade_5_B1":
                {
                    if(data.standard_Upgrade_lvl_5 < 500)
                    {
                        numberOfBuys = 1;
                        var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                        Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "AutoClicker_Upgrade_5_B5":
                {
                    if (data.standard_Upgrade_lvl_5 <= 495)
                    {
                        numberOfBuys = 5;
                        var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                        Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "AutoClicker_Upgrade_5_B10":
                {
                    if (data.standard_Upgrade_lvl_5 <= 490)
                    {
                        numberOfBuys = 10;
                        var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                        Buy(ref data.standard_Upgrade_lvl_5, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            #endregion
            #region Chance to get product on click----------------------------------------------------------------------------------------------------------------------
            case "productOnClickChance_Upgrade_6_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);


                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "productOnClickChance_Upgrade_6_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);

                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "productOnClickChance_Upgrade_6_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);

                    Buy(ref data.standard_Upgrade_lvl_6, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Product amount on click----------------------------------------------------------------------------------------------------------------------
            case "productOnClick_Upgrade_7_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);


                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "productOnClick_Upgrade_7_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);

                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "productOnClick_Upgrade_7_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);

                    Buy(ref data.standard_Upgrade_lvl_7, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins per click 2----------------------------------------------------------------------------------------------------------------------
            case "CPC_2_Upgrade_8_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[7] * (Pow(changeInPrice[7], numberOfBuys) - 1) / (changeInPrice[7] - 1);

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_2_Upgrade_8_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[7] * (Pow(changeInPrice[7], numberOfBuys) - 1) / (changeInPrice[7] - 1);

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPC_2_Upgrade_8_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[7] * (Pow(changeInPrice[7], numberOfBuys) - 1) / (changeInPrice[7] - 1);

                    Buy(ref data.standard_Upgrade_lvl_8, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Coins per Second 2----------------------------------------------------------------------------------------------------------------------
            case "CPS_2_Upgrade_9_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[8] * (Pow(changeInPrice[8], numberOfBuys) - 1) / (changeInPrice[8] - 1);

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_2_Upgrade_9_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[8] * (Pow(changeInPrice[8], numberOfBuys) - 1) / (changeInPrice[8] - 1);

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPS_2_Upgrade_9_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[8] * (Pow(changeInPrice[8], numberOfBuys) - 1) / (changeInPrice[8] - 1);

                    Buy(ref data.standard_Upgrade_lvl_9, ref data.currency, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Add another button----------------------------------------------------------------------------------------------------------------------
            case "AddButton_Upgrade_10_B1":
                {
                    if (data.standard_Upgrade_lvl_10 < 4)
                    {
                        numberOfBuys = 1;
                        var theCost = costs[9] * (Pow(changeInPrice[9], numberOfBuys) - 1) / (changeInPrice[9] - 1);

                        Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "AddButton_Upgrade_10_B2":
                {
                    if (data.standard_Upgrade_lvl_10 < 2)
                    {
                        numberOfBuys = 2;
                        var theCost = costs[9] * (Pow(changeInPrice[9], numberOfBuys) - 1) / (changeInPrice[9] - 1);
                        Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            case "AddButton_Upgrade_10_B4":
                {
                    if (data.standard_Upgrade_lvl_10 < 0)
                    {
                        numberOfBuys = 4;
                        var theCost = costs[9] * (Pow(changeInPrice[9], numberOfBuys) - 1) / (changeInPrice[9] - 1);
                        Buy(ref data.standard_Upgrade_lvl_10, ref data.currency, theCost, numberOfBuys);
                    }
                }
                break;
            #endregion
            #region CPC and CPS Upgrade----------------------------------------------------------------------------------------------------------------------
            case "CPCandCPS_Upgrade_11_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[10] * (Pow(changeInPrice[10], numberOfBuys) - 1) / (changeInPrice[10] - 1);
                    Buy(ref data.standard_Upgrade_lvl_11, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPCandCPS_Upgrade_11_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[10] * (Pow(changeInPrice[10], numberOfBuys) - 1) / (changeInPrice[10] - 1);
                    Buy(ref data.standard_Upgrade_lvl_11, ref data.currency, theCost, numberOfBuys);
                }
                break;
            case "CPCandCPS_Upgrade_11_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[10] * (Pow(changeInPrice[10], numberOfBuys) - 1) / (changeInPrice[10] - 1);
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


    }
    public void UpdateUI()
    {
        upgradeDesc = new string[]
        {
            $"Increase coins per second by +{gameController.UpdateNotation((gameController.data.cps_Upgrade_1_base_amount * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5) + (0.1 * gameController.data.production_upg_lvl4))),"F3")}C/s",
            $"Increase coins per click by +{gameController.UpdateNotation(gameController.data.cpc_Upgrade_1_base_amount * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5) + (0.05 * gameController.data.production_upg_lvl3)),"F3")}",
            $"Increase critical click chance by +0.01%, every time you click there is a chace to gain a big multiplier!",
            $"Improve base critical click multiplier by +1.05x, bigger multipliers on a critical click!",
            $"Upgrade to reduce time by 0.02 seconds, clicks for you",
            $"Increase chance of earning product on click by +0.01%, there's a chance to earn product when you click.",
            $"Increase product earned upon click +{gameController.UpdateNotation(gameController.data.productionEarnedOnClick_Upgrade * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5)),"F3")}, increases the base amount of product earned on a product click.",
            $"Increase coins per click by +{gameController.UpdateNotation(gameController.data.cpc_Upgrade_2_base_amount * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5)+ (0.05 * gameController.data.production_upg_lvl3)),"F3")}",
            $"Increase coins per second by +{gameController.UpdateNotation(gameController.data.cps_Upgrade_2_base_amount * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5)+ (0.1 * gameController.data.production_upg_lvl4)),"F3")}C/s",
            $"Add another button, auto click applies. The auto clicker will click each button.",
            $"Increase both CPC and CPS by {gameController.UpdateNotation(gameController.data.cpsAndCpc_Upgrade_base_amount * (gameController.data.production_multiplier + (0.01 * gameController.data.production_upg_lvl5)),"F3")}"
        };

        for (int i = 0; i < costText.Length; i++)
        {
            costText[i].text = $"{gameController.UpdateNotation(costs[i], "F3")} coins";
            levelText[i].text = $"{levels[i]}";
            upgradeDescText[i].text = upgradeDesc[i];

            gameController.BigDoubleFillAmount(gameController.data.currency, costs[i], costBarSmooth[i]);
            gameController.BigDoubleFillAmount(gameController.currencyTemp, costs[i], costBars[i]);
        }
    }

    private void LateUpdate()
    {
        Run();
    }



    public void Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys)
    {

        if (currency < theCost) return;
        currency -= theCost;
        upgradeLevel += numberOfBuys;
    }
}

