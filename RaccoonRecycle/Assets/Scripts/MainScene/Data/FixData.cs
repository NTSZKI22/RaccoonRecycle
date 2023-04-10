using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class FixData : MonoBehaviour
{
    public float multiplierPos = 1.02f;
    public float multiplierNeg = 0.98f;
    public float multiplierOffline = 0.4f;
    public float prestigeDivide = 100f;

    public float defaultValue = 15f;

    public float PB_defValue = 25f;
    public float BX_defValue = 50f;
    public float GL_defValue = 100f;
    public float BY_defValue = 200f;

    public float PB_defFrequency = 2f;
    public float BX_defFrequency = 3f;
    public float GL_defFrequency = 4f;
    public float BY_defFrequency = 6f;

    public float PB_defSpeed = 200f;
    public float BX_defSpeed = 100f;
    public float GL_defSpeed = 90f;
    public float BY_defSpeed = 80f;

    public float PB_ValueDefCost = 50f;
    public float PB_SpeedDefCost = 25f;
    public float PB_FrequencyDefCost = 15f;

    public float BX_ValueDefCost = 100f;
    public float BX_SpeedDefCost = 50f;
    public float BX_FrequencyDefCost = 30f;

    public float GL_ValueDefCost = 200f;
    public float GL_SpeedDefCost = 100f;
    public float GL_FrequencyDefCost = 60f;

    public float BY_ValueDefCost = 400f;
    public float BY_SpeedDefCost = 200f;
    public float BY_FrequencyDefCost = 120f;

    public int maxLevel = 75;

    public float cost_UnlockPB = 50;
    public float cost_UnlockBX = 1000;
    public float cost_UnlockGL = 15000;
    public float cost_UnlockBY = 200000;

    public int[] cost_1 = new int[] { 100, 225, 325 };
    public string[] details_1 = new string[] { "15 min -> 30 min", "30 min -> 1 hr", "1 hr -> 1,5 hr" };

    public int[] cost_2 = new int[] { 85, 175, 225, 260, 340 };
    public string[] details_2 = new string[] { "0 -> 25%", "25% -> 50%", "50% -> 75%", "75% -> 100%", "100% -> 125%" };

    public int[] cost_3 = new int[] { 155, 275 };
    public string[] details_3 = new string[] { "0 -> 50%", "50% -> 100%" };

    public int[] requirements7 = new int[] { 100, 1000, 10000, 100000, 1000000, 10000000, 100000000 };

    public string[] achievementTexts = new string[]
    {
        "2 Earn x Raccoins in total",
        "2 Have c Racoonium",
        "2 Spend x Raccoins",
        "2 Spend x Racoonium",
        "3 Reach level y with any upgrades",
        "5 Reach a total of z level with upgrades",
        "0 Max out any upgrade",
        "0 Max out the Petbottle refinery",
        "0 Max out the Box refinery",
        "0 Max out the Glass refinery",
        "0 Max out the Battery refinery",
        "0 Unlock Petbottle refinery",
        "0 Unlock Box refinery",
        "0 Unlock Glass refinery",
        "0 Unlock Battery refinery",
        "2 Earn x Raccoins in total with PetBottles",
        "2 Earn x Raccoins in total with Boxes",
        "2 Earn x Raccoins in total with Glasses",
        "2 Earn x Raccoins in total with Batteries",
        "6 Upgrade the Petbottles value to n Raccoins",
        "6 Upgrade the Boxes value to n Raccoins",
        "6 Upgrade the Glasses value to n Raccoins",
        "6 Upgrade the Battery value to n Raccoins"
    };

    public string[] achievementProgress = new string[] { "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0" };

    void Start()
    {
        Debug.Log($"speeds: {PB_defSpeed} {BX_defSpeed} {GL_defSpeed} {BY_defSpeed}");
    }
    
    public float gemshopValueMultiplier(int itemLvl)
    {
        switch (itemLvl)
        {
            case 1: return 1.25f;
            case 2: return 1.5f;
            case 3: return 1.75f;
            case 4: return 2f;
            case 5: return 2.25f;
            default: return 1f;
        }
    }

    public float gemshopPrestigeMultiplier(int itemLvl)
    {
        switch (itemLvl)
        {
            case 1: return 1.5f;
            case 2: return 2f;
            default: return 1f;
        }
    }

    public float giveTrashProperties(string Trash, string Property)
    {
        switch (Trash)
        {
            case "PetBottle":
                switch (Property)
                {
                    case "Speed": return 200f; //return PB_defSpeed;
                    case "Value": return 25f;
                    case "Frequency": return 2f;
                    default: Debug.Log($"{Trash} {Property} default");  return 50f;
                }
                break;
            case "Box":
                switch (Property)
                {
                    case "Speed": return 100f; //return BX_defSpeed;
                    case "Value": return 50f;
                    case "Frequency": return 3f;
                    default: Debug.Log($"{Trash} {Property} default"); return 50f;
                }
                break;
            case "Glass":
                switch (Property)
                {
                    case "Speed": return 90f; //return GL_defSpeed;
                    case "Value": return 100f;
                    case "Frequency": return 4f;
                    default: Debug.Log($"{Trash} {Property} default");  return 50f;
                }
                break;
            case "Battery":
                switch (Property)
                {
                    case "Speed": return 80f; //return BY_defSpeed;
                    case "Value": return 200f;
                    case "Frequency": return 6f;
                    default: Debug.Log($"{Trash} {Property} default");  return 50f;
                }
                break;
            default:
                Debug.Log($"{Trash} default");
                switch (Property)
                {
                    case "Speed": return 200f; 
                    case "Value": return 25f;
                    case "Frequency": return 2f;
                    default: Debug.Log($"{Trash} {Property} default");  return 50f;
                }
                break;
        }
    }

    public float giveUpgradeProperties(string Trash, string Property)
    {
        switch (Trash)
        {
            case "PetBottleU":
                switch (Property)
                {
                    case "Speed": return PB_SpeedDefCost;
                    case "Value": return PB_ValueDefCost;
                    case "Frequency": return PB_FrequencyDefCost;
                }
                break;
            case "BoxU":
                switch (Property)
                {
                    case "Speed": return BX_SpeedDefCost;
                    case "Value": return BX_ValueDefCost;
                    case "Frequency": return BX_FrequencyDefCost;
                }
                break;
            case "GlassU":
                switch (Property)
                {
                    case "Speed": return GL_SpeedDefCost;
                    case "Value": return GL_ValueDefCost;
                    case "Frequency": return GL_FrequencyDefCost;
                }
                break;
            case "BatteryU":
                switch (Property)
                {
                    case "Speed": return BY_SpeedDefCost;
                    case "Value": return BY_ValueDefCost;
                    case "Frequency": return BY_FrequencyDefCost;
                }
                break;
        }
        return 0f;
    }
}
