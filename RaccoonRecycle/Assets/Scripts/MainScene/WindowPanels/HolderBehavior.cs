using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderBehavior : MonoBehaviour
{
    public GameObject Unlock_PB; //petbottle feloldását intézõ ablak
    public GameObject Upgrade_PB; //petbottle fejlesztéseit tartalmazó ablak

    public GameObject Unlock_BX; //kartondoboz feloldását intézõ ablak
    public GameObject Upgrade_BX; //kartondoboz fejlesztéseit tartalmazó ablak

    public GameObject Unlock_GL; //üveg feloldását intézõ ablak
    public GameObject Upgrade_GL; //üveg fejlesztéseit tartalmazó ablak

    public GameObject Unlock_BY; //elem feloldását intézõ ablak
    public GameObject Upgrade_BY; //elem fejlesztéseit tartalmazó ablak



    void Start() //a játék elindulásakor lefut
    {
        defaultStart(); //lefut a defaultstart metódus
        
        petbottleUnlock();
        boxUnlock();
    }

    void Update() //képfrissítésenként lefut
    {
        
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

        /*
        GameObject.Find("lvl1_PetBottle").SetActive(false);
        GameObject.Find("lvl2_Box").SetActive(false);
        GameObject.Find("lvl3_Glass").SetActive(false);
        GameObject.Find("lvl4_Battery").SetActive(false);
        */
    }

    void petbottleUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - petpalack feloldása
    {
        Unlock_PB.SetActive(false);
        Upgrade_PB.SetActive(true);

        Unlock_BX.SetActive(true);

        GameObject.Find("lvl1_PetBottle").SetActive(true);
    }

    void boxUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - kartondoboz feloldása
    {
        Unlock_BX.SetActive(false);
        Upgrade_BX.SetActive(true);

        Unlock_GL.SetActive(true);

        GameObject.Find("lvl2_Box").SetActive(true);
        GameObject.Find("TrashCan_Main").SetActive(false);
    }

    void glassUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - üveg feloldása
    {
        Unlock_GL.SetActive(false);
        Upgrade_GL.SetActive(true);

        Unlock_BY.SetActive(true);

        GameObject.Find("lvl3_Glass").SetActive(true);
        GameObject.Find("TrashCan_Main2").SetActive(false);
    }

    void batteryUnlock() //metódus, meghívásával minden szükséges elem láthatósága változik - elem feloldása
    {
        Unlock_BY.SetActive(false);
        Upgrade_BY.SetActive(true);

        GameObject.Find("lvl4_Battery").SetActive(true);
        GameObject.Find("TrashCan_Main3").SetActive(false);
    }
}
