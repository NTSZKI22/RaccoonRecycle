using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderBehavior : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    public GameObject Unlock_PB; //petbottle feloldását intézõ ablak
    public GameObject Upgrade_PB; //petbottle fejlesztéseit tartalmazó ablak
    public GameObject Conveyor_PB; //petbottle-hoz tartozó futószalagelemek

    public GameObject Unlock_BX; //kartondoboz feloldását intézõ ablak
    public GameObject Upgrade_BX; //kartondoboz fejlesztéseit tartalmazó ablak
    public GameObject Conveyor_BX; //kartondobozhoz tartozó futószalagelemek

    public GameObject Unlock_GL; //üveg feloldását intézõ ablak
    public GameObject Upgrade_GL; //üveg fejlesztéseit tartalmazó ablak
    public GameObject Conveyor_GL; //üveghez tartozó futószalagelemek

    public GameObject Unlock_BY; //elem feloldását intézõ ablak
    public GameObject Upgrade_BY; //elem fejlesztéseit tartalmazó ablak
    public GameObject Conveyor_BY; //elemhez tartozó futószalagelemek

    //alap kukák objektumai
    public GameObject DefSeller1;
    public GameObject DefSeller2;
    public GameObject DefSeller3;
    public GameObject DefSeller4;

    //jelzi, hogy fel van-e oldva a bizonyos elem, funkciói a mentéskor -> lekérés és küldés,  futáskor -> kezdedti betöltés
    bool PBUnlocked;
    bool BXUnlocked;
    bool GLUnlocked;
    bool BYUnlocked;

    void Start() //a játék elindulásakor lefut
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

        defaultStart(); //alapértelmezett indulás
        getData(); //adatok elkérése
        loadedStart(); //betöltött adatokkal való indítás
    }

    void Update() //képfrissítésenként lefut
    {
        
    }

    public void getData() //metódus, lekéri az adatokat a szükséges változókba
    {
        //a datascriptbõl kivett adatokat
        PBUnlocked = dataScript.PB_Unlocked;
        BXUnlocked = dataScript.BX_Unlocked;
        GLUnlocked = dataScript.GL_Unlocked;
        BYUnlocked = dataScript.BY_Unlocked;

        Debug.Log(PBUnlocked);
        Debug.Log(BXUnlocked);
        Debug.Log(GLUnlocked);
        Debug.Log(BYUnlocked);
        Debug.Log("holder, getdata");
    }

    void defaultStart() //alapértelmezett indítási felállás (mikor 0.ról kezd)
    {
        //csak a petpalack feloldásához szükséges ablak aktív minden más nem akatív

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
    
    public void loadedStart() //meghívásával egy bizonyos mentés állását tölti be
    {
        if (PBUnlocked) { petbottleUnlock(); } //ha fel van oldva a petbottle akkor meghívja a metódust
        if (BXUnlocked) { boxUnlock(); }//ha fel van oldva a doboz akkor meghívja a metódust
        if (GLUnlocked) { glassUnlock(); } //ha fel van oldva az üveg akkor meghívja a metódust
        if (BYUnlocked) { batteryUnlock(); } //ha fel van oldva az elem akkor meghívja a metódust
    }

    public void petbottleUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - petpalack feloldása
    {
        Debug.Log("petbottle unlock");
        Unlock_PB.SetActive(false);
        Upgrade_PB.SetActive(true);

        Unlock_BX.SetActive(true);

        Conveyor_PB.SetActive(true);


        dataScript.unlock(1, true);
    }

    public void boxUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - kartondoboz feloldása
    {
        Debug.Log("Box unlock");
        Unlock_BX.SetActive(false);
        Upgrade_BX.SetActive(true);

        Unlock_GL.SetActive(true);

        Conveyor_BX.SetActive(true);
        DefSeller1.SetActive(false);
        

        dataScript.unlock(2, true);
    }

    public void glassUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - üveg feloldása
    {
        Unlock_GL.SetActive(false);
        Upgrade_GL.SetActive(true);

        Unlock_BY.SetActive(true);

        Conveyor_GL.SetActive(true);
        DefSeller2.SetActive(false);


        dataScript.unlock(3, true);
    }

    public void batteryUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - elem feloldása
    {
        Unlock_BY.SetActive(false);
        Upgrade_BY.SetActive(true);

        Conveyor_BY.SetActive(true);
        DefSeller3.SetActive(false);


        dataScript.unlock(4, true);
    }
}
