using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using TMPro;

public class ProductionUpgrades : MonoBehaviour
{
    public GameController gameController;

    public TextMeshProUGUI[] costText = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] upgradeDescText = new TextMeshProUGUI[7];

    public Image[] costBars = new Image[7];
  
    private string[] upgradeDesc;

    public BigDouble[] costs;
    public int[] levels;

    public BigDouble[] basePrice;
    public float[] changeInPrice;


    private BigDouble cost_1 => basePrice[0] * BigDouble.Pow(changeInPrice[0], levels[0]);
    private BigDouble cost_2 => basePrice[1] * BigDouble.Pow(changeInPrice[1], levels[1]);
    private BigDouble cost_3 => basePrice[2] * BigDouble.Pow(changeInPrice[2], levels[2]);
    private BigDouble cost_4 => basePrice[3] * BigDouble.Pow(changeInPrice[3], levels[3]);
    private BigDouble cost_5 => basePrice[4] * BigDouble.Pow(changeInPrice[4], levels[4]);
    private BigDouble cost_6 => basePrice[5] * BigDouble.Pow(changeInPrice[5], levels[5]);
    private BigDouble cost_7 => basePrice[6] * BigDouble.Pow(changeInPrice[6], levels[6]);



    // Start is called before the first frame update
    public void StartProductionUpgrades()
    {
        
        costs = new BigDouble[7];
        levels = new int[7];
        
    }

    public void Run()
    {
        ArrayManager();
        UpdateUI();

    }

    public void UpdateUI()
    {
        upgradeDesc = new[]
        {
            $"+100 product",
            $"+{gameController.UpdateNotation((gameController.data.productEarnedPerSec_base_amount * gameController.data.production_multiplier), "F3")} product per second",
            $"Clicks are 5% more effective",
            $"Gain 10% more coins per second",
            $"Production is +1.01x better",
            $"Increase bet size",
            $"Improve offline efficiency by 5%"
        };

        for (int i = 0; i < costText.Length; i++)
        {
            if (i == 0) { costText[i].text = $"{costs[i]} coins"; }
            else costText[i].text = $"{gameController.UpdateNotation(costs[i], "F3")} product";

            levelText[i].text = $"{levels[i]}";
            upgradeDescText[i].text = $"{upgradeDesc[i]}";
            if (i == 0) gameController.BigDoubleFillAmount(gameController.data.currency, costs[i], costBars[i]);
            else gameController.BigDoubleFillAmount(gameController.data.product, costs[i], costBars[i]);
        }
    }

    public void BuyUpgrade(string UpgradeType)
    {
        var data = gameController.data;

        int numberOfBuys;

        switch (UpgradeType)
        {
            #region Buy Product-----------------------------------------------------------------------------------------------------------------------------------
            case "Buy_Product_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);

                    if (data.currency < theCost) return;
                    else
                    {
                        data.currency -= theCost;
                        data.production_upg_lvl1 += numberOfBuys;
                        data.product += 100;
                        data.totalProduct += 100;
                    }
                    
                }
                break;
            case "Buy_Product_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);

                    if (data.currency < theCost) return;
                    else
                    {
                        data.currency -= theCost;
                        data.production_upg_lvl1 += numberOfBuys;
                        data.product += 500;
                        data.totalProduct += 500;
                    }
                }
                break;
            case "Buy_Product_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[0] * (Pow(changeInPrice[0], numberOfBuys) - 1) / (changeInPrice[0] - 1);

                    if (data.currency < theCost) return;
                    else
                    {
                        data.currency -= theCost;
                        data.production_upg_lvl1 += numberOfBuys;
                        data.product += 1000;
                        data.totalProduct += 1000;
                    }
                }
                break;
            #endregion
            #region Passive Product---------------------------------------------------------------------------------------------------------------------------------------
            case "Passive_Product_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);

                    Buy(ref data.production_upg_lvl2, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Passive_Product_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);

                    Buy(ref data.production_upg_lvl2, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Passive_Product_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[1] * (Pow(changeInPrice[1], numberOfBuys) - 1) / (changeInPrice[1] - 1);

                    Buy(ref data.production_upg_lvl2, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Click Upgrade--------------------------------------------------------------------------------------------------------------------------------
            case "Click_Upgrade_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1);

                    Buy(ref data.production_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Click_Upgrade_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1);

                    Buy(ref data.production_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Click_Upgrade_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[2] * (Pow(changeInPrice[2], numberOfBuys) - 1) / (changeInPrice[2] - 1);

                    Buy(ref data.production_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Coins per sec upgrade-----------------------------------------------------------------------------------------------------------------------
            case "CoinsPerSecond_Upgrade_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);

                    Buy(ref data.production_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "CoinsPerSecond_Upgrade_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);

                    Buy(ref data.production_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "CoinsPerSecond_Upgrade_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[3] * (Pow(changeInPrice[3], numberOfBuys) - 1) / (changeInPrice[3] - 1);

                    Buy(ref data.production_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Production Upgrade----------------------------------------------------------------------------------------------------------------------
            case "Production_Upgrade_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                    Buy(ref data.production_upg_lvl5, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Production_Upgrade_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                    Buy(ref data.production_upg_lvl5, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Production_Upgrade_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[4] * (Pow(changeInPrice[4], numberOfBuys) - 1) / (changeInPrice[4] - 1);

                    Buy(ref data.production_upg_lvl5, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Increase Max Bet----------------------------------------------------------------------------------------------------------------------
            case "MaxBet_Upgrade_B1":
                {
                    numberOfBuys = 1;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);

                    Buy(ref data.production_upg_lvl6, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "MaxBet_Upgrade_B5":
                {
                    numberOfBuys = 5;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);

                    Buy(ref data.production_upg_lvl6, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "MaxBet_Upgrade_B10":
                {
                    numberOfBuys = 10;
                    var theCost = costs[5] * (Pow(changeInPrice[5], numberOfBuys) - 1) / (changeInPrice[5] - 1);

                    Buy(ref data.production_upg_lvl6, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Offline Efficiency----------------------------------------------------------------------------------------------------------------------
            case "OfflineEfficiency_Upgrade_B1":
                {
                    if(data.production_upg_lvl7 < 20)
                    {
                        numberOfBuys = 1;
                        var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);

                        Buy(ref data.production_upg_lvl7, ref data.product, theCost, numberOfBuys);
                    }
                }
                break;
            case "OfflineEfficiency_Upgrade_B5":
                {
                    if (data.production_upg_lvl7 < 15)
                    {
                        numberOfBuys = 5;
                        var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);

                        Buy(ref data.production_upg_lvl7, ref data.product, theCost, numberOfBuys);
                    }
                }
                break;
            case "OfflineEfficiency_Upgrade_B10":
                {
                    if (data.production_upg_lvl7 < 10)
                    {
                        numberOfBuys = 10;
                        var theCost = costs[6] * (Pow(changeInPrice[6], numberOfBuys) - 1) / (changeInPrice[6] - 1);

                        Buy(ref data.production_upg_lvl7, ref data.product, theCost, numberOfBuys);
                    }
                }
                break;
            #endregion
         } 


    }

    public void Buy(ref int upgradeLevel, ref BigDouble currency, BigDouble theCost, int numberOfBuys )
    {
        
        if (currency < theCost) return;
        currency -= theCost;
        upgradeLevel += numberOfBuys;
    }

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
       
        

        levels[0] = data.production_upg_lvl1;
        levels[1] = data.production_upg_lvl2;
        levels[2] = data.production_upg_lvl3;
        levels[3] = data.production_upg_lvl4;
        levels[4] = data.production_upg_lvl5;
        levels[5] = data.production_upg_lvl6;
        levels[6] = data.production_upg_lvl7;

    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
}
