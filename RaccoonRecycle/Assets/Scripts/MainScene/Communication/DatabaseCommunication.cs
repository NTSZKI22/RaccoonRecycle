using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using System.Runtime.InteropServices.ComTypes;
using Classes;
using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;

public class DatabaseCommunication : MonoBehaviour
{
    Selling sellingScript;

    private static string username;

    private string json;

    private string saveId;

    public SaveClass saveClass;

    int userid;

    public float normalCurrency;
    public float prestigeCurrency;
    public float totalEarnings;

    public float PB_soldAmount;
    public bool PB_Unlocked;
    public int PB_valueLvl;
    public int PB_speedLvl;
    public int PB_frequencyLvl;

    public float BX_soldAmount;
    public bool BX_Unlocked;
    public int BX_valueLvl;
    public int BX_speedLvl;
    public int BX_frequencyLvl;

    public float GL_soldAmount;
    public bool GL_Unlocked;
    public int GL_valueLvl;
    public int GL_speedLvl;
    public int GL_frequencyLvl;

    public float BY_soldAmount;
    public bool BY_Unlocked;
    public int BY_valueLvl;
    public int BY_speedLvl;
    public int BY_frequencyLvl;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        //ideiglenesen:
        userid = 0;
        StartCoroutine(getData());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator getData()
    {
        //adatok lekérése
        switch (userid)
        {
            case 0:
                if (Register.localUserName != null)
                {
                    username = Register.localUserName;
                }
                else if (Login.localUserName != null)
                {
                    username = Login.localUserName;
                }
                else if (ForgottenPassword.localUserName != null)
                {
                    username = ForgottenPassword.localUserName;
                }

                WWWForm form = new WWWForm();
                form.AddField("username", username);
                var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/getsave", form);
                var handler = request.SendWebRequest();

                float startTime = 0f;
                while (!handler.isDone)
                {
                    startTime += Time.deltaTime;
                    if (startTime > 10.0f)
                    {
                        break;
                    }
                    yield return null;
                }
                if (request.result == UnityWebRequest.Result.Success)
                {
                    json = request.downloadHandler.text;
                    saveClass = JsonUtility.FromJson<SaveClass>(json);

                }
                else
                {
                    Debug.Log("eror.");
                }

                //adatok beállítása
                saveId = saveClass.id;
                normalCurrency = saveClass.normalCurrency;
                prestigeCurrency = saveClass.prestigeCurrency;
                totalEarnings = saveClass.totalEarnings;

                PB_soldAmount = saveClass.pbSoldAmount;
                PB_Unlocked = saveClass.pbUnlocked;
                PB_valueLvl = saveClass.pbValue;
                PB_speedLvl = saveClass.pbSpeed;
                PB_frequencyLvl = saveClass.pbFrequency;

                BX_soldAmount = saveClass.bxSoldAmount;
                BX_Unlocked = saveClass.byUnlocked;
                BX_valueLvl = saveClass.bxValue;
                BX_speedLvl = saveClass.bxSpeed;
                BX_frequencyLvl = saveClass.byFrequency;

                GL_soldAmount = saveClass.glSoldAmount;
                GL_Unlocked = saveClass.glUnlocked;
                GL_valueLvl = saveClass.glValue;
                GL_speedLvl = saveClass.glSpeed;
                GL_frequencyLvl = saveClass.glFrequency;

                BY_soldAmount = saveClass.glSoldAmount;
                BY_Unlocked = saveClass.glUnlocked;
                BY_valueLvl = saveClass.glValue;
                BY_speedLvl = saveClass.glSpeed;
                BY_frequencyLvl = saveClass.glFrequency;


                sellingScript.getCurrencieValues();

                break;

            case -1:

                normalCurrency = 0;
                prestigeCurrency = 0;
                totalEarnings = 0;

                PB_soldAmount = 0;
                PB_Unlocked = false;
                PB_valueLvl = 0;
                PB_speedLvl = 0;
                PB_frequencyLvl = 0;

                BX_soldAmount = 0;
                BX_Unlocked = false;
                BX_valueLvl = 0;
                BX_speedLvl = 0;
                BX_frequencyLvl = 0;

                GL_soldAmount = 0;
                GL_Unlocked = false;
                GL_valueLvl = 0;
                GL_speedLvl = 0;
                GL_frequencyLvl = 0;

                BY_soldAmount = 0;
                BY_Unlocked = false;
                BY_valueLvl = 0;
                BY_speedLvl = 0;
                BY_frequencyLvl = 0;

                break;
        }
    }

