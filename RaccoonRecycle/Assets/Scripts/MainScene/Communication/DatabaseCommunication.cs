using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseCommunication : MonoBehaviour
{

    int userid;

    public float normalCurrency;
    public float prestigeCurrency;
    public float totalEarnings;

    public int PB_soldAmount;
    public bool PB_Unlocked;
    public int PB_valueLvl;
    public int PB_speedLvl;
    public int PB_frequencyLvl;

    public int BX_soldAmount;
    public bool BX_Unlocked;
    public int BX_valueLvl;
    public int BX_speedLvl;
    public int BX_frequencyLvl;

    public int GL_soldAmount;
    public bool GL_Unlocked;
    public int GL_valueLvl;
    public int GL_speedLvl;
    public int GL_frequencyLvl;

    public int BY_soldAmount;
    public bool BY_Unlocked;
    public int BY_valueLvl;
    public int BY_speedLvl;
    public int BY_frequencyLvl;

    void Start()
    {
        //ideiglenesen:
        userid = -1;
        getData();
    }

    // Update is called once per frame
    void Update()
    {
        saveData();
    }

    void getData()
    {
        //adatok lekérése

        if(userid == -1)
        {
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
        }
    }

    void saveData()
    {

    }

    public void pbValue()
    {
        PB_valueLvl++;
    }

}
