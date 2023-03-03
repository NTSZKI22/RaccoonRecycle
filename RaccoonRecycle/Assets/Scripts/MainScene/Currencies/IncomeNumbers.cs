using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeNumbers : MonoBehaviour
{
    Selling sellingScript;
    FixData fixDataScript;

    public Text text_Minta;
    public GameObject IncomeHolder;

    bool isOn;
    int itemLvl_2;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        isOn = true;
    }

    void Update()
    {
        itemLvl_2 = sellingScript.itemLvl_2;
        toAble();
    }

    public void toggleOn(bool choice)
    {
        isOn = choice;
    }

    void toAble()
    {
        IncomeHolder.SetActive(isOn);
    }

    public void showIncome(float amount, Vector2 position)
    {
        amount *= fixDataScript.gemshopValueMultiplier(itemLvl_2);
        Text txt = Instantiate(text_Minta) as Text;
        txt.text = sellingScript.convertCurrencyToDisplay(amount.ToString());
        txt.transform.position = position;
        txt.transform.SetParent(IncomeHolder.transform);
        Destroy(txt.transform.gameObject, 1f);
    }
}
