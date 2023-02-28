using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineEarning : MonoBehaviour
{
    float earnedOffline;

    DatabaseCommunication dataScript;
    Selling sellingScript;

    float[] values = new float[5];
    float[] frequencies = new float[5];
    bool[] unlocks = new bool[5];

    float multiplierPos = 1.02f;

    public Text text_Earning;
    public Button button_Confirm;
    public GameObject offlineEarning_Holder;

    bool gotData;
    bool reg;

    int itemLvl_1;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
    }

    public float OfflineEarnings(DateTime lastSaveDate)
    {
        DateTime now = DateTime.Now;
        now = now.AddHours(-1);
        TimeSpan? offlineHours = new TimeSpan();
        offlineHours = now.Subtract(lastSaveDate);

        switch (itemLvl_1)
        {
            case 0:
                if (offlineHours.Value.TotalHours > 0.25)
                {
                    return 600f;
                }
                else
                {
                    float offlineSeconds = (float)offlineHours.Value.TotalSeconds;
                    return offlineSeconds;
                }
            case 1:
                if (offlineHours.Value.TotalHours > 0.5)
                {
                    return 1800f;
                }
                else
                {
                    float offlineSeconds = (float)offlineHours.Value.TotalSeconds;
                    return offlineSeconds;
                }
            case 2:
                if (offlineHours.Value.TotalHours > 1)
                {
                    return 3600f;
                }
                else
                {
                    float offlineSeconds = (float)offlineHours.Value.TotalSeconds;
                    return offlineSeconds;
                }
            case 3:
                if (offlineHours.Value.TotalHours > 1.5)
                {
                    return 5400f;
                }
                else
                {
                    float offlineSeconds = (float)offlineHours.Value.TotalSeconds;
                    return offlineSeconds;
                }
        }
        return 0f;
    }

    public void getData()
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

        itemLvl_1 = dataScript.itemLvl_1;
        reg = dataScript.registrating;
    }

    float calculateEarning(float time)
    {
        earnedOffline = 0;
        for (int i = 1; i < 5; i++)
        {
            if (unlocks[i])
            {
                earnedOffline += time / frequencies[i] * values[i] * 0.7f;
            }
            else
            {
                switch (i)
                {
                    case 1: earnedOffline += time / 2 * 10 * 0.7f; break;
                    case 2: earnedOffline += time / 3 * 10 * 0.7f; break;
                    case 3: earnedOffline += time / 4 * 10 * 0.7f; break;
                    case 4: earnedOffline += time / 6 * 10 * 0.7f; break;
                }
            }
        }
        return earnedOffline;
    }

    public void proceedWithTasks()
    {
        getData();
        if(reg != true)
        {
            DateTime time = dataScript.lastSaveTime;
            calculateEarning(OfflineEarnings(time));
            offlineEarning_Holder.SetActive(true);
            Debug.Log(earnedOffline);
            text_Earning.text = sellingScript.convertCurrencyToDisplay(earnedOffline.ToString());
        }
    }

    public void confirmed()
    {
        sellingScript.increaseMoney(earnedOffline);
    }
}
