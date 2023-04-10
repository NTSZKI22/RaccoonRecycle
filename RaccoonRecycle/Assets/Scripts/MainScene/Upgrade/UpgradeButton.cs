using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    Selling sellingScript;
    DatabaseCommunication dataScript;
    FixData fixDataScript;

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

    public Text text_ValueLvl;
    public Text text_SpeedLvl;
    public Text text_FrequencyLvl;

    int maxLevel;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        switch (gameObject.tag)
        {
            case "PetBottleU": trashType = 1; break;
            case "BoxU": trashType = 2; break;
            case "GlassU": trashType = 3; break;
            case "BatteryU": trashType = 4; break;
            default: trashType = 0; Debug.Log("TrashType 0!"); break;
        }

        defaultStart();

        Button btn_UV = button_Value.GetComponent<Button>();
        btn_UV.onClick.AddListener(value);

        Button btn_US = button_Speed.GetComponent<Button>();
        btn_US.onClick.AddListener(speed);

        Button btn_UF = button_Frequency.GetComponent<Button>();
        btn_UF.onClick.AddListener(frequency);
    }

    void Update()
    {
        calculateCost();
        displayCost();
        displayLevel();
        toAble(); 
    }

    void defaultStart()
    {
        maxLevel = fixDataScript.maxLevel;
        multiplier = fixDataScript.multiplierPos + 0.03f;
        ValueDefCost = fixDataScript.giveUpgradeProperties(gameObject.tag, "Value");
        SpeedDefCost = fixDataScript.giveUpgradeProperties(gameObject.tag, "Speed");
        FrequencyDefCost = fixDataScript.giveUpgradeProperties(gameObject.tag, "Frequency");
    }

    public void getLevels()
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

    void toAble()
    {
        switch (ValueCostLvl <= maxLevel)
        {
            case true: button_Value.interactable = sellingScript.isEnoughNormalCurrency(ValueCost); break;
            case false: button_Value.interactable = false; break;
        }

        switch (SpeedCostLvl <= maxLevel)
        {
            case true: button_Speed.interactable = sellingScript.isEnoughNormalCurrency(SpeedCost); break;
            case false: button_Speed.interactable = false; break;
        }

        switch (FrequencyCostLvl <= maxLevel)
        {
            case true: button_Frequency.interactable = sellingScript.isEnoughNormalCurrency(FrequencyCost); break;
            case false: button_Frequency.interactable = false; break;
        }
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

    void displayLevel()
    {
        switch (ValueCostLvl <= maxLevel)
        {
            case true: text_ValueLvl.text = $"Lvl {ValueCostLvl}"; break;
            case false: text_ValueLvl.text = "Max level"; break;
        }
        
        switch (SpeedCostLvl <= maxLevel)
        {
            case true: text_SpeedLvl.text = $"Lvl {SpeedCostLvl}"; break;
            case false: text_SpeedLvl.text = "Max level"; break;
        }

        switch (FrequencyCostLvl <= maxLevel)
        {
            case true: text_FrequencyLvl.text = $"Lvl {FrequencyCostLvl}"; break;
            case false: text_FrequencyLvl.text = "Max level"; break;
        }
    }

    void value()
    {
        ValueCostLvl++;
        sellingScript.boughtUpgradeNormal(ValueCost);
        dataScript.upgrade(trashType, "value");
    }

    void speed()
    {
        SpeedCostLvl++;
        sellingScript.boughtUpgradeNormal(SpeedCost);
        dataScript.upgrade(trashType, "speed");
    }

    void frequency()
    {
        FrequencyCostLvl++;
        sellingScript.boughtUpgradeNormal(FrequencyCost);
        dataScript.upgrade(trashType, "frequency");
    }
}
