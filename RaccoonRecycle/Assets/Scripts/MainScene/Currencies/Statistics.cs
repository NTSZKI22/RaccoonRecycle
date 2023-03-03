using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    DatabaseCommunication dataScript;
    Selling sellingScript;
    FixData fixDataScript;

    public Text text_Nc; 
    public Text text_Pc; 
    public Text text_Te; 

    public Text text_PBValue; 
    public Text text_PBEarnings; 

    public Text text_BXValue; 
    public Text text_BXEarnings; 

    public Text text_GLValue; 
    public Text text_GLEarnings; 

    public Text text_BYValue; 
    public Text text_BYEarnings; 

    float multiplier; 

    int itemLvl_2;

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        multiplier = fixDataScript.multiplierPos;
    }

    void Update()
    {
        displayData();
        itemLvl_2 = dataScript.itemLvl_2;
    }

    void displayData()
    {
        float szorzo = fixDataScript.gemshopValueMultiplier(itemLvl_2);

        text_Nc.text = sellingScript.convertCurrencyToDisplay(dataScript.normalCurrency.ToString());
        text_Pc.text = sellingScript.convertCurrencyToDisplay(dataScript.prestigeCurrency.ToString());
        text_Te.text = sellingScript.convertCurrencyToDisplay(dataScript.totalEarnings.ToString());

        text_PBValue.text = sellingScript.convertCurrencyToDisplay((fixDataScript.PB_defValue * Mathf.Pow(multiplier, dataScript.PB_valueLvl) * szorzo).ToString());
        text_PBEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.PB_soldAmount.ToString());

        text_BXValue.text = sellingScript.convertCurrencyToDisplay((fixDataScript.BX_defValue * Mathf.Pow(multiplier, dataScript.BX_valueLvl) * szorzo).ToString());
        text_BXEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.BX_soldAmount.ToString());

        text_GLValue.text = sellingScript.convertCurrencyToDisplay((fixDataScript.GL_defValue * Mathf.Pow(multiplier, dataScript.GL_valueLvl) * szorzo).ToString());
        text_GLEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.GL_soldAmount.ToString());

        text_BYValue.text = sellingScript.convertCurrencyToDisplay((fixDataScript.BY_defValue * Mathf.Pow(multiplier, dataScript.BY_valueLvl) * szorzo).ToString());
        text_BYEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.BY_soldAmount.ToString());
    }
}
