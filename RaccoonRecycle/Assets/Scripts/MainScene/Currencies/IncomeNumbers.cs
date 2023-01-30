using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeNumbers : MonoBehaviour
{
    public Text text_Minta;
    public GameObject IncomeHolder;

    Selling sellingScript; //a currency-t kezelõ script

    bool isOn;

    int itemLvl_2;

    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense

        isOn = true;
    }

    // Update is called once per frame
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
            case 0: amount = amount; break;
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
