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
    FixData fixDataScript;

    float[] values = new float[5];
    float[] frequencies = new float[5];
    bool[] unlocks = new bool[5];

    float multiplierPos;

    public Text text_Earning;
    public Button button_Confirm;
    public GameObject offlineEarning_Holder;

    bool gotData;
    bool reg;

    int itemLvl_1;
    int itemLvl_2;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        multiplierPos = fixDataScript.multiplierPos;
    }

    public float OfflineEarnings(DateTime lastSaveDate)
    {
        DateTime now = DateTime.Now;
        now = now.AddHours(-1);
        TimeSpan? offlineHours = new TimeSpan();
        offlineHours = now.Subtract(lastSaveDate);
        if (itemLvl_1 > 3)
        {
            itemLvl_1 = 3;
        }

        switch (itemLvl_1)
        {
            default:
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
    }

    public void getData()
    {
        itemLvl_1 = dataScript.itemLvl_1;
        itemLvl_1 = dataScript.itemLvl_2;

        values[1] = fixDataScript.giveTrashProperties("PetBottle", "Value") * Mathf.Pow(multiplierPos, dataScript.PB_valueLvl) * fixDataScript.gemshopValueMultiplier(itemLvl_2);
        values[2] = fixDataScript.giveTrashProperties("Box", "Value") * Mathf.Pow(multiplierPos, dataScript.BX_valueLvl) * fixDataScript.gemshopValueMultiplier(itemLvl_2);
        values[3] = fixDataScript.giveTrashProperties("Glass", "Value") * Mathf.Pow(multiplierPos, dataScript.GL_valueLvl) * fixDataScript.gemshopValueMultiplier(itemLvl_2);
        values[4] = fixDataScript.giveTrashProperties("Battery", "Value") * Mathf.Pow(multiplierPos, dataScript.BY_valueLvl) * fixDataScript.gemshopValueMultiplier(itemLvl_2);

        frequencies[1] = fixDataScript.giveTrashProperties("PetBottle", "Frequency") * Mathf.Pow(multiplierPos, dataScript.PB_frequencyLvl);
        frequencies[2] = fixDataScript.giveTrashProperties("Box", "Frequency") * Mathf.Pow(multiplierPos, dataScript.BX_frequencyLvl);
        frequencies[3] = fixDataScript.giveTrashProperties("Glass", "Frequency") * Mathf.Pow(multiplierPos, dataScript.GL_frequencyLvl);
        frequencies[4] = fixDataScript.giveTrashProperties("Battery", "Frequency") * Mathf.Pow(multiplierPos, dataScript.BY_frequencyLvl);

        unlocks[1] = dataScript.giveTrashStatus("PetBottle");
        unlocks[2] = dataScript.giveTrashStatus("Box");
        unlocks[3] = dataScript.giveTrashStatus("Glass");
        unlocks[4] = dataScript.giveTrashStatus("Battery");

        
        reg = dataScript.registrating;
    }

    float calculateEarning(float time)
    {
        earnedOffline = 0;
        for (int i = 1; i < 5; i++)
        {
            if (unlocks[i])
            {
                earnedOffline += (time / frequencies[i]) * values[i] * fixDataScript.multiplierOffline;
            }
            else
            {
                earnedOffline += (time / frequencies[i]) * fixDataScript.defaultValue * fixDataScript.multiplierOffline;
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
            text_Earning.text = sellingScript.convertCurrencyToDisplay(earnedOffline.ToString());
        }
    }

    public void confirmed()
    {
        sellingScript.increaseMoney(earnedOffline);
    }
}
