using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    Selling sellingScript;
    DatabaseCommunication dataScript;

    float multiplier;
    int trashType;

    int ValueCostLvl;
    int SpeedCostLvl;
    int FrequencyCostLvl;

    float ValueDefCost;
    float SpeedDefCost;
    float FrequencyDefCost;

    float ValueCost;
    float SpeedCost;
    float FrequencyCost;

    public Button button_Value;
    public Button button_Speed;
    public Button button_Frequency;

    public Text text_Value;
    public Text text_Speed;
    public Text text_Frequency;


    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        switch (gameObject.tag)
        {
            case "PetBottle": trashType = 1; break;
            case "Box": trashType = 2; break;
            case "Glass": trashType = 3; break;
            case "Battery": trashType = 4; break;
            default: trashType = 0; break;
        }

        defaultStart();
        getLevels();
        calculateCost();
        displayCost();

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
        calculateCost();
        displayCost();
        toAble();
    }

    void defaultStart()
    {
        multiplier = 1.07f;

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
            default: Debug.Log("Default �rt�kek");
                ValueDefCost = 40;
                SpeedDefCost = 20;
                FrequencyDefCost = 10;
                break;
        }
    }

    void getLevels()
    {
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

    void toAble() //feladata meghat�rozni, hogy a gomb el�rhet� legyen e
    {
        //gomb el�rhet�s�g�t �ll�tja, mely f�gg att�l, hogy van-e el�g p�nze a felold�sra
        button_Value.interactable = sellingScript.isEnoughNormalCurrency(ValueCost);
        button_Speed.interactable = sellingScript.isEnoughNormalCurrency(SpeedCost);
        button_Frequency.interactable = sellingScript.isEnoughNormalCurrency(FrequencyCost);
    }

    void calculateCost()
    {
        ValueCost = ValueDefCost * Mathf.Pow(multiplier, ValueCostLvl);
        SpeedCost = SpeedDefCost * Mathf.Pow(multiplier, SpeedCostLvl);
        FrequencyCost = FrequencyDefCost * Mathf.Pow(multiplier, FrequencyCostLvl);
    }

    void displayCost()
    {
        text_Value.text = sellingScript.convertCurrencyToDisplay(ValueCost.ToString());
        text_Speed.text = sellingScript.convertCurrencyToDisplay(SpeedCost.ToString());
        text_Frequency.text = sellingScript.convertCurrencyToDisplay(FrequencyCost.ToString());
    }

    void value()
    {
        ValueCostLvl++;
        sellingScript.boughtUpgradeNormal(ValueCost);

        switch (trashType)
        {
            case 0: Debug.Log("Tag hiba"); break;
            case 1: dataScript.pbValue(); break;
            case 2: dataScript.bxValue(); break;
            case 3: dataScript.glValue(); break;
            case 4: dataScript.byValue(); break;
            default: Debug.Log("Hiba"); break;
        }
    }

    void speed()
    {
        SpeedCostLvl++;
        sellingScript.boughtUpgradeNormal(SpeedCost);

        switch (trashType)
        {
            case 0: Debug.Log("Tag hiba"); break;
            case 1: dataScript.pbSpeed(); break;
            case 2: dataScript.bxSpeed(); break;
            case 3: dataScript.glSpeed(); break;
            case 4: dataScript.bySpeed(); break;
            default: Debug.Log("Hiba"); break;
        }
    }

    void frequency()
    {
        FrequencyCostLvl++;
        sellingScript.boughtUpgradeNormal(FrequencyCost);

        switch (trashType)
        {
            case 0: Debug.Log("Tag hiba"); break;
            case 1: dataScript.pbFrequency(); break;
            case 2: dataScript.bxFrequency(); break;
            case 3: dataScript.glFrequency(); break;
            case 4: dataScript.byFrequency(); break;
            default: Debug.Log("Hiba"); break;
        }
    }
}