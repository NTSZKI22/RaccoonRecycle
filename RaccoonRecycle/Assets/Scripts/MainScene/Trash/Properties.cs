using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    //alap értékei a bizonyos tulajdonságoknak
    float defValue; //minél nagyobb annál többet ért
    float defSpeed; //minél nagyobb annál gyorsabb
    float defFrequency; //minél kisebb annál gyorsabb

    int valueLvl; //érték szintje
    int speedLvl; //gyorsaság szintje
    int frequencylvl; //gyakoriság szintje

    float multiplierPos; //pozitív szorzó
    float multiplierNeg; //negatív szorzó


    // Start is called before the first frame update
    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

        //alap érték megadása a következõ változóknak
        multiplierPos = 1.02f; //2%-os növekedés
        multiplierNeg = 0.98f; //2%-os csökkenés

        defProperties(); //alap értéket határoz meg az elemnek, amin van

        
    }

    // Update is called once per frame
    void Update()
    {
        getLevels(); //elkéri a szintjeit a különbözõ tulajdonságoknak folyamatos, hátha közben valamit fejlesztettek
    }

    public void defProperties() //megadja a tulajdonságok alapértelmezett értékét
    {
        if (this.gameObject.tag == "PetBottle") //ha az objektum petbottle tag-gel rendelkezik
        {
            
            //alap értékeket ad
            
            defSpeed = 200; 
            defValue = 25;
            defFrequency = 2;
        }
        if (gameObject.tag == "Box") //ha az objektum box tag-gel rendelkezik
        {
            //alap értékeket ad
            defSpeed = 100;
            defValue = 50;
            defFrequency = 3;
        }
        if (gameObject.tag == "Glass") //ha az objektum glass tag-gel rendelkezik
        {
            //alap értékeket ad
            defSpeed = 90;
            defValue = 100;
            defFrequency = 4;
        }
        if (gameObject.tag == "Battery") //ha az objektum battery tag-gel rendelkezik
        {
            //alap értékeket ad
            defSpeed = 80;
            defValue = 200;
            defFrequency = 6;
        }
    }

    void getLevels() //adatbázisból elkért a változók értékét beállítja
    {
        if (gameObject.tag == "PetBottle") //ha az objektum petbottle tag-gel rendelkezik
        {
            //a változók értéke a hozzá tartozó adat lesz
            speedLvl = dataScript.PB_speedLvl;
            valueLvl = dataScript.PB_valueLvl;
            frequencylvl = dataScript.PB_frequencyLvl;
        }
        if (gameObject.tag == "Box") //ha az objektum box tag-gel rendelkezik
        {
            //a változók értéke a hozzá tartozó adat lesz
            speedLvl = dataScript.BX_speedLvl;
            valueLvl = dataScript.BX_valueLvl;
            frequencylvl = dataScript.BX_frequencyLvl;
        }
        if (gameObject.tag == "Glass") //ha az objektum glass tag-gel rendelkezik
        {
            //a változók értéke a hozzá tartozó adat lesz
            speedLvl = dataScript.GL_speedLvl;
            valueLvl = dataScript.GL_valueLvl;
            frequencylvl = dataScript.GL_frequencyLvl;
        }
        if (gameObject.tag == "Battery") //ha az objektum battery tag-gel rendelkezik
        {
            //a változók értéke a hozzá tartozó adat lesz
            speedLvl = dataScript.BY_speedLvl;
            valueLvl = dataScript.BY_valueLvl;
            frequencylvl = dataScript.BY_frequencyLvl;
        }
    }

    public float value() //visszaadja a tényleges értéket
    {
        return defValue * Mathf.Pow(multiplierPos, valueLvl); //alapérték * szorzó ^ szint képlet értékét visszaadja
    }

    public float valueDef() //visszaadja a tényleges értéket
    {
        return defValue; //alapérték
    }

    public float speed() //visszaadja a tényleges sepességét
    {
        return defSpeed * Mathf.Pow(multiplierPos, speedLvl); //alapérték * szorzó ^ szint képlet értékét visszaadja
    }

    public float frequency() //visszaadja a tényleges létrehozásának gyakoriságát
    {
        return defFrequency * Mathf.Pow(multiplierNeg, frequencylvl); //alapérték * szorzó ^ szint képlet értékét visszaadja
    }

}
