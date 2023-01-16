using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public class SaveClass
    {
        //{"id":"63861a1625b38ec4ea98f618","usersId":"63861a1525b38ec4ea98f617","lastSaveDate":"1669732885526","normalCurrency":0,
        //"prestigeCurrency":0,"totalEarnings":0,"pbUnlocked":false,"pbSoldAmount":0,"pbValue":0,"pbSpeed":0,"pbFrequency":0,"bxUnlocked":false,"bxSoldAmount":0,"bxValue":0,"bxSpeed":0,
        //"bxFrequency":0,"glUnlocked":false,"glSoldAmount":0,
        //"glValue":0,"glSpeed":0,"glFrequency":0,"byUnlocked":false,"bySoldAmount":0,"byValue":0,"bySpeed":0,"byFrequency":0}
        public string id;
        public string usersId;
        public string lastSaveDate;
        public int normalCurrency;
        public int prestigeCurrency;
        public int totalEarnings;

        public bool pbUnlocked;
        public int pbSoldAmount;
        public int pbValue;
        public int pbSpeed;
        public int pbFrequency;

        public bool bxUnlocked;
        public int bxSoldAmount;
        public int bxValue;
        public int bxSpeed;
        public int bxFrequency;

        public bool glUnlocked;
        public int glSoldAmount;
        public int glValue;
        public int glSpeed;
        public int glFrequency;

        public bool byUnlocked;
        public int bySoldAmount;
        public int byValue;
        public int bySpeed;
        public int byFrequency;

    }

}