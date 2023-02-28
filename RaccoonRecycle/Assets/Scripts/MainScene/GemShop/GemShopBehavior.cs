using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemShopBehavior : MonoBehaviour
{
    Selling sellingScript;
    DatabaseCommunication dataScript;

    public Text text_Item_Text_1;
    public Text text_Item_Cost_1;
    public Text text_Item_Details_1;
    public Button button_Item_Buy_1;
    int itemLvl_1;
    int[] cost_1 = new int[] { 100, 225, 325 };
    string[] details_1 = new string[] { "15 min -> 30 min", "30 min -> 1 hr", "1 hr -> 1,5 hr" };

    public Text text_Item_Text_2;
    public Text text_Item_Cost_2;
    public Text text_Item_Details_2;
    public Button button_Item_Buy_2;
    int itemLvl_2;
    int[] cost_2 = new int[] { 85, 175, 225, 260, 340 };
    string[] details_2 = new string[] { "0 -> 25%", "25% -> 50%", "50% -> 75%", "75% -> 100%", "100% -> 125%" };

    public Text text_Item_Text_3;
    public Text text_Item_Cost_3;
    public Text text_Item_Details_3;
    public Button button_Item_Buy_3;
    int itemLvl_3;
    int[] cost_3 = new int[] { 155, 275 };
    string[] details_3 = new string[] { "0 -> 50%", "50% -> 100%"};

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        text_Item_Text_1.text = "Offline earning upgrade";
        Button btn_Buy_1 = button_Item_Buy_1.GetComponent<Button>();
        btn_Buy_1.onClick.AddListener(bought1);

        text_Item_Text_2.text = "Increase value by";
        Button btn_Buy_2 = button_Item_Buy_2.GetComponent<Button>();
        btn_Buy_2.onClick.AddListener(bought2);
        
        text_Item_Text_3.text = "Pestige earning booster";
        Button btn_Buy_3 = button_Item_Buy_3.GetComponent<Button>();
        btn_Buy_3.onClick.AddListener(bought3);

        itemLvl_1 = 0;
        itemLvl_2 = 0;
        itemLvl_3 = 0;
    }

    void Update()
    {
        showText();
        toAble();
    }

    public void getData()
    {
        itemLvl_1 = dataScript.itemLvl_1;
        itemLvl_2 = dataScript.itemLvl_2;
        itemLvl_3 = dataScript.itemLvl_3;
    }

    void showText()
    {
        if(itemLvl_1 == cost_1.Length){
            text_Item_Cost_1.text = "Maxed out";
            text_Item_Details_1.text = "1,5 hr";
        }else
        {
            text_Item_Cost_1.text = cost_1[itemLvl_1].ToString();
            text_Item_Details_1.text = details_1[itemLvl_1].ToString();
        }

        if (itemLvl_2 == cost_2.Length)
        {
            text_Item_Cost_2.text = "Maxed out";
            text_Item_Details_2.text = "125%";
        }
        else
        {
            text_Item_Cost_2.text = cost_2[itemLvl_2].ToString();
            text_Item_Details_2.text = details_2[itemLvl_2].ToString();
        }

        if (itemLvl_3 == cost_3.Length)
        {
            text_Item_Cost_3.text = "Maxed out";
            text_Item_Details_3.text = "100%";
        }
        else
        {
            text_Item_Cost_3.text = cost_3[itemLvl_3].ToString();
            text_Item_Details_3.text = details_3[itemLvl_3].ToString();
        }
    }

    void toAble()
    {
        if (itemLvl_1 < cost_1.Length)
        {
            button_Item_Buy_1.interactable = sellingScript.isEnoughGemCurrency(cost_1[itemLvl_1]);
        }
        else
        {
            button_Item_Buy_1.interactable = false;
        }
        if (itemLvl_2 < cost_2.Length)
        {
            button_Item_Buy_2.interactable = sellingScript.isEnoughGemCurrency(cost_2[itemLvl_2]);
        }
        else
        {
            button_Item_Buy_2.interactable = false;
        }
        if (itemLvl_3 < cost_3.Length)
        {
            button_Item_Buy_3.interactable = sellingScript.isEnoughGemCurrency(cost_3[itemLvl_3]);
        }
        else
        {
            button_Item_Buy_3.interactable = false;
        }

    }

    public void bought1()
    {
        sellingScript.boughtGemshop(cost_1[itemLvl_1]); 
        itemLvl_1++;
        dataScript.itemLvl_1 = itemLvl_1;
    }

    public void bought2()
    {
        sellingScript.boughtGemshop(cost_2[itemLvl_2]);
        itemLvl_2++;
        dataScript.itemLvl_2 = itemLvl_2;
    }

    public void bought3()
    {
        sellingScript.boughtGemshop(cost_3[itemLvl_3]);
        itemLvl_3++;
        dataScript.itemLvl_3 = itemLvl_3;
    }
}
