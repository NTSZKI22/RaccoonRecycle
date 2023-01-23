using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineEarning : MonoBehaviour
{
    public double OfflineEarnings(DateTime lastSaveDate)
    {
        DateTime now = DateTime.Now;
        TimeSpan? offlineHours = new TimeSpan();
        offlineHours = now.Subtract(lastSaveDate);
        return offlineHours.Value.TotalHours;
    }
}
