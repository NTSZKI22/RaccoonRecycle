using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prestige : MonoBehaviour
{
    Selling sellingScript;
    HolderBehavior holderScript;

    DatabaseCommunication dataScript;

    public Button button_PrestigeConfirm;
    public GameObject PrestigeWindow;
    public Text text_PrestigeEarnings;

    float prestigeEarn;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); 
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); 
        holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>(); 
    }

    void Update()
    {
        prestigeEarn = sellingScript.prestigeEarning();
        text_PrestigeEarnings.text = sellingScript.convertCurrencyToDisplay(prestigeEarn.ToString());
    }

    public void prestige()
    {
        sellingScript.prestigeTasks();
        dataScript.prestigeTasks();
    }
}
