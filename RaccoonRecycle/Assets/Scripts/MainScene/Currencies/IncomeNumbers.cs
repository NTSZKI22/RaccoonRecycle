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

    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense

        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        
        Text txt = Instantiate(text_Minta) as Text;
        txt.text = sellingScript.convertCurrencyToDisplay(amount.ToString());
        txt.transform.position = position;
        txt.transform.SetParent(IncomeHolder.transform);

        Destroy(txt.transform.gameObject, 1f);
    }
}
