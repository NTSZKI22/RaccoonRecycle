using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prestige : MonoBehaviour
{
    Selling sellingScript; //a currency-t kezelõ script

    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    public Button button_PrestigeConfirm; //gomb, amire nyomva megerõsíti szándékát, prestigel
    public GameObject PrestigeWindow; //üzenetablak a prestige-el kapcsolatban
    public Text text_PrestigeEarnings; //szöveg, amin keresztül meg lesz jelenítve az aktuális prestige-gel kapható currency

    float prestigeEarn; //a prestige során kapható currency

    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

    }

    // Update is called once per frame
    void Update()
    {
        prestigeEarn = sellingScript.prestigeEarning(); //megszerzi a prestige earn értékét
        text_PrestigeEarnings.text = sellingScript.convertCurrencyToDisplay(prestigeEarn.ToString()); //megjeleníti a prestigeearnt konverzió után
    }

    public void prestige()  //feladata a kellõ értékek megváltoztatása
    {
        //a kellõ scriptekben megtörténik a feladatok lefutása
        sellingScript.prestigeTasks();
        dataScript.prestigeTasks();
    }
}
