using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.ComTypes;
using Classes;
using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Bson;
using System;
using Newtonsoft.Json;

    public class DatabaseCommunication : MonoBehaviour
    {
        Selling sellingScript; 
        UpgradeButton pbUpgradeScripts; 
        UpgradeButton bxUpgradeScripts; 
        UpgradeButton glUpgradeScripts; 
        UpgradeButton byUpgradeScripts; 
        HolderBehavior holderScript; 
        OfflineEarning oEarningScript;
        AchivementController achivementScript;
        GemShopBehavior gemshopScript;

        OfflineEarning offlineEarning = new OfflineEarning(); // az offline eltöltött órákat adja vissza

        private static string username;
        private static string token;
        private string json;
        private string saveId;

        public SaveClass saveClass;
        public AchievementMainClass achievementClass;

        public string id;
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

        public float normalCurrency_spent;
        public float prestigeCurrency_spent;
        public int gemCurrency;
        public string[] achievementProgress;
        public int itemLvl_1;
        public int itemLvl_2;
        public int itemLvl_3;

        public DateTime lastSaveTime;

        public bool registrating;
        public bool login;

        void Start()
        {
            sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            pbUpgradeScripts = GameObject.FindGameObjectWithTag("PetBottleU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            bxUpgradeScripts = GameObject.FindGameObjectWithTag("BoxU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            glUpgradeScripts = GameObject.FindGameObjectWithTag("GlassU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            byUpgradeScripts = GameObject.FindGameObjectWithTag("BatteryU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            oEarningScript = GameObject.FindGameObjectWithTag("OfflineEarningsScript").GetComponent<OfflineEarning>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            achivementScript = GameObject.FindGameObjectWithTag("AchivementScript").GetComponent<AchivementController>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            gemshopScript = GameObject.FindGameObjectWithTag("GemshopScript").GetComponent<GemShopBehavior>(); //a scriptet kiveszi az adott objektumb�l mint komponense

            registrating = LogOrReg.Registered;
            login = LogOrReg.LoggedIn;

            achievementProgress = new string[] { "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0" };
            normalCurrency_spent = 0;
            prestigeCurrency_spent = 0;
            gemCurrency = 0;
            itemLvl_1 = 0;
            itemLvl_2 = 0;
            itemLvl_3 = 0;

            userid = 0;
            StartCoroutine(getData());
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
                    if (Register.token != null)
                    {
                        token = Register.token;
                    }
                    else if (Login.token != null)
                    {
                        token = Login.token;
                    }
                    else if (ForgottenPassword.token != null)
                    {
                        token = ForgottenPassword.token;
                    }

                    WWWForm form = new WWWForm();
                    form.AddField("username", username);
                    var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/getsave", form);
                    request.SetRequestHeader("Authorization", "Bearer " + token);
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
                    Debug.Log(json);
                        saveClass = JsonUtility.FromJson<SaveClass>(json);
                        StartCoroutine(getAchievements());
                        yield return null;
                    }
                    else
                    {
                        yield return null;
                    }

                    //adatok beállítása
                    id = saveClass.usersId;
                    normalCurrency = saveClass.normalCurrency;
                    prestigeCurrency = saveClass.prestigeCurrency;
                    totalEarnings = saveClass.totalEarnings;

                    PB_soldAmount = saveClass.pbSoldAmount;
                    PB_Unlocked = saveClass.pbUnlocked;
                    PB_valueLvl = saveClass.pbValue;
                    PB_speedLvl = saveClass.pbSpeed;
                    PB_frequencyLvl = saveClass.pbFrequency;

                    BX_soldAmount = saveClass.bxSoldAmount;
                    BX_Unlocked = saveClass.bxUnlocked;
                    BX_valueLvl = saveClass.bxValue;
                    BX_speedLvl = saveClass.bxSpeed;
                    BX_frequencyLvl = saveClass.bxFrequency;

                    GL_soldAmount = saveClass.glSoldAmount;
                    GL_Unlocked = saveClass.glUnlocked;
                    GL_valueLvl = saveClass.glValue;
                    GL_speedLvl = saveClass.glSpeed;
                    GL_frequencyLvl = saveClass.glFrequency;

                    BY_soldAmount = saveClass.bySoldAmount;
                    BY_Unlocked = saveClass.byUnlocked;
                    BY_valueLvl = saveClass.byValue;
                    BY_speedLvl = saveClass.bySpeed;
                    BY_frequencyLvl = saveClass.byFrequency;

                    lastSaveTime = DateTime.Parse(saveClass.lastSaveDate, null, System.Globalization.DateTimeStyles.RoundtripKind);

                /*
                    Debug.Log("savedata get");
                    Debug.Log(saveClass.pbUnlocked);
                    
                */
                Debug.Log(json);
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

                    achievementProgress = new string[] { "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0" };
                    normalCurrency_spent = 0;
                    prestigeCurrency_spent = 0;
                    gemCurrency = 0;
                    itemLvl_1 = 0;
                    itemLvl_2 = 0;
                    itemLvl_3 = 0;

                    break;
            }
        }

        public void startSaveData()
        {
            sellingScript.giveData();
            StartCoroutine(saveData());
        }

        public IEnumerator saveData()
        {
            WWWForm form = new WWWForm();
            form.AddField("id", "" + id);
            form.AddField("normalCurrency", "" + normalCurrency);
            form.AddField("prestigeCurrency", "" + prestigeCurrency);
            form.AddField("totalEarnings", "" + totalEarnings);
            if (GameObject.FindWithTag("U1") is not null)
            {
                form.AddField("pbUnlocked", "" + 1);
            }
            else
            {
                form.AddField("pbUnlocked", "" + 0);
            }
            form.AddField("pbSoldAmount", "" + PB_soldAmount);
            form.AddField("pbValue", "" + PB_valueLvl);
            form.AddField("pbFrequency", "" + PB_frequencyLvl);
            form.AddField("pbSpeed", "" + PB_speedLvl);
            if (GameObject.FindWithTag("U2") is not null)
            {
                //Debug.Log("jo");
                form.AddField("bxUnlocked", "" + 1);
            }
            else
            {
                form.AddField("bxUnlocked", "" + 0);
            }
            form.AddField("bxSoldAmount", "" + BX_soldAmount);
            form.AddField("bxValue", "" + BX_valueLvl);
            form.AddField("bxFrequency", "" + BX_frequencyLvl);
            form.AddField("bxSpeed", "" + BX_speedLvl);
            if (GameObject.FindWithTag("U3") is not null)
            {
                form.AddField("glUnlocked", "" + 1);
            }
            else
            {
                form.AddField("glUnlocked", "" + 0);
            }
            form.AddField("glSoldAmount", "" + GL_soldAmount);
            form.AddField("glValue", "" + GL_valueLvl);
            form.AddField("glFrequency", "" + GL_frequencyLvl);
            form.AddField("glSpeed", "" + GL_speedLvl);
            if (GameObject.FindWithTag("U4") is not null)
            {
                //Debug.Log("jo");
                form.AddField("byUnlocked", "" + 1);
            }
            else
            {
                form.AddField("byUnlocked", "" + 0);
            }
            form.AddField("bySoldAmount", "" + BY_soldAmount);
            form.AddField("byValue", "" + BY_valueLvl);
            form.AddField("byFrequency", "" + BY_frequencyLvl);
            form.AddField("bySpeed", "" + BY_speedLvl);
            var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/save", form);
            request.SetRequestHeader("Authorization", "Bearer " + token);
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
                //Debug.Log("Sikerült!");
                StartCoroutine(saveAchievements());
                yield return null;
            }
            else
            {
                //Debug.Log("error. " + request.downloadHandler.ToString());
            }
            yield return null;
        }

        public void loadCurreny(float nc, float pc, float te, int gc, float ncs, float pcs) //currencyk értékének frissitése
        {
            normalCurrency = nc;
            prestigeCurrency = pc;
            totalEarnings = te;
            gemCurrency = gc;
            normalCurrency_spent = ncs;
            prestigeCurrency_spent = pcs;
        }

        public void earningIncrease(string type, float n) //adott szeméttípus összbevételét n-nel növeli
        {
            switch (type)
            {
                case "PetBottle": PB_soldAmount += n; break;
                case "Box": BX_soldAmount += n; break;
                case "Glass": GL_soldAmount += n; break;
                case "Battery": BY_soldAmount += n; break;
            }
        }

        public void upgrade(int type, string property) //adott szeméttípus és tulajdonsás alapján a megfelelő szintet növelni
        {
            switch (type)
            {
                case 1:
                    switch (property)
                    {
                        case "value": PB_valueLvl++; break;
                        case "speed": PB_speedLvl++; break;
                        case "frequency": PB_frequencyLvl++; break;
                        //default: Debug.Log("Property hiba"); break;
                    }
                    break;
                case 2:
                    switch (property)
                    {
                        case "value": BX_valueLvl++; break;
                        case "speed": BX_speedLvl++; break;
                        case "frequency": BX_frequencyLvl++; break;
                        //default: Debug.Log("Property hiba"); break;
                    }
                    break;
                case 3:
                    switch (property)
                    {
                        case "value": GL_valueLvl++; break;
                        case "speed": GL_speedLvl++; break;
                        case "frequency": GL_frequencyLvl++; break;
                        //default: Debug.Log("Property hiba"); break;
                    }
                    break;
                case 4:
                    switch (property)
                    {
                        case "value": BY_valueLvl++; break;
                        case "speed": BY_speedLvl++; break;
                        case "frequency": BY_frequencyLvl++; break;
                        //default: Debug.Log("Property hiba"); break;
                    }
                    break;
                //default: Debug.Log("Type hiba"); break;
            }
        }

        public void giveData() //játék indításakor kiosztja a kapott adatokat
        {
            sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            pbUpgradeScripts = GameObject.FindGameObjectWithTag("PetBottleU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            bxUpgradeScripts = GameObject.FindGameObjectWithTag("BoxU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            glUpgradeScripts = GameObject.FindGameObjectWithTag("GlassU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            byUpgradeScripts = GameObject.FindGameObjectWithTag("BatteryU").GetComponent<UpgradeButton>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            holderScript = GameObject.FindGameObjectWithTag("WindowBehavior").GetComponent<HolderBehavior>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            oEarningScript = GameObject.FindGameObjectWithTag("OfflineEarningsScript").GetComponent<OfflineEarning>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            achivementScript = GameObject.FindGameObjectWithTag("AchivementScript").GetComponent<AchivementController>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            gemshopScript = GameObject.FindGameObjectWithTag("GemshopScript").GetComponent<GemShopBehavior>(); //a scriptet kiveszi az adott objektumb�l mint komponense
            Debug.Log("dbcomm given currencyvalues");
            sellingScript.getCurrencieValues();
            sellingScript.gotData = true;
            Debug.Log("dbcomm given holderscript data");
            holderScript.getData();
            Debug.Log("dbcomm holderscript loadedstart");
            holderScript.loadedStart();
            Debug.Log("dbcomm pb upgrades getlevels");
            pbUpgradeScripts.getLevels();
            Debug.Log("dbcomm bx upgrades getlevels");
            bxUpgradeScripts.getLevels();
            Debug.Log("dbcomm gl upgrades getlevels");
            glUpgradeScripts.getLevels();
            Debug.Log("dbcomm by upgrades getlevels");
            byUpgradeScripts.getLevels();
            Debug.Log("dbcomm achievement given data");
            achivementScript.getData();
            achivementScript.gotData = true;
            Debug.Log("dbcomm given gemshop data");
            gemshopScript.getData();
            Debug.Log("dbcomm proceed with offline earning");
            oEarningScript.proceedWithTasks();
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
                case "PetBottle": return PB_Unlocked;
                case "Box": return BX_Unlocked;
                case "Glass": return GL_Unlocked;
                case "Battery": return BY_Unlocked;
            }
            return false;
        }

        public void refreshTrashStatus(string type, bool status)
        {
            switch (type)
            {
                case "PetBottle": PB_Unlocked = status; break;
                case "Box": BX_Unlocked = status; break;
                case "Glass": GL_Unlocked = status; break;
                case "Battery": BY_Unlocked = status; break;
            }
        }

        public IEnumerator getAchievements()
        {
            if (Register.token != null)
            {
                token = Register.token;
            }
            else if (Login.token != null)
            {
                token = Login.token;
            }
            else if (ForgottenPassword.token != null)
            {
                token = ForgottenPassword.token;
            }

            //Debug.Log(token);
            WWWForm form = new WWWForm();
            var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/getAchievements", form);
            request.SetRequestHeader("Authorization", "Bearer " + token);
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
                //Debug.Log(json);
                achievementClass = JsonConvert.DeserializeObject<AchievementMainClass>(json); //Sikeresen megkapja az adatot backendről
                //Sikeresen deserializáljuk.
                normalCurrency_spent = (float)achievementClass.Achievements[0].normalCurrency_spent;
                prestigeCurrency_spent = (float)achievementClass.Achievements[0].prestigeCurrency_spent;
                gemCurrency = (int)achievementClass.Achievements[0].gemCurrency;
                itemLvl_1 = achievementClass.Achievements[0].itemLvl_1;
                itemLvl_2 = achievementClass.Achievements[0].itemLvl_2;
                itemLvl_3 = achievementClass.Achievements[0].itemLvl_3;
                achievementProgress = achievementClass.Achievements[0].achievementProgress;
                yield return null;
            }
            else
            {
                //Debug.Log("eror. getdata");
                yield return null;
            }
            login = true;
            giveData();
        }

        public IEnumerator saveAchievements()
        {
            if (Register.token != null)
            {
                token = Register.token;
            }
            else if (Login.token != null)
            {
                token = Login.token;
            }
            else if (ForgottenPassword.token != null)
            {
                token = ForgottenPassword.token;
            }

            AchievementClass achievementToSend = new AchievementClass
            {
                id = "",
                achievementProgress = achievementProgress,
                gemCurrency = gemCurrency,
                itemLvl_1 = itemLvl_1,
                itemLvl_2 = itemLvl_2,
                itemLvl_3 = itemLvl_3,
                normalCurrency_spent = normalCurrency_spent,
                prestigeCurrency_spent = prestigeCurrency_spent,
                usersId = ""
            };
            string jsonToSend = JsonConvert.SerializeObject(achievementToSend, Formatting.Indented);

            WWWForm form = new WWWForm();
            var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/setAchievements", form);
            byte[] arrayToSend = new System.Text.UTF8Encoding().GetBytes(jsonToSend);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(arrayToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);
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
                Debug.Log(json);
                yield return null;
            }
            else
            {
                //Debug.Log("eror. savedata");
                yield return null;
            }

            yield return null;
        }
    }
