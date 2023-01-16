using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.ComTypes;
using Classes;
using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;

public class DatabaseCommunication : MonoBehaviour
{
    Selling sellingScript; //a currency-t kezelõ script
    UpgradeButton pbUpgradeScripts; //a petbottle fejlesztését kezelõ script
    UpgradeButton bxUpgradeScripts; //a box fejlesztését kezelõ script
    UpgradeButton glUpgradeScripts; //a glass fejlesztését kezelõ script
    UpgradeButton byUpgradeScripts; //a battery fejlesztését kezelõ script
    HolderBehavior holderScript; //a holderek viselkedését kezelõ script
    GettingProgress progressScript; // a feloldott haladást jelzi vissza

    private static string username;

    private string json;

    private string saveId;

    public SaveClass saveClass;

    int userid;

    public float normalCurrency;
    public float prestigeCurrency;
    public float totalEarnings;

    public float PB_soldAmount;
    private bool PB_Unlocked;
    public int PB_valueLvl;
    public int PB_speedLvl;
    public int PB_frequencyLvl;

    public float BX_soldAmount;
    private bool BX_Unlocked;
    public int BX_valueLvl;
    public int BX_speedLvl;
    public int BX_frequencyLvl;

    public float GL_soldAmount;
    private bool GL_Unlocked;
    public int GL_valueLvl;
    public int GL_speedLvl;
    public int GL_frequencyLvl;