    public void startSaveData()
    {
        StartCoroutine(saveData());
    }

    public IEnumerator saveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", saveId);
        form.AddField("normalCurrency", "" + normalCurrency);
        form.AddField("prestigeCurrency", "" + prestigeCurrency);
        form.AddField("totalEarnings", "" + totalEarnings);
        form.AddField("pbUnlocked", "" + PB_Unlocked);
        form.AddField("pbSoldAmount", "" + PB_soldAmount);
        form.AddField("pbValue", "" + PB_valueLvl);
        form.AddField("pbFrequency", "" + PB_frequencyLvl);
        form.AddField("pbSpeed", "" + PB_speedLvl);

        form.AddField("bxUnlocked", "" + BX_Unlocked);
        form.AddField("bxSoldAmount", "" + BX_soldAmount);
        form.AddField("bxValue", "" + BX_valueLvl);
        form.AddField("bxFrequency", "" + BX_frequencyLvl);
        form.AddField("bxSpeed", "" + BX_speedLvl);

        form.AddField("glUnlocked", "" + GL_Unlocked);
        form.AddField("glSoldAmount", "" + GL_soldAmount);
        form.AddField("glValue", "" + GL_valueLvl);
        form.AddField("glFrequency", "" + GL_frequencyLvl);
        form.AddField("glSpeed", "" + GL_speedLvl);

        form.AddField("byUnlocked", "" + BY_Unlocked);
        form.AddField("bySoldAmount", "" + BY_soldAmount);
        form.AddField("byValue", "" + BY_valueLvl);
        form.AddField("byFrequency", "" + BY_frequencyLvl);
        form.AddField("bySpeed", "" + BY_speedLvl);
        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/save", form);
        var handler = request.SendWebRequest();

        float startTime = 0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 10.0f)
            {
                break;
            }
            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.ToString());
        }
        else
        {
            Debug.Log("error.");
        }

        yield return null;
    }

    public void loadCurreny(float nc, float pc, float te)
    {
        normalCurrency = nc;
        prestigeCurrency = pc;
        totalEarnings = te;
    }

    public void pbValue()
    {
        PB_valueLvl++;
    }

    public void pbSpeed()
    {
        PB_speedLvl++;
    }

    public void pbFrequency()
    {
        PB_frequencyLvl++;
    }

    public void bxValue()
    {
        BX_valueLvl++;
    }

    public void bxSpeed()
    {
        BX_speedLvl++;
    }

    public void bxFrequency()
    {
        BX_frequencyLvl++;
    }

    public void glValue()
    {
        GL_valueLvl++;
    }

    public void glSpeed()
    {
        GL_speedLvl++;
    }

    public void glFrequency()
    {
        GL_frequencyLvl++;
    }

    public void byValue()
    {
        BY_valueLvl++;
    }

    public void bySpeed()
    {
        BY_speedLvl++;
    }

    public void byFrequency()
    {
        BY_frequencyLvl++;
    }

    public void pbEarningsIncrease(float n)
    {
        PB_soldAmount += n;
    }

    public void bxEarningsIncrease(float n)
    {
        BX_soldAmount += n;
    }

    public void glEarningsIncrease(float n)
    {
        GL_soldAmount += n;
    }

    public void byEarningsIncrease(float n)
    {
        BY_soldAmount += n;
    }
}
