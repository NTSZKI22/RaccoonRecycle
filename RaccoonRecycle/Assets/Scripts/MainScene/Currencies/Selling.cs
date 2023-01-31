using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selling : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script
    Properties propertiesScript; //az adott szemetek tulajdonságait kezelõ script

    float normalCurrency; //az alap currenciet tartalmazó változó
    float prestigeCurrency; //a prestige currenciet tartalmazó változó
    float totalearnings; //a játék kezdete vagy utolsó prestige óta szerzett normal currency

    public Text ncDisplay; //a megjelenítésre használt mezöt tartalmazó változó
    public Text pcDisplay; //megjelenítésre használt mezõt tartalmazó változó

    //egyenlõre nem fixek, teszteléshez:
    public float defaultValue; //a szemetek alapértelmezett értéke
    float multiplier; //a szemetek értékéhez használt szorzó

    int gemCurrency;
    float normalCurrency_spent;
    float prestigeCurrency_spent;

    public Text gcDisplay;

    public int itemLvl_2;
    int itemLvl_3;


    void Start() //a játék elindulásánál lefut
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense
        gemCurrency = 0;

        defaultValue = 10; //alapértelmezett érték beállítása
        multiplier = 1.05f; //a szorzó alap értékének beállítása 5%-os növekedés

    }

    void Update() //minden képfrissítésnél lefut
    {
        itemLvl_3 = dataScript.itemLvl_3;
        itemLvl_2 = dataScript.itemLvl_2;
        displayCurrency(); //meghívja a metódust
        dataScript.loadCurreny(normalCurrency, prestigeCurrency, totalearnings, gemCurrency, normalCurrency_spent, prestigeCurrency_spent);  //a normal, presstigecurrency és totalearnings értékeit visszaadja a datascript-nek
    }

    public void giveData()
    {
        dataScript.loadCurreny(normalCurrency, prestigeCurrency, totalearnings, gemCurrency, normalCurrency_spent, prestigeCurrency_spent);
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
        gcDisplay.text = convertCurrencyToDisplay(gemCurrency.ToString()); //pcdisplay étékét az átalakítottt prestigecurrency-vé állítja
    }

    //public methods: 

    public void claimedAchievement(int reward)
    {
        gemCurrency += reward;
    }

    public void normalSelling() //metódus, más scriptek és objektumok által is meghívható, mely a default seller által történõ eladáskor fut le
    {
        addCurrency(defaultValue); //a szemetek alap értékét átadva meghívjuk az addCurrency-t
    }

    public void soldTrash(float amount) //bármilyen fix szemét eladásakor hívódik, majd fut le, növelli a normalcurrency értékét
    {
        switch (itemLvl_2)
        {
            case 0: addCurrency(amount); break;
            case 1: addCurrency(amount*1.25f); break;
            case 2: addCurrency(amount*1.5f); break;
            case 3: addCurrency(amount*1.75f); break;
            case 4: addCurrency(amount*2f); break;
            case 5: addCurrency(amount*2.25f); break;
        }
    }

    public void increaseMoney(float amount) //bármilyen fix szemét eladásakor hívódik, majd fut le, növelli a normalcurrency értékét
    {
        addCurrency(amount);  //a megkapott mennyiséget átadva meghívjuk az addCurrency-t
    }

    public bool isEnoughNormalCurrency(float n) //metódus, meghívásával igaz v hamis értéket ad vissza arról, hogy van-e ellég normalcurrency a fejlesztésre
    {
        if (normalCurrency > n || normalCurrency == n) //ha több vagy egyenlõ
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

    public bool isEnoughGemCurrency(float n) //metódus, meghívásával igaz v hamis értéket ad vissza arról, hogy van-e ellég prestigecurrency a fejlesztésre
    {
        if (gemCurrency > n || gemCurrency == n) //ha több vagy egyenlõ
        {
            return true; //igaz
        }
        return false; //hamis
    }

    public string convertCurrencyToDisplay(string nc) //metódus, meghívásával megjeleníthetõ állapotba lehet alakítani a currency-k mennyiségét, kér egy string nc-t, mely a birtokolt currency szöveggé alakítva
    {
        string[] endings = { "", "", "K", "M", "B", "T", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", "P", "Q", "R", "S" }; //tömb, tartalmazza a rövidítéseket, melyek utalnak a birtokolt pénzmennyiség értékére
        if (nc.Contains(",")) //ha vesszõt tartalmaz az nc
        {
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
        else
        {
            if (nc.Split(".")[0].Length > 4) //tizedesvesszõnél elválasztva a 0. fele ha nagyobb mint 4
            {
                switch (nc.Split(".")[0].Length % 3) //az elsõ fél hosszának hárommal osztva adott maradéka alapján
                {
                    case 0: //ha a maradék 0
                        return nc.Substring(0, 3) + "." + nc.Substring(3, 1) + " " + endings[(nc.Split(".")[0].Length) / 3]; //visszaadott formátum
                    case 1: //ha a maradék 1
                        return nc.Substring(0, 1) + "." + nc.Substring(1, 2) + " " + endings[(nc.Split(".")[0].Length + 2) / 3]; //visszaadott formátum
                    case 2: //ha a maradék 2
                        return nc.Substring(0, 2) + "." + nc.Substring(2, 2) + " " + endings[(nc.Split(".")[0].Length + 2) / 3]; //visszaadott formátum
                    default: return nc; //alapértelmezett visszatérési érték
                }
            }
            else //tizedesvesszõnél elválasztva a 0. fele ha nem nagyobb mint 4
            {
                return nc.Split(".")[0]; //visszadaja az elválasztott elsõ felét
            }
        }
        
    }

    public void boughtUpgradeNormal(float n) //metódus, meghívva és átadva neki az n-t, levonja az n mennyiségét a normalcurrency-bõl
    {
        normalCurrency -= n; //normalcurrency-bõl kivonja az n értékét
        normalCurrency_spent += n;
    }

    public void boughtUpgradePrestige(float n) //metódus, meghívva és átadva neki az n-t, levonja az n mennyiségét a prestigecurrency-bõl
    {
        prestigeCurrency -= n; //pretigecurrency-bõl kivonja az n értékét
        prestigeCurrency_spent += n;
    }

    public void boughtGemshop(int n) //metódus, meghívva és átadva neki az n-t, levonja az n mennyiségét a prestigecurrency-bõl
    {
        gemCurrency -= n; //pretigecurrency-bõl kivonja az n értékét
    }

    public void getCurrencieValues() //metódus, feladata megszerezni a normal es prestige currency elmentett mennyiségét
    {
        //a változóknak megadja az adatbázisból megszerzett változók értékeit
        normalCurrency = dataScript.normalCurrency;
        prestigeCurrency = dataScript.prestigeCurrency;
        totalearnings = dataScript.totalEarnings;
        gemCurrency = dataScript.gemCurrency;
        normalCurrency_spent = dataScript.normalCurrency_spent;
        prestigeCurrency_spent = dataScript.prestigeCurrency_spent;
        Debug.Log("getcurrency " + gemCurrency);
    }

    public float prestigeEarning() //feladata visszaadni az aktuálisan megkapható prestigecurrency mennyiségét
    {
        switch (itemLvl_3)
        {
            case 1: return totalearnings / 100 * 1.5f;
            case 2: return totalearnings / 100 * 2;
            default: return totalearnings / 100;
        }
        return totalearnings / 100;

    }

    public void prestigeTasks() //feladata a prestige során elvégezendõ feladatokat végrehajtani
    {
        prestigeCurrency += prestigeEarning();
        totalearnings = 0;
        normalCurrency = 0;
    }
}
