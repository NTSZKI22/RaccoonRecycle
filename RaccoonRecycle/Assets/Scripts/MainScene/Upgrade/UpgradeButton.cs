using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    Selling sellingScript;
    DatabaseCommunication dataScript;

    float multiplier;

    public Button button_PB_Value;
    public Text text_PB_Value;
    int PB_ValueLCostLvl;
    float PB_ValueDefCost;
    float PBValCost;


    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        defaultStart();
        getPBLevels();

        PBValCost = PB_ValueDefCost * Mathf.Pow(multiplier, PB_ValueLCostLvl);

        text_PB_Value.text = sellingScript.convertCurrencyToDisplay(PBValCost.ToString());
        Button btn_UPBV = button_PB_Value.GetComponent<Button>();
        btn_UPBV.onClick.AddListener(PBValue);
    }

    // Update is called once per frame
    void Update()
    {
        toAble();

        PBValCost = PB_ValueDefCost * Mathf.Pow(multiplier, PB_ValueLCostLvl);
        text_PB_Value.text = sellingScript.convertCurrencyToDisplay(PBValCost.ToString());
    }

    void defaultStart()
    {
        multiplier = 1.07f;
        PB_ValueDefCost = 50;
        PB_ValueLCostLvl = 0;
    }

    void getPBLevels()
    {
        PB_ValueLCostLvl = dataScript.PB_valueLvl;
    }

    void toAble() //feladata meghatározni, hogy a gomb elérhetõ legyen e
    {
        //gomb elérhetõségét állítja, mely függ attól, hogy van-e elég pénze a feloldásra
        button_PB_Value.interactable = sellingScript.isEnoughNormalCurrency(PBValCost);
    }

    void PBValue()
    {
        dataScript.pbValue();
        sellingScript.boughtUpgradeNormal(PBValCost);
        PB_ValueLCostLvl++;
        text_PB_Value.text = sellingScript.convertCurrencyToDisplay(PBValCost.ToString());
    }

}
