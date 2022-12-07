using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour //kezel mimndent, ami az upgrade gombok lenyomásakor történik
{
    Selling sellingScript; //a currency-t kezelõ script
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    float multiplier; //értékszámításokhoz a szorzó
    int trashType; //a szemét típusa 1-petbottle, 2-box, 3-glass, 4-battery

    int ValueCostLvl; //érték árának szintje
    int SpeedCostLvl; //sebesség árának szintje
    int FrequencyCostLvl; //gyakoriság árának szintje

    float ValueDefCost; //érték alap ára
    float SpeedDefCost; //gyorsaság alap ára
    float FrequencyDefCost; //gyakoriság alap ára

    float ValueCost; //érték ára - számított érték
    float SpeedCost; //gyorsaság ára - számított érték
    float FrequencyCost; //gyakoriság ára - számított érték

    public Button button_Value; //érték fejlesztéséhez tartozó gmb
    public Button button_Speed; //gyorsaság fejlesztéséhez tartozó gomb
    public Button button_Frequency; //gyakoriság fejlesztéséhez tartozó gomb

    public Text text_Value; //érték árának megjelenítéséhez használt szöveg
    public Text text_Speed; //gyorsaság megjelenítéséhez használt szöveg
    public Text text_Frequency; //gyakoriság megjelenítéséhez használt szöveg


    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

        //az aktuális objektum tag-je alapján(melyen a script van), meghatározza a trashtype értékét
        switch (gameObject.tag)
        {
            case "PetBottleU": trashType = 1; break;
            case "BoxU": trashType = 2; break;
            case "GlassU": trashType = 3; break;
            case "BatteryU": trashType = 4; break;
            default: trashType = 0; Debug.Log("TrashType 0!"); break; //ha a tag egyik opcióval se egyezik alap értékként 0-át állítunk valamint logoljuk
        }

        defaultStart(); //elindítja a defaultstart-ot

        //a feljebb megadott gombok 'gomb' komponensére click listener kerül, kattintáskor a megfelelõ kód fut le
        Button btn_UV = button_Value.GetComponent<Button>();
        btn_UV.onClick.AddListener(value);

        Button btn_US = button_Speed.GetComponent<Button>();
        btn_US.onClick.AddListener(speed);

        Button btn_UF = button_Frequency.GetComponent<Button>();
        btn_UF.onClick.AddListener(frequency);
    }

    // Update is called once per frame
    void Update()
    {
        calculateCost(); //elindítja a calculatecost-t
        displayCost(); //elindítja a displaycast-t
        toAble(); //elindítja a toAble-t
    }

    void defaultStart() //azon adatokat definiálja, melyeket nem válaszként várunk az adatbázisból
    {
        multiplier = 1.07f; // szorzó értéke 7%-os növekedés

        //trashtype értékétõl függõen lesznek az tulajdonságok alap árai beállítva
        switch (trashType) 
        {
            case 0: Debug.Log("Tag hiba"); break;
            case 1:
                ValueDefCost = 50;
                SpeedDefCost = 25;
                FrequencyDefCost = 15;
                break;
            case 2:
                ValueDefCost = 100;
                SpeedDefCost = 50;
                FrequencyDefCost = 30;
                break;
            case 3:
                ValueDefCost = 200;
                SpeedDefCost = 100;
                FrequencyDefCost = 60;
                break;
            case 4:
                ValueDefCost = 400;
                SpeedDefCost = 200;
                FrequencyDefCost = 120;
                break;
            default: Debug.Log("Default értékek");
                ValueDefCost = 40;
                SpeedDefCost = 20;
                FrequencyDefCost = 10;
                break;
        }
    }

    public void getLevels()  //a szintek értékét szerzi meg a datascript-bõl
    {
        //trashtype értéke alapján állítja be a value, speed, frequency értékeit
        switch (trashType)
        {
            case 0: Debug.Log("Tag hiba"); break;
            case 1:
                ValueCostLvl = dataScript.PB_valueLvl;
                SpeedCostLvl = dataScript.PB_speedLvl;
                FrequencyCostLvl = dataScript.PB_frequencyLvl;
                break;
            case 2:
                ValueCostLvl = dataScript.BX_valueLvl;
                SpeedCostLvl = dataScript.BX_speedLvl;
                FrequencyCostLvl = dataScript.BX_frequencyLvl;
                break;
            case 3:
                ValueCostLvl = dataScript.GL_valueLvl;
                SpeedCostLvl = dataScript.GL_speedLvl;
                FrequencyCostLvl = dataScript.GL_frequencyLvl;
                break;
            case 4:
                ValueCostLvl = dataScript.BY_valueLvl;
                SpeedCostLvl = dataScript.BY_speedLvl;
                FrequencyCostLvl = dataScript.BY_frequencyLvl;
                break;
            default:
                Debug.Log("Default szintek");
                ValueCostLvl = 0;
                SpeedCostLvl = 0;
                FrequencyCostLvl = 0;
                break;
        }
    }

    void toAble() //feladata meghatározni, hogy a gomb elérhetõ legyen e
    {
        //gomb elérhetõségét állítja, mely függ attól, hogy van-e elég pénze a feloldásra
        button_Value.interactable = sellingScript.isEnoughNormalCurrency(ValueCost);
        button_Speed.interactable = sellingScript.isEnoughNormalCurrency(SpeedCost);
        button_Frequency.interactable = sellingScript.isEnoughNormalCurrency(FrequencyCost);
    }

    void calculateCost() //feldata kiszámolni a fejleszthetõ tulajdonságok árait
    {
        //ár = alap érték * szorzó^szint
        ValueCost = ValueDefCost * Mathf.Pow(multiplier, ValueCostLvl);
        SpeedCost = SpeedDefCost * Mathf.Pow(multiplier, SpeedCostLvl);
        FrequencyCost = FrequencyDefCost * Mathf.Pow(multiplier, FrequencyCostLvl);
    }

    void displayCost() // feldata megjeleníteni a kiszámolt árakat
    {
        //az értékeket megjeleníthetõ fomába állítjuk, majd azt megkapja a text komponense
        text_Value.text = sellingScript.convertCurrencyToDisplay(ValueCost.ToString());
        text_Speed.text = sellingScript.convertCurrencyToDisplay(SpeedCost.ToString());
        text_Frequency.text = sellingScript.convertCurrencyToDisplay(FrequencyCost.ToString());
    }

    void value() //érték gomb lenyomásakor fut le
    {
        ValueCostLvl++; //az ár szintjét növeli eggyel
        sellingScript.boughtUpgradeNormal(ValueCost); //az árat levonja a normalcurrency egyenlegbõl

        dataScript.upgrade(trashType, "value"); //datascript upgrade-ját futtatja le, átadva neki a trashtype-ot és egy stringet (jelenleg: value)
    }

    void speed() //gyorsaság gomb lenyomásakor fut le
    {
        SpeedCostLvl++; //az ár szintjét növeli eggyel
        sellingScript.boughtUpgradeNormal(SpeedCost); //az árat levonja a normalcurrency egyenlegbõl

        dataScript.upgrade(trashType, "speed"); //datascript upgrade-ját futtatja le, átadva neki a trashtype-ot és egy stringet (jelenleg: speed)
    }

    void frequency()//gyakoriság gomb lenyomásakor fut le
    {
        FrequencyCostLvl++; //az ár szintjét növeli eggyel
        sellingScript.boughtUpgradeNormal(FrequencyCost); //az árat levonja a normalcurrency egyenlegbõl

        dataScript.upgrade(trashType, "frequency"); //datascript upgrade-ját futtatja le, átadva neki a trashtype-ot és egy stringet (jelenleg: frequency)
    }
}
