using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineEarning : MonoBehaviour
{
    float earnedOffline;

    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelő script
    Selling sellingScript; //a currency-t kezel� script

    float[] values = new float[5];
    float[] frequencies = new float[5];
    bool[] unlocks = new bool[5];

    float multiplierPos = 1.02f; //2%-os növekedés
    float multiplierNeg = 0.98f; //2%-os csökkenés

    public Text text_Earning;
    public Button button_Confirm;
    public GameObject offlineEarning_Holder;

    //def value - 10
    //pb - 25, bx - 50, gl - 100, by - 200 -> value
    //pb - 2, bx - 3, gl - 4, by - 6 -> frequency

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumb�l mint komponense
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

        earnedOffline = 1000; //alap érték, később kivehető
        proceedWithTasks();
    }

    public float OfflineEarnings(DateTime lastSaveDate)
    {
        DateTime now = DateTime.Now;
        now = now.AddHours(-1);
        TimeSpan? offlineHours = new TimeSpan();
        offlineHours = now.Subtract(lastSaveDate);
        if (offlineHours.Value.TotalHours > 0.15)
        {
            Debug.Log("offline 600f");
            return 600f;
        }
        else
        {
            float offlineSeconds = (float)offlineHours.Value.TotalSeconds;
            Debug.Log($"offline {offlineSeconds}");
            return offlineSeconds;
        }
        return 0f;
    }

    public void getData() //megszerzi az adatokat a datascriptből
    {
        values[1] = 25 * Mathf.Pow(multiplierPos, dataScript.PB_valueLvl);
        values[2] = 50 * Mathf.Pow(multiplierPos, dataScript.BX_valueLvl);
        values[3] = 100 * Mathf.Pow(multiplierPos, dataScript.GL_valueLvl);
        values[4] = 200 * Mathf.Pow(multiplierPos, dataScript.BY_valueLvl);

        frequencies[1] = 2 * Mathf.Pow(multiplierPos, dataScript.PB_frequencyLvl);
        frequencies[2] = 3 * Mathf.Pow(multiplierPos, dataScript.BX_frequencyLvl);
        frequencies[3] = 4 * Mathf.Pow(multiplierPos, dataScript.GL_frequencyLvl);
        frequencies[4] = 6 * Mathf.Pow(multiplierPos, dataScript.BY_frequencyLvl);

        unlocks[1] = dataScript.giveTrashStatus("PetBottle");
        unlocks[2] = dataScript.giveTrashStatus("Box");
        unlocks[3] = dataScript.giveTrashStatus("Glass");
        unlocks[4] = dataScript.giveTrashStatus("Battery");
    }

    float calculateEarning(float time) //kiszámolja az offline szerzett currency-t
    {
        for (int i = 1; i < 5; i++)
        {
            if (unlocks[i])
            {
                earnedOffline += time / frequencies[i] * values[i];
            }
            else
            {
                switch (i)
                {
                    case 1: earnedOffline += time / 2 * 10; break;
                    case 2: earnedOffline += time / 3 * 10; break;
                    case 3: earnedOffline += time / 4 * 10; break;
                    case 4: earnedOffline += time / 6 * 10; break;
                }
            }
        }
        return earnedOffline;
    }

    public void proceedWithTasks() //ezzel lehet majd elindítani az offline earning teask-jeit !!hiányoss
    {
        getData(); //megszerzi a szükséges adatokat
        DateTime time = dataScript.lastSaveTime;
        calculateEarning(OfflineEarnings(time)); 
        offlineEarning_Holder.SetActive(true); //láthatóvá teszi az ablakot
        Debug.Log(earnedOffline);
        text_Earning.text = sellingScript.convertCurrencyToDisplay(earnedOffline.ToString()); //megjeleníti a szerzett pénzmennyiséget
    }

    public void confirmed() //akkor futódik le, ha leokolja az ablakot a játékos
    {
        sellingScript.soldTrashType(earnedOffline); //megkapja a keresett pénzmennyiséget
    }
}
