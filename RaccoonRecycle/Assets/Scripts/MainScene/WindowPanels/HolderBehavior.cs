using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderBehavior : MonoBehaviour
{
    DatabaseCommunication dataScript;

    public GameObject Unlock_PB;
    public GameObject Upgrade_PB;
    public GameObject Conveyor_PB;

    public GameObject Unlock_BX;
    public GameObject Upgrade_BX;
    public GameObject Conveyor_BX;

    public GameObject Unlock_GL;
    public GameObject Upgrade_GL;
    public GameObject Conveyor_GL;

    public GameObject Unlock_BY;
    public GameObject Upgrade_BY;
    public GameObject Conveyor_BY;

    public GameObject DefSeller1;
    public GameObject DefSeller2;
    public GameObject DefSeller3;
    public GameObject DefSeller4;

    bool PBUnlocked;
    bool BXUnlocked;
    bool GLUnlocked;
    bool BYUnlocked;

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        defaultStart();
    }

    public void getData()
    {
        PBUnlocked = dataScript.giveTrashStatus("PetBottle");
        BXUnlocked = dataScript.giveTrashStatus("Box");
        GLUnlocked = dataScript.giveTrashStatus("Glass");
        BYUnlocked = dataScript.giveTrashStatus("Battery");

        Debug.Log(PBUnlocked);
        Debug.Log(BXUnlocked);
        Debug.Log(GLUnlocked);
        Debug.Log(BYUnlocked);
        Debug.Log("holderbehavior, getdata");
    }

    public void defaultStart()
    {
        Unlock_PB.SetActive(true);

        Unlock_BX.SetActive(false);
        Unlock_GL.SetActive(false);
        Unlock_BY.SetActive(false);

        Upgrade_PB.SetActive(false);
        Upgrade_BX.SetActive(false);
        Upgrade_GL.SetActive(false);
        Upgrade_BY.SetActive(false);

        
        Conveyor_PB.SetActive(false);
        Conveyor_BX.SetActive(false);
        Conveyor_GL.SetActive(false);
        Conveyor_BY.SetActive(false);

        PBUnlocked = false;
        GLUnlocked = false;
        BXUnlocked = false;
        BYUnlocked = false;
    }
    
    public void loadedStart()
    {
        if (PBUnlocked) { petbottleUnlock(); }
        if (BXUnlocked) { boxUnlock(); }
        if (GLUnlocked) { glassUnlock(); }
        if (BYUnlocked) { batteryUnlock(); }
    }

    public void petbottleUnlock()
    {
        Unlock_PB.SetActive(false);
        Upgrade_PB.SetActive(true);
        Unlock_BX.SetActive(true);
        Conveyor_PB.SetActive(true);
    }

    public void boxUnlock()
    {
        Unlock_BX.SetActive(false);
        Upgrade_BX.SetActive(true);
        Unlock_GL.SetActive(true);
        Conveyor_BX.SetActive(true);
        DefSeller1.SetActive(false);
    }

    public void glassUnlock()
    {
        Unlock_GL.SetActive(false);
        Upgrade_GL.SetActive(true);
        Unlock_BY.SetActive(true);
        Conveyor_GL.SetActive(true);
        DefSeller2.SetActive(false);
    }

    public void batteryUnlock()
    {
        Unlock_BY.SetActive(false);
        Upgrade_BY.SetActive(true);
        Conveyor_BY.SetActive(true);
        DefSeller3.SetActive(false);
    }
}
