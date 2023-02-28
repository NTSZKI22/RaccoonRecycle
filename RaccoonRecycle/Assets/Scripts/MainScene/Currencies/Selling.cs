using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selling : MonoBehaviour
{
    DatabaseCommunication dataScript;
    FixData fixDataScript;

    float normalCurrency;
    float prestigeCurrency;
    float totalearnings;

    public Text ncDisplay;
    public Text pcDisplay;

    public float defaultValue;

    int gemCurrency;
    float normalCurrency_spent;
    float prestigeCurrency_spent;

    public Text gcDisplay;

    public int itemLvl_2;
    int itemLvl_3;

    public bool gotData;

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();
        gemCurrency = 0;
        gotData = false;

        defaultValue = fixDataScript.defaultValue;
    }

    void Update()
    {
        itemLvl_3 = dataScript.itemLvl_3;
        itemLvl_2 = dataScript.itemLvl_2;
        displayCurrency();
        if (gotData && dataScript.login)
        {
            giveData();
            
        } else if (dataScript.registrating)
        {
            giveData();
        }
    }

    public void giveData()
    {
        dataScript.loadCurreny(normalCurrency, prestigeCurrency, totalearnings, gemCurrency, normalCurrency_spent, prestigeCurrency_spent);
    }

    void addCurrency(float n)
    {
        normalCurrency += n;
        totalearnings += n;
    }

    void displayCurrency()
    {
        ncDisplay.text = convertCurrencyToDisplay(normalCurrency.ToString());
        pcDisplay.text = convertCurrencyToDisplay(prestigeCurrency.ToString());
        gcDisplay.text = convertCurrencyToDisplay(gemCurrency.ToString());
    }

    public void claimedAchievement(int reward)
    {
        gemCurrency += reward;
    }

    public void normalSelling()
    {
        addCurrency(defaultValue);
    }

    public void soldTrash(float amount)
    {
        addCurrency(amount* fixDataScript.gemshopValueMultiplier(itemLvl_2));
    }

    public void increaseMoney(float amount)
    {
        addCurrency(amount);
    }

    public bool isEnoughNormalCurrency(float n)
    {
        if (!(normalCurrency < n))
        {
            return true;
        }
        return false;
    }

    public bool isEnoughPrestigeCurrency(float n)
    {
        if ( !(prestigeCurrency < n) )
        {
            return true;
        }
        return false;
    }

    public bool isEnoughGemCurrency(float n)
    {
        if (!(gemCurrency < n))
        {
            return true; 
        }
        return false; 
    }

    public string convertCurrencyToDisplay(string nc) 
    {
        string[] endings = { "", "", "K", "M", "B", "T", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", "P", "Q", "R", "S" }; 
        if (nc.Contains(","))
        {
            if (nc.Split(",")[0].Length > 4)
            {
                switch (nc.Split(",")[0].Length % 3)
                {
                    case 0:
                        return nc.Substring(0, 3) + "." + nc.Substring(3, 1) + " " + endings[(nc.Split(",")[0].Length) / 3];
                    case 1: 
                        return nc.Substring(0, 1) + "." + nc.Substring(1, 2) + " " + endings[(nc.Split(",")[0].Length + 2) / 3];
                    case 2: 
                        return nc.Substring(0, 2) + "." + nc.Substring(2, 2) + " " + endings[(nc.Split(",")[0].Length + 2) / 3];
                    default: return nc; 
                }
            }
            else 
            {
                return nc.Split(",")[0];
            }
        }
        else if(nc.Contains("."))
        {
            if (nc.Split(".")[0].Length > 4)
            {
                switch (nc.Split(".")[0].Length % 3)
                {
                    case 0:
                        return nc.Substring(0, 3) + "." + nc.Substring(3, 1) + " " + endings[(nc.Split(".")[0].Length) / 3];
                    case 1:
                        return nc.Substring(0, 1) + "." + nc.Substring(1, 2) + " " + endings[(nc.Split(".")[0].Length + 2) / 3];
                    case 2: 
                        return nc.Substring(0, 2) + "." + nc.Substring(2, 2) + " " + endings[(nc.Split(".")[0].Length + 2) / 3];
                    default: return nc;
                }
            }
            else 
            {
                return nc.Split(".")[0]; 
            }
        }
        return nc;

    }

    public void boughtUpgradeNormal(float n)
    {
        normalCurrency -= n;
        normalCurrency_spent += n;
    }

    public void boughtUpgradePrestige(float n)
    {
        prestigeCurrency -= n;
        prestigeCurrency_spent += n;
    }

    public void boughtGemshop(int n)
    {
        gemCurrency -= n;
    }

    public void getCurrencieValues()
    {
        normalCurrency = dataScript.normalCurrency;
        prestigeCurrency = dataScript.prestigeCurrency;
        totalearnings = dataScript.totalEarnings;
        gemCurrency = dataScript.gemCurrency;
        normalCurrency_spent = dataScript.normalCurrency_spent;
        prestigeCurrency_spent = dataScript.prestigeCurrency_spent;
    }

    public float prestigeEarning()
    {
        return ( totalearnings / fixDataScript.prestigeDivide * fixDataScript.gemshopPrestigeMultiplier(itemLvl_3));
    }

    public void prestigeTasks()
    {
        prestigeCurrency += prestigeEarning();
        totalearnings = 0;
        normalCurrency = 0;
    }
}
