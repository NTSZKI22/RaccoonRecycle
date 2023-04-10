using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    DatabaseCommunication dataScript;
    Selling sellingScript;
    HolderBehavior holderScript;
    FixData fixDataScript;

    public Button button_UnlockPB;
    public Text text_UnlockPB;
    float cost_UnlockPB;

    public Button button_UnlockBX;
    public Text text_UnlockBX;
    float cost_UnlockBX;

    public Button button_UnlockGL;
    public Text text_UnlockGL;
    float cost_UnlockGL;

    public Button button_UnlockBY;
    public Text text_UnlockBY;
    float cost_UnlockBY;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        cost_UnlockPB = fixDataScript.cost_UnlockPB;
        cost_UnlockBX = fixDataScript.cost_UnlockBX;
        cost_UnlockGL = fixDataScript.cost_UnlockGL;
        cost_UnlockBY = fixDataScript.cost_UnlockBY;

        Button btn_UPB = button_UnlockPB.GetComponent<Button>();
        btn_UPB.onClick.AddListener(unlockPB);
        
        Button btn_UBX = button_UnlockBX.GetComponent<Button>();
        btn_UBX.onClick.AddListener(unlockBX);

        Button btn_UGL = button_UnlockGL.GetComponent<Button>();
        btn_UGL.onClick.AddListener(unlockGL);

        Button btn_UBY = button_UnlockBY.GetComponent<Button>();
        btn_UBY.onClick.AddListener(unlockBY);

        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        text_UnlockPB.text = sellingScript.convertCurrencyToDisplay(cost_UnlockPB.ToString());
        text_UnlockBX.text = sellingScript.convertCurrencyToDisplay(cost_UnlockBX.ToString());
        text_UnlockGL.text = sellingScript.convertCurrencyToDisplay(cost_UnlockGL.ToString());
        text_UnlockBY.text = sellingScript.convertCurrencyToDisplay(cost_UnlockBY.ToString());
    }

    void Update()
    {
       toAble();
    }

    void toAble()
    {
        button_UnlockPB.interactable = sellingScript.isEnoughNormalCurrency(cost_UnlockPB);
        button_UnlockBX.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockBX);
        button_UnlockGL.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockGL);
        button_UnlockBY.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockBY);
    }

    void unlockPB()
    {
        holderScript.petbottleUnlock();
        sellingScript.boughtUpgradeNormal(cost_UnlockPB);
        dataScript.refreshTrashStatus("PetBottle", true);
    }

    void unlockBX()
    {
        holderScript.boxUnlock();
        sellingScript.boughtUpgradePrestige(cost_UnlockBX);
        dataScript.refreshTrashStatus("Box", true);
    }

    void unlockGL()
    {
        holderScript.glassUnlock();
        sellingScript.boughtUpgradePrestige(cost_UnlockGL);
        dataScript.refreshTrashStatus("Glass", true);
    }

    void unlockBY()
    {
        holderScript.batteryUnlock();
        sellingScript.boughtUpgradePrestige(cost_UnlockBY);
        dataScript.refreshTrashStatus("Battery", true);
    }
}
