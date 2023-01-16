using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    //használt scriptek változói
    Selling sellingScript; //a currency-t kezelõ script
    HolderBehavior holderScript; //a holderek viselkedését kezelõ script
    GettingProgress progressScript; // a feloldott haladást jelzi vissza

    //minden szemétfajtához tartozó gomb, felirat és ár
    //gomb -> megnyomásával feloldható a futószalag, felirat -> kiírja az árat, cost -> megadja az árat
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

    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>(); //a scriptet kiveszi az adott objektumból mint komponense
        progressScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<GettingProgress>(); //a scriptet kiveszi az adott objektumból mint komponense

        defaultStart(); //alapértelmezett elindulás

        //a feljebb megadott gombok 'gomb' komponensére click listener kerül, kattintáskor a megfelelõ kód fut le
        Button btn_UPB = button_UnlockPB.GetComponent<Button>();
        btn_UPB.onClick.AddListener(unlockPB);
        
        Button btn_UBX = button_UnlockBX.GetComponent<Button>();
        btn_UBX.onClick.AddListener(unlockBX);

        Button btn_UGL = button_UnlockGL.GetComponent<Button>();
        btn_UGL.onClick.AddListener(unlockGL);

        Button btn_UBY = button_UnlockBY.GetComponent<Button>();
        btn_UBY.onClick.AddListener(unlockBY);

        //a feliratok tartalmát az árra változtatja, mely elõtte konverzión esik át
        text_UnlockPB.text = sellingScript.convertCurrencyToDisplay(cost_UnlockPB.ToString());
        text_UnlockBX.text = sellingScript.convertCurrencyToDisplay(cost_UnlockBX.ToString());
        text_UnlockGL.text = sellingScript.convertCurrencyToDisplay(cost_UnlockGL.ToString());
        text_UnlockBY.text = sellingScript.convertCurrencyToDisplay(cost_UnlockBY.ToString());
    }

    // Update is called once per frame
    void Update()
    {
       toAble(); //gombok használhatósága
    }

    void defaultStart() //alap értékállítás bizonyos változóknak
    {
        cost_UnlockPB = 50;
        cost_UnlockBX = 10000;
        cost_UnlockGL = 150000;
        cost_UnlockBY = 2000000;
    }

    void toAble() //feladata meghatározni, hogy a gomb elérhetõ legyen e
    {
        //gomb elérhetõségét állítja, mely függ attól, hogy van-e elég pénze a feloldásra
        button_UnlockPB.interactable = sellingScript.isEnoughNormalCurrency(cost_UnlockPB);
        button_UnlockBX.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockBX);
        button_UnlockGL.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockGL);
        button_UnlockBY.interactable = sellingScript.isEnoughPrestigeCurrency(cost_UnlockBY);
    }

    void unlockPB() //petpalack feloldása
    {
        holderScript.petbottleUnlock(); //a szügséges objektumok láthatósága változik
        sellingScript.boughtUpgradeNormal(cost_UnlockPB); //a feloldás ára levonásra kerül az egyenlegrõl
        progressScript.ProgressSet(1);
    }

    void unlockBX() //doboz feloldása
    {
        holderScript.boxUnlock(); //a szügséges objektumok láthatósága változik
        sellingScript.boughtUpgradePrestige(cost_UnlockBX); //a feloldás ára levonásra kerül az egyenlegrõl
        progressScript.ProgressSet(2);
    }

    void unlockGL() //üveg feloldása
    {
        holderScript.glassUnlock(); //a szügséges objektumok láthatósága változik
        sellingScript.boughtUpgradePrestige(cost_UnlockGL); //a feloldás ára levonásra kerül az egyenlegrõl
        progressScript.ProgressSet(3);
    }

    void unlockBY() //elem feloldása
    {
        holderScript.batteryUnlock(); //a szügséges objektumok láthatósága változik
        sellingScript.boughtUpgradePrestige(cost_UnlockBY); //a feloldás ára levonásra kerül az egyenlegrõl
        progressScript.ProgressSet(4);
    }
}
