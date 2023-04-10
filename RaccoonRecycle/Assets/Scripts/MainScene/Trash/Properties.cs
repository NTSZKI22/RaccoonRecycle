using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Properties : MonoBehaviour
{
    DatabaseCommunication dataScript;
    FixData fixDataScript;

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
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();

        multiplierPos = fixDataScript.multiplierPos;
        multiplierNeg = fixDataScript.multiplierNeg;

        defProperties();
        
    }

    void Update()
    {
        getLevels();
    }

    public void defProperties()
    {
        fixDataScript = GameObject.FindGameObjectWithTag("FixData").GetComponent<FixData>();
        defSpeed = fixDataScript.giveTrashProperties(gameObject.tag, "Speed");
        defValue = fixDataScript.giveTrashProperties(gameObject.tag, "Value");
        defFrequency = fixDataScript.giveTrashProperties(gameObject.tag, "Frequency");
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
