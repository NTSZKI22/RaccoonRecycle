using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AchievementClass
{

    public string id { get; set; }
    public float normalCurrency_spent { get; set; }
    public float prestigeCurrency_spent { get; set; }
    [JsonProperty("gemCurrency")]
    public int gemCurrency { get; set; }
    public string[] achievementProgress { get; set; }
    public int itemLvl_1 { get; set; }
    public int itemLvl_2 { get; set; }
    public int itemLvl_3 { get; set; }
    public string usersId { get; set; }

}
