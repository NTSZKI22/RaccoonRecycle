using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script
    Selling sellingScript; //a currency-t kezelõ script

    //a megjelnítésre használt különbözõ mezõk
    public Text text_Nc; //normalCurrency
    public Text text_Pc; //prestigeCurrency
    public Text text_Te; //total earnings -> játék kezdete, vagy utolsó prestige óta

    public Text text_PBValue; //petpalack értéke
    public Text text_PBEarnings; //petpalackkal szerzett összbevétel

    public Text text_BXValue; //box értéke
    public Text text_BXEarnings; //boxxal szerzett összbevétel

    public Text text_GLValue; //üveg értéke
    public Text text_GLEarnings; //üveggel szerzett összbevétel

    public Text text_BYValue; //battery értéke
    public Text text_BYEarnings; //batteryvel szerzett összbevétel

    float multiplier; //szorzó

    // Start is called before the first frame update
    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense

        multiplier = 1.07f; //szorzó alap értékét beállítja 7%-ps növekedés
    }

    // Update is called once per frame
    void Update()
    {
        displayData(); //elindítja a displaydata-t
    }

    void displayData() //feladata megjeleníteni az adatokat
    {
        //a szövegmezõk értékei a datascript-bõl kivett adatok, melyeket elõtte megjeleníthetõ formába alakítunk
        text_Nc.text = sellingScript.convertCurrencyToDisplay(dataScript.normalCurrency.ToString());
        text_Pc.text = sellingScript.convertCurrencyToDisplay(dataScript.prestigeCurrency.ToString());
        text_Te.text = sellingScript.convertCurrencyToDisplay(dataScript.totalEarnings.ToString());

        text_PBValue.text = sellingScript.convertCurrencyToDisplay((25 * Mathf.Pow(multiplier, dataScript.PB_valueLvl)).ToString());
        text_PBEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.PB_soldAmount.ToString());

        text_BXValue.text = sellingScript.convertCurrencyToDisplay((50 * Mathf.Pow(multiplier, dataScript.BX_valueLvl)).ToString());
        text_BXEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.BX_soldAmount.ToString());

        text_GLValue.text = sellingScript.convertCurrencyToDisplay((100 * Mathf.Pow(multiplier, dataScript.GL_valueLvl)).ToString());
        text_GLEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.GL_soldAmount.ToString());

        text_BYValue.text = sellingScript.convertCurrencyToDisplay((200 * Mathf.Pow(multiplier, dataScript.BY_valueLvl)).ToString());
        text_BYEarnings.text = sellingScript.convertCurrencyToDisplay(dataScript.BY_soldAmount.ToString());
    }
}
