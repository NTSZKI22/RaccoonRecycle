using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    DatabaseCommunication dataScript;

    float defValue;
    float defSpeed;
    float defFrequency;

    int valueLvl;
    int speedLvl;
    int frequencylvl;

    float multiplierPos;
    float multiplierNeg;

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        multiplierPos = 1.02f;
        multiplierNeg = 0.98f;

        defProperties();
    }

    void Update()
    {
        getLevels();
    }

    public void defProperties()
    {
        if (this.gameObject.tag == "PetBottle")
        {
            defSpeed = 200; 
            defValue = 25;
            defFrequency = 2;
        }
        if (gameObject.tag == "Box")
        {
            defSpeed = 100;
            defValue = 50;
            defFrequency = 3;
        }
        if (gameObject.tag == "Glass")
        {
            defSpeed = 90;
            defValue = 100;
            defFrequency = 4;
        }
        if (gameObject.tag == "Battery")
        {
            defSpeed = 80;
            defValue = 200;
            defFrequency = 6;
        }
    }

    void getLevels()
    {
        if (gameObject.tag == "PetBottle")
        {
            speedLvl = dataScript.PB_speedLvl;
            valueLvl = dataScript.PB_valueLvl;
            frequencylvl = dataScript.PB_frequencyLvl;
        }
        if (gameObject.tag == "Box")
        {
            speedLvl = dataScript.BX_speedLvl;
            valueLvl = dataScript.BX_valueLvl;
            frequencylvl = dataScript.BX_frequencyLvl;
        }
        if (gameObject.tag == "Glass")
        {
            speedLvl = dataScript.GL_speedLvl;
            valueLvl = dataScript.GL_valueLvl;
            frequencylvl = dataScript.GL_frequencyLvl;
        }
        if (gameObject.tag == "Battery")
        {
            speedLvl = dataScript.BY_speedLvl;
            valueLvl = dataScript.BY_valueLvl;
            frequencylvl = dataScript.BY_frequencyLvl;
        }
    }

    public float value()
    {
        return defValue * Mathf.Pow(multiplierPos, valueLvl);
    }

    public float valueDef()
    {
        return defValue;
    }

    public float speed()
    {
        return defSpeed * Mathf.Pow(multiplierPos, speedLvl);
    }

    public float frequency()
    {
        return defFrequency * Mathf.Pow(multiplierNeg, frequencylvl);
    }

}
