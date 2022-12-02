using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selling : MonoBehaviour
{
    DatabaseCommunication dataScript;
    Properties propertiesScript;

    float normalCurrency; //az alap currenciet tartalmazó változó
    float prestigeCurrency; //a prestige currenciet tartalmazó változó
    float totalearnings; //a játék kezdete vagy utolsó prestige óta szerzett normal currency

    public Text ncDisplay; //a megjelenítésre használt mezöt tartalmazó változó
    public Text pcDisplay; //megjelenítésre használt mezõt tartalmazó változó

    //egyenlõre nem fixek, teszteléshez:
    float defaultValue; //a szemetek alapértelmezett értéke
    float multiplier; //a szemetek értékéhez használt szorzó


    void Start() //a játék elindulásánál lefut
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        defaultValue = 10; //alapértelmezett érték beállítása
        multiplier = 1.05f;
        getCurrencieValues(); //meghívja a metódust
    }

    void Update() //minden képfrissítésnél lefut
    {
        displayCurrency(); //meghívja a metódust
    }

    void getCurrencieValues() //metódus, feladata megszerezni a normal es prestige currency elmentett mennyiségét
    {
        normalCurrency = dataScript.normalCurrency;
        prestigeCurrency = dataScript.prestigeCurrency;
        totalearnings = dataScript.totalEarnings;
    }

    void addCurrency(float n) //metódus, növeli a currencyk mennyiségét
    {
        normalCurrency += n; //a normalcurrency mennyiségét növeli az n értékével
        totalearnings += n; //a totalearnigs mennyiségét növeli az n értékével
    }

    void displayCurrency() //metódus, feladata a currencyk mennyiségének megjelenítése bizonyos objektumokon keresztül
    {
        ncDisplay.text = convertCurrencyToDisplay(normalCurrency.ToString()); //ncdisplay étékét az átalakítottt normalcurrency-vé állítja
        pcDisplay.text = convertCurrencyToDisplay(prestigeCurrency.ToString()); //pcdisplay étékét az átalakítottt prestigecurrency-vé állítja
    }


    //public methods: 

    public void normalSelling() //metódus, más scriptek és objektumok által is meghívható, mely a default seller által történõ eladáskor fut le
    {
        addCurrency(defaultValue); //a szemetek alap értékét átadva meghívjuk az addCurrency-t
    }

    public void soldTrashType(float amount) //petpalack, bármilyen fix szemét eladásakor hívódik, majd fut le, növelli a normalcurrency értékét
    {
        addCurrency(amount);
    }

    public bool isEnoughNormalCurrency(float n) //metódus, meghívásával igaz v hamis értéket ad vissza arról, hogy van-e ellég normalcurrency a fejlesztésre
    {
        if(normalCurrency > n || normalCurrency == n) //ha több vagy egyenlõ
        {
            return true; //igaz
        }
        return false; //hamis
    }

    public bool isEnoughPrestigeCurrency(float n) //metódus, meghívásával igaz v hamis értéket ad vissza arról, hogy van-e ellég prestigecurrency a fejlesztésre
    {
        if (prestigeCurrency > n || prestigeCurrency == n) //ha több vagy egyenlõ
        {
            return true; //igaz
        }
        return false; //hamis
    }

    public string convertCurrencyToDisplay(string nc) //metódus, meghívásával megjeleníthetõ állapotba lehet alakítani a currency-k mennyiségét, kér egy string nc-t, mely a birtokolt currency szöveggé alakítva
    {
        string[] endings = { "", "", "K", "M", "B", "T", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", "P", "Q", "R", "S" }; //tömb, tartalmazza a rövidítéseket, melyek utalnak a birtokolt pénzmennyiség értékére
        if (nc.Split(",")[0].Length > 4) //tizedesvesszõnél elválasztva a 0. fele ha nagyobb mint 4
        {
            switch (nc.Split(",")[0].Length % 3) //az elsõ fél hosszának hárommal osztva adott maradéka alapján
            {
                case 0: //ha a maradék 0
                    return nc.Substring(0, 3) + "." + nc.Substring(3, 1) + " " + endings[(nc.Split(",")[0].Length) / 3]; //visszaadott formátum
                case 1: //ha a maradék 1
                    return nc.Substring(0, 1) + "." + nc.Substring(1, 2) + " " + endings[(nc.Split(",")[0].Length + 2) / 3]; //visszaadott formátum
                case 2: //ha a maradék 2
                    return nc.Substring(0, 2) + "." + nc.Substring(2, 2) + " " + endings[(nc.Split(",")[0].Length + 2) / 3]; //visszaadott formátum
                default: return nc; //alapértelmezett visszatérési érték
            }
        }
        else //tizedesvesszõnél elválasztva a 0. fele ha nem nagyobb mint 4
        {
            return nc.Split(",")[0]; //visszadaja az elválasztott elsõ felét
        }
    }

    public void boughtUpgradeNormal(float n) //metódus, meghívva és átadva neki az n-t, levonja az n mennyiségét a normalcurrency-bõl
    {
        normalCurrency -= n; //normalcurrency-bõl kivonja az n értékét
    }

    public void boughtUpgradePrestige(float n) //metódus, meghívva és átadva neki az n-t, levonja az n mennyiségét a prestigecurrency-bõl
    {
        prestigeCurrency -= n; //pretigecurrency-bõl kivonja az n értékét
    }
}