    public float BY_soldAmount;
    private bool BY_Unlocked;
    public int BY_valueLvl;
    public int BY_speedLvl;
    public int BY_frequencyLvl;


    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        pbUpgradeScripts = GameObject.FindGameObjectWithTag("PetBottleU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumból mint komponense
        bxUpgradeScripts = GameObject.FindGameObjectWithTag("BoxU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumból mint komponense
        glUpgradeScripts = GameObject.FindGameObjectWithTag("GlassU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumból mint komponense
        byUpgradeScripts = GameObject.FindGameObjectWithTag("BatteryU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumból mint komponense
        holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>(); //a scriptet kiveszi az adott objektumból mint komponense
        progressScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<GettingProgress>(); //a scriptet kiveszi az adott objektumból mint komponense

        //ideiglenesen:
        userid = 0;
        StartCoroutine(getData());
        giveData();
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
                    Debug.Log(json);

                }
                else
                {
                    Debug.Log("eror. getdata");
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

                Debug.Log(saveClass.id);
                Debug.Log("savedata get");

                sellingScript.getCurrencieValues();
                giveData();

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
        getTrashStatus();
        Debug.Log(progressScript.sendProgress());
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
        Debug.Log(PB_Unlocked);
        form.AddField("pbSoldAmount", "" + PB_soldAmount);
        form.AddField("pbValue", "" + PB_valueLvl);
        form.AddField("pbFrequency", "" + PB_frequencyLvl);
        form.AddField("pbSpeed", "" + PB_speedLvl);
        Debug.Log(BX_Unlocked);
        form.AddField("bxUnlocked", "" + BX_Unlocked);
        form.AddField("bxSoldAmount", "" + BX_soldAmount);
        form.AddField("bxValue", "" + BX_valueLvl);
        form.AddField("bxFrequency", "" + BX_frequencyLvl);
        form.AddField("bxSpeed", "" + BX_speedLvl);
        Debug.Log(GL_Unlocked);
        form.AddField("glUnlocked", "" + GL_Unlocked);
        form.AddField("glSoldAmount", "" + GL_soldAmount);
        form.AddField("glValue", "" + GL_valueLvl);
        form.AddField("glFrequency", "" + GL_frequencyLvl);
        form.AddField("glSpeed", "" + GL_speedLvl);
        Debug.Log(BY_Unlocked);
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
           Debug.Log("error. savedata");
        }

        

        yield return null;
    }

    public void loadCurreny(float nc, float pc, float te) //más scriptek átadják neki ezzel a currency-k értékét
    {
        //a megadott értékekre állítja a változókat
        normalCurrency = nc;
        prestigeCurrency = pc;
        totalEarnings = te;
    }

    public void earningIncrease(string type, float n) //feladata a megkapott szeméttípus összbevételét n-nel növelni
    {
        switch (type) 
        {
            case "PetBottle": PB_soldAmount += n; break;
            case "Box": BX_soldAmount += n; break;
            case "Glass": GL_soldAmount += n; break;
            case "Battery": BY_soldAmount += n; break;
        }
    }

    public void upgrade(int type, string property) //feladata a megkapott szeméttípus és annak tulajdonsága alapján a megfelelõ szintet növelni
    {
        switch (type)
        {
            case 1:
                switch (property)
                {
                    case "value": PB_valueLvl++; break;
                    case "speed": PB_speedLvl++; break;
                    case "frequency": PB_frequencyLvl++; break;
                    default: Debug.Log("Property hiba"); break; //kiírja, ha rossz adatot kapott, mint property
                }
                break;
            case 2:
                switch (property)
                {
                    case "value": BX_valueLvl++; break;
                    case "speed": BX_speedLvl++; break;
                    case "frequency": BX_frequencyLvl++; break;
                    default: Debug.Log("Property hiba"); break; //kiírja, ha rossz adatot kapott, mint property
                }
                break;
            case 3:
                switch (property)
                {
                    case "value": GL_valueLvl++; break;
                    case "speed": GL_speedLvl++; break;
                    case "frequency": GL_frequencyLvl++; break;
                    default: Debug.Log("Property hiba"); break; //kiírja, ha rossz adatot kapott, mint property
                }
                break;
            case 4:
                switch (property)
                {
                    case "value": BY_valueLvl++; break;
                    case "speed": BY_speedLvl++; break;
                    case "frequency": BY_frequencyLvl++; break;
                    default: Debug.Log("Property hiba"); break; //kiírja, ha rossz adatot kapott, mint property
                }
                break;
            default: Debug.Log("Type hiba"); break; //kiírja, ha rossz adatot kapott, mint type
        }
    }

    public void giveData() //feladata (a játék indulásakor) az összes script metódusát meghívni, amelyik adatot vesz át a mentésbõl
    {
        progresssetupAtStart();
        pbUpgradeScripts.getLevels();
        bxUpgradeScripts.getLevels();
        glUpgradeScripts.getLevels();
        byUpgradeScripts.getLevels();

        sellingScript.getCurrencieValues();

        holderScript.getData();
        holderScript.loadedStart();
    }

    public void prestigeTasks()
    {
        PB_soldAmount = 0;
        PB_valueLvl = 0;
        PB_speedLvl = 0;
        PB_frequencyLvl = 0;

        BX_soldAmount = 0;
        BX_valueLvl = 0;
        BX_speedLvl = 0;
        BX_frequencyLvl = 0;

        GL_soldAmount = 0;
        GL_valueLvl = 0;
        GL_speedLvl = 0;
        GL_frequencyLvl = 0;

        BY_soldAmount = 0;
        BY_valueLvl = 0;
        BY_speedLvl = 0;
        BY_frequencyLvl = 0;

        pbUpgradeScripts.getLevels();
        bxUpgradeScripts.getLevels();
        glUpgradeScripts.getLevels();
        byUpgradeScripts.getLevels();

        holderScript.getData();
        holderScript.loadedStart();
    }

    public bool giveTrashStatus(string type)
    {
        switch (type)
        {
            case "PetBottle": return PB_Unlocked; break;
            case "Box": return BX_Unlocked; break;
            case "Glass": return GL_Unlocked; break;
            case "Battery": return BY_Unlocked; break;
        }
        return false;
    }

    void getTrashStatus()
    {
        switch (progressScript.sendProgress())
        {
            case 0:
                PB_Unlocked = false;
                BX_Unlocked = false;
                GL_Unlocked = false;
                BY_Unlocked = false;
                break;
            case 1:
                PB_Unlocked = true;
                BX_Unlocked = false;
                GL_Unlocked = false;
                BY_Unlocked = false;
                break;
            case 2:
                PB_Unlocked = true;
                BX_Unlocked = true;
                GL_Unlocked = false;
                BY_Unlocked = false;
                break;
            case 3:
                PB_Unlocked = true;
                BX_Unlocked = true;
                GL_Unlocked = true;
                BY_Unlocked = false;
                break;
            case 4:
                PB_Unlocked = true;
                BX_Unlocked = true;
                GL_Unlocked = true;
                BY_Unlocked = true;
                break;
            default: Debug.Log("getTrashStats:databasecomm hiba"); break;
        }
    }

    void progresssetupAtStart()
    {
        if(!PB_Unlocked && !BX_Unlocked && !GL_Unlocked && !BY_Unlocked)
        {
            progressScript.ProgressSet(0);
        }
        if (PB_Unlocked && !BX_Unlocked && !GL_Unlocked && !BY_Unlocked)
        {
            progressScript.ProgressSet(1);
        }
        if (PB_Unlocked && BX_Unlocked && GL_Unlocked && !BY_Unlocked)
        {
            progressScript.ProgressSet(3);
        }
        if (PB_Unlocked && BX_Unlocked && GL_Unlocked && BY_Unlocked)
        {
            progressScript.ProgressSet(4);
        }
    }
}
