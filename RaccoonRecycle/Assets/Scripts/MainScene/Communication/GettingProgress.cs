using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingProgress : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    public GameObject Unlock_PB; //petbottle feloldását intézõ ablak
    public GameObject Upgrade_PB; //petbottle fejlesztéseit tartalmazó ablak

    public GameObject Unlock_BX; //kartondoboz feloldását intézõ ablak
    public GameObject Upgrade_BX; //kartondoboz fejlesztéseit tartalmazó ablak

    public GameObject Unlock_GL; //üveg feloldását intézõ ablak
    public GameObject Upgrade_GL; //üveg fejlesztéseit tartalmazó ablak

    public GameObject Unlock_BY; //elem feloldását intézõ ablak
    public GameObject Upgrade_BY; //elem fejlesztéseit tartalmazó ablak

    public bool PB_Unlocked;
    public bool BX_Unlocked;
    public bool GL_Unlocked;
    public bool BTY_Unlocked;

    public int progress;

    // Start is called before the first frame update
    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense
    }

    // Update is called once per frame
    void Update()
    {
        ProgressUpdate();
    }

    public void ProgressUpdate()
    {

        if (!Unlock_PB.active)
        {
            if (!Unlock_BX.active)
            {
                if (!Unlock_GL.active)
                {
                    if (!Unlock_BY.active)
                    {
                        progress = 4;
                    }
                    else
                    {
                        progress = 3;
                    }
                }
                else
                {
                    progress = 2;
                }
            }
            else
            {
                progress = 1;
            }
        }
        else
        {
            progress = 0;
        }
        
    }

    public int sendProgress()
    {
        return progress;
    }
}
