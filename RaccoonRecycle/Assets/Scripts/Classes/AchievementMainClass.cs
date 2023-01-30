using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMainClass
{
    [JsonProperty("Achievements")]
    public AchievementClass[] Achievements { get; set; }
}
