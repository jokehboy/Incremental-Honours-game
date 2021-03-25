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

    public TextMeshProUGUI[] costText = new TextMeshProUGUI[5];
    public TextMeshProUGUI[] levelText = new TextMeshProUGUI[5];
    public TextMeshProUGUI[] upgradeDescText = new TextMeshProUGUI[5];

    public Image[] costBars = new Image[5];
    //public Image[] costBarsBG = new Image[5];



    public string[] upgradeDesc;

    public BigDouble[] costs;
    public int[] levels;

    public BigDouble[] basePrice;
    public float[] changeInPrice;


    private BigDouble cost_1 => 100 * BigDouble.Pow(1.25, gameController.data.prestiege_upg_lvl1);
    private BigDouble cost_2 => 5000;
    private BigDouble cost_3 => 500 * BigDouble.Pow(1.5, gameController.data.prestiege_upg_lvl3);
    private BigDouble cost_4 => 1000 * BigDouble.Pow(1.75, gameController.data.prestiege_upg_lvl4);
    private BigDouble cost_5 => 2000 * BigDouble.Pow(2.00, gameController.data.prestiege_upg_lvl5);

    // Start is called before the first frame update
    public void StartProductionUpgrades()
    {
        
        costs = new BigDouble[5];
        levels = new int[5];
        upgradeDesc = new[] { "Passive product creation, +0.5 product per second","Buy product, +100 product","Clicks are 5% more effective", "Gain 10% more coins per second", "Production is +1.01x better" };
    }

    public void Run()
    {
        ArrayManager();
        UpdateUI();


        void UpdateUI()
        {
            for (int i = 0; i < costText.Length; i++)
            {
                if (i == 1) { costText[i].text = $"{costs[i]} coins";}
                else costText[i].text = $"{costs[i]} product";

                levelText[i].text = $"{levels[i]}";
                upgradeDescText[i].text = $"{upgradeDesc[i]}"; 
                if(i==1) gameController.BigDoubleFillAmount(gameController.data.currency, costs[i], costBars[i]);
                else gameController.BigDoubleFillAmount(gameController.data.product, costs[i], costBars[i]);
            }
        }
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
            #region Passive Product---------------------------------------------------------------------------------------------------------------------------------------
            case "Passive_Product_B1":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl1, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Passive_Product_B5":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl1, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Passive_Product_B10":
                {
                    basePriceOfUpgrade = basePrice[0];
                    theChangeInPrice = changeInPrice[0];
                    currentUpgradeLvl = levels[0];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl1, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Buy Product-----------------------------------------------------------------------------------------------------------------------------------
            case "Buy_Product_B1":
                {
                    if (data.currency < costs[1]) return;
                    data.currency -= costs[1];
                    data.product += 100;
                    data.prestiege_upg_lvl2++;
                }
                break;
            case "Buy_Product_B5":
                {
                    if (data.currency < costs[1]*5) return;
                    data.currency -= costs[1]*5;
                    data.product += 500;
                    data.prestiege_upg_lvl2+=5;
                }
                break;
            case "Buy_Product_B10":
                {
                    if (data.currency < costs[1] * 10) return;
                    data.currency -= costs[1] * 10;
                    data.product += 1000;
                    data.prestiege_upg_lvl2+=10;
                }
                break;
            #endregion
            #region Click Upgrade--------------------------------------------------------------------------------------------------------------------------------
            case "Click_Upgrade_B1":
                {
                    basePriceOfUpgrade = basePrice[2];
                    theChangeInPrice = changeInPrice[2];
                    currentUpgradeLvl = levels[2];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Click_Upgrade_B5":
                {
                    basePriceOfUpgrade = basePrice[2];
                    theChangeInPrice = changeInPrice[2];
                    currentUpgradeLvl = levels[2];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Click_Upgrade_B10":
                {
                    basePriceOfUpgrade = basePrice[2];
                    theChangeInPrice = changeInPrice[2];
                    currentUpgradeLvl = levels[2];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl3, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Coins per sec upgrade-----------------------------------------------------------------------------------------------------------------------
            case "CoinsPerSecond_Upgrade_B1":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "CoinsPerSecond_Upgrade_B5":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "CoinsPerSecond_Upgrade_B10":
                {
                    basePriceOfUpgrade = basePrice[3];
                    theChangeInPrice = changeInPrice[3];
                    currentUpgradeLvl = levels[3];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            #endregion
            #region Production Upgrade----------------------------------------------------------------------------------------------------------------------
            case "Production_Upgrade_B1":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 1;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Production_Upgrade_B5":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 5;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
                }
                break;
            case "Production_Upgrade_B10":
                {
                    basePriceOfUpgrade = basePrice[4];
                    theChangeInPrice = changeInPrice[4];
                    currentUpgradeLvl = levels[4];
                    coins = data.product;
                    //BigDouble n = Floor(Log(((coins * (changeInPrice - 1)) / (b * Pow(changeInPrice, currentUpgradeLvl))) + 1, changeInPrice));

                    numberOfBuys = 10;
                    var theCost = basePriceOfUpgrade * (Pow(theChangeInPrice, currentUpgradeLvl) * (Pow(theChangeInPrice, numberOfBuys) - 1) / (theChangeInPrice - 1));

                    Buy(ref data.prestiege_upg_lvl4, ref data.product, theCost, numberOfBuys);
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

        levels[0] = data.prestiege_upg_lvl1;
        levels[1] = data.prestiege_upg_lvl2;
        levels[2] = data.prestiege_upg_lvl3;
        levels[3] = data.prestiege_upg_lvl4;
        levels[4] = data.prestiege_upg_lvl5;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
