using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeNumbers : MonoBehaviour
{
    Selling sellingScript;

    public Text text_Minta;
    public GameObject IncomeHolder;

    bool isOn;
    int itemLvl_2;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();

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
        switch (itemLvl_2)
        {
            case 1: amount = amount * 1.25f; break;
            case 2: amount = amount * 1.5f; break;
            case 3: amount = amount * 1.75f; break;
            case 4: amount = amount * 2f; break;
            case 5: amount = amount * 2.25f; break;
        }
        Text txt = Instantiate(text_Minta) as Text;
        txt.text = sellingScript.convertCurrencyToDisplay(amount.ToString());
        txt.transform.position = position;
        txt.transform.SetParent(IncomeHolder.transform);

        Destroy(txt.transform.gameObject, 1f);
    }
}
