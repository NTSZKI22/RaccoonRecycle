using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementController : MonoBehaviour
{
    DatabaseCommunication dataScript; 
    Selling sellingScript;
    FixData fixDataScript;

    int[] requirements7;

    string[] achievementTexts;

    string[] achievementProgress;

    public GameObject notificationDot;
    public Text notificationNumber;

    public GameObject notificationWindow;
    public GameObject parent;

    float normalCurrency;
    float prestigeCurrency;
    float totalEarnings;

    float PB_soldAmount;
    bool PB_Unlocked;
    int PB_valueLvl;
    int PB_speedLvl;
    int PB_frequencyLvl;

    float BX_soldAmount;
    bool BX_Unlocked;
    int BX_valueLvl;
    int BX_speedLvl;
    int BX_frequencyLvl;

    float GL_soldAmount;
    bool GL_Unlocked;
    int GL_valueLvl;
    int GL_speedLvl;
    int GL_frequencyLvl;

    float BY_soldAmount;
    bool BY_Unlocked;
    int BY_valueLvl;
    int BY_speedLvl;
    int BY_frequencyLvl;

    float normalCurrency_spent;
    float prestigeCurrency_spent;

    public bool gotData;

    float PB_defValue;
    float BX_defValue;
    float GL_defValue;
    float BY_defValue;

    float multiplier;

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        gotData = false;

        PB_defValue = fixDataScript.PB_defValue;
        BX_defValue = fixDataScript.BX_defValue;
        GL_defValue = fixDataScript.GL_defValue;
        BY_defValue = fixDataScript.BY_defValue;
        multiplier = fixDataScript.multiplierPos;

        requirements7 = fixDataScript.requirements7;
        achievementTexts = fixDataScript.achievementTexts;
        achievementProgress = fixDataScript.achievementProgress;
    }

    void Update()
    {
        if (gotData || dataScript.registrating)
        {
            getData();
            refreshAchievments();
            returnData();
            notifyDot();
        }
        refreshAchievments();
    }

    public void getData()
    {
        achievementProgress = dataScript.achievementProgress;

        normalCurrency = dataScript.normalCurrency;
        prestigeCurrency = dataScript.prestigeCurrency;
        totalEarnings = dataScript.totalEarnings;
        normalCurrency_spent = dataScript.normalCurrency_spent;
        prestigeCurrency_spent = dataScript.prestigeCurrency_spent;

        PB_soldAmount = dataScript.PB_soldAmount;
        PB_Unlocked = dataScript.giveTrashStatus("PetBottle");
        PB_valueLvl = dataScript.PB_valueLvl;
        PB_speedLvl = dataScript.PB_speedLvl;
        PB_frequencyLvl = dataScript.PB_frequencyLvl;

        BX_soldAmount = dataScript.BX_soldAmount;
        BX_Unlocked = dataScript.giveTrashStatus("Box");
        BX_valueLvl = dataScript.BX_valueLvl;
        BX_speedLvl = dataScript.BX_speedLvl;
        BX_frequencyLvl = dataScript.BX_frequencyLvl;

        GL_soldAmount = dataScript.GL_soldAmount;
        GL_Unlocked = dataScript.giveTrashStatus("Glass");
        GL_valueLvl = dataScript.GL_valueLvl;
        GL_speedLvl = dataScript.GL_speedLvl;
        GL_frequencyLvl = dataScript.GL_frequencyLvl;

        BY_soldAmount = dataScript.BY_soldAmount;
        BY_Unlocked = dataScript.giveTrashStatus("Battery");
        BY_valueLvl = dataScript.BY_valueLvl;
        BY_speedLvl = dataScript.BY_speedLvl;
        BY_frequencyLvl = dataScript.BY_frequencyLvl;
    }

    void notifyDot()
    {
        int count = 0;
        for (int i = 0; i < achievementProgress.Length; i++)
        {
            string[] ab = achievementProgress[i].Split("_");
            if (int.Parse(ab[0]) !=0 && !(int.Parse(ab[0]) < int.Parse(ab[1])))
            {
                if( 5 < i && i < 15)
                {
                    if (achievementProgress[i].Equals("1_0"))
                    {
                        count++;
                    }
                }else if (!achievementProgress[i].Equals("0_0"))
                {
                    count++;
                }
            }
        }
        if (count > 0)
        {
            notificationNumber.text = count.ToString();
            notificationDot.SetActive(true);
        }
        else
        {
            notificationDot.SetActive(false);
        }
    }

    void notifyWindow()
    {
        GameObject window = Instantiate(notificationWindow) as GameObject;
        window.transform.SetParent(parent.transform);
        window.SetActive(true);
        window.transform.position = notificationWindow.transform.position;
        Destroy(window, 1);
    }

    public string[] placeholderStrings(int index)
    {
        switch (index)
        {
            case 0: case 2: case 3: case 15: case 16: case 17: case 18: 
                return new string[] { "100", "1000", "10K", "100K", "1M", "10M", "100M" };
            case 1: return new string[] { "10K", "100K", "1M", "10M" };
            case 4: return new string[] { "10", "20", "30", "40", "50", "60", "70", "76(max)" };
            case 5: return new string[] { "10", "25", "50", "100", "150", "200", "300", "400", "500" };
            case 19: return new string[] { "50", "75", "100" };
            case 20: return new string[] { "75", "100", "125", "150", "175", "200", "225" };
            case 21: return new string[] { "150", "200", "250", "300", "350", "400", "450" };
            case 22: return new string[] { "300", "400", "500", "600", "700", "800", "900" };
        }
        return new string[] { " ", " "};
    }

    public int[] rewardArray(int index)
    {
        switch (index)
        {
            case 6: case 7: case 8: case 9: case 10: case 11: case 12: case 13: case 14:
                return new int[] { 100 };
            case 19:
                return new int[] { 5, 10, 25 };
            case 1:
                return new int[] { 20, 25, 50, 100 };
            case 0: case 2: case 3: case 15: case 16: case 17: case 18: case 20: case 21: case 22:
                return new int[] { 10, 25, 50, 75, 100, 150, 200 };
            case 4:
                return new int[] { 10, 25, 50, 75, 100, 100, 100, 100 };
            case 5:
                return new int[] { 10, 25, 50, 75, 100, 100, 100, 100, 100 };
        }
        return new int[] { 100 };
    }

    public string achievementText(int index)
    {
        return achievementTexts[index];
    }

    public string Achivement_Status(int index)
    {
        return achievementProgress[index];
    }

    public void AchievementClaimed(int index)
    {
        string[] ab = achievementProgress[index].Split("_");
        int b = int.Parse(ab[1]) + 1;
        achievementProgress[index] = $"{ab[0]}_{b}";
    }

    public void returnData()
    {
        dataScript.achievementProgress = achievementProgress;
    }

    void refreshAchievments()
    {
        int[] trashLevels = new int[] { PB_valueLvl, PB_speedLvl, PB_frequencyLvl, BX_valueLvl, BX_speedLvl, BX_frequencyLvl, GL_valueLvl, GL_speedLvl, GL_frequencyLvl, BY_valueLvl, BY_speedLvl, BY_frequencyLvl }; 

        Achievement_General(0, requirements7, totalEarnings); //Earn x Raccoins in total
        Achievement_General(1, new int[] { 10000, 100000, 1000000, 10000000 }, prestigeCurrency); //Have c Racoonium
        Achievement_General(2, requirements7, normalCurrency_spent); //Spend x Raccoins
        Achievement_General(3, requirements7, prestigeCurrency_spent); //Spend x Racoonium

        Achievement_Any(4, new int[] { 10, 20, 30, 40, 50, 60, 70, 76 }, trashLevels); //Reach level y with any upgrades
        Achievement_All(5, new int[] { 10, 25, 50, 100, 150, 200, 300, 400, 500 }, trashLevels); //Reach a total of z level with upgrades
        Achievement_Any(6, new int[] { 76 }, trashLevels); //Max out any upgrade
        
        Achievement_MaxFull(7, 76, new int[] { PB_valueLvl, PB_speedLvl, PB_frequencyLvl }); //Max out the Petbottle refinery
        Achievement_MaxFull(8, 76, new int[] { BX_valueLvl, BX_speedLvl, BX_frequencyLvl }); //Max out the Box refinery
        Achievement_MaxFull(9, 76, new int[] { GL_valueLvl, GL_speedLvl, GL_frequencyLvl }); //Max out the Gass refinery
        Achievement_MaxFull(10, 76, new int[] { BY_valueLvl, BY_speedLvl, BY_frequencyLvl }); //Max out the Battery refinery
        
        Achivement_True(11, PB_Unlocked); //Unlock Petbottle refinery
        Achivement_True(12, BX_Unlocked); //Unlock Box refinery
        Achivement_True(13, GL_Unlocked); //Unlock Glass refinery
        Achivement_True(14, BY_Unlocked); //Unlock Baattery refinery
        
        Achievement_General(15, requirements7, PB_soldAmount); //Earn x Raccoins in total with PetBottles
        Achievement_General(16, requirements7, BX_soldAmount); //Earn x Raccoins in total with Boxes
        Achievement_General(17, requirements7, GL_soldAmount); //Earn x Raccoins in total with Glasses
        Achievement_General(18, requirements7, BY_soldAmount); //Earn x Raccoins in total with Batteries
        
        Achievement_value(19, new int[] { 50, 75, 100 }, PB_valueLvl, PB_defValue); //Upgrade the Petbottles value to n Raccoins
        Achievement_value(20, new int[] { 75, 100, 125, 150, 175, 200, 225 }, BX_valueLvl, BX_defValue); //Upgrade the Boxes value to n Raccoins
        Achievement_value(21, new int[] { 150, 200, 250, 300, 350, 400, 450 }, GL_valueLvl, GL_defValue); //Upgrade the Glasses value to n Raccoins
        Achievement_value(22, new int[] { 300, 400, 500, 600, 700, 800, 900 }, BY_valueLvl, BY_defValue); //Upgrade the Battery value to n Raccoins
    }

    void refreshAchievement(int AchNumber, int a)
    {
        string[] ab = achievementProgress[AchNumber].Split("_");
        if (a > int.Parse(ab[0]))
        {
            notifyWindow();
            achievementProgress[AchNumber] = $"{a}_{ab[1]}";
        }
    }

    void Achievement_General(int AchNumber, int[] requirements, float control) //achnumber -> achievment száma, requirements -> számok amikre ellenőrzi, control-> a szám amit a requirements-hez hasonlít
    {
        int a = 0;
        for (int i = 0; i < requirements.Length; i++)
        {
            if (!(control < requirements[i]))
            {
                a = i;
            }
        }
        refreshAchievement(AchNumber, a);
    }

    void Achievement_Any(int AchNumber, int[] requirements, int[] controls) //achnumber -> achievment száma, requirements -> számok amikre ellenőrzi, control-> a számok amit a requirements-hez hasonlít / legalább egyik jó legyen
    {
        int a = 0;
        for (int i = 0; i < controls.Length; i++)
        {
            for (int j = 0; j < requirements.Length; j++)
            {
                if ( !(controls[i] < requirements[j]) )
                {
                    a = Mathf.Max(a, j);
                }
            }
        }
        refreshAchievement(AchNumber, a);
    }

    void Achievement_All(int AchNumber, int[] requirements, int[] controls) //achnumber -> achievment száma, requirements -> számok amikre ellenőrzi, control-> a számok amik összegét a requirements-hez hasonlítja
    {
        int a = 0; int sum = 0;
        for (int j = 0; j < controls.Length; j++)
        {
            sum += controls[j];
        }
        for (int i = 0; i < requirements.Length; i++)
        {
            if (sum >= requirements[i])
            {
                a = i;
            }
        }
        refreshAchievement(AchNumber, a);
    }

    void Achievement_MaxFull(int AchNumber, int requirement, int[] controls) //achnumber -> achievment száma, requirement -> elérendő feltétel, control-> a számok amiket ellenőriz, osszesnek meg kell felelnie
    {
        int a = 1; int count = 0;
        for (int j = 0; j < controls.Length; j++)
        {
            if(requirement == controls[j])
            {
                count++;
            }
        }
        if(count == controls.Length)
        {
            refreshAchievement(AchNumber, a);
        }
    }

    void Achivement_True(int AchNumber, bool control) //achnumber -> achievment száma, control-> megnézi h igaz-e
    {
        if (control)
        {
            refreshAchievement(AchNumber, 1);
        }
    }

    void Achievement_value(int AchNumber, int[] requirements, int control, float defValue)  //achnumber -> achievment száma, requirements -> számok amikre ellenőrzi, control -> szintje a value-nak, defValue-> alap értéke a szemétnek
    {
        float value = defValue * Mathf.Pow(multiplier, control);
        int a = 0;
        for (int i = 0; i < requirements.Length; i++)
        {
            if ( !(value < requirements[i]) )
            {
                a = i;
            }
        }
        refreshAchievement(AchNumber, a);
    }
}
