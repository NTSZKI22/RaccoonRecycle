using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    public GameObject parent;
    public Slider scrollSlider;

    public GameObject machinery; //def -75, -42
    public GameObject windows; //def -50

    public GameObject alap;

    public GameObject marker1;
    public GameObject marker2;
    public GameObject marker3;

    float machineDefPos;
    float windowsDefPos;
    float screenWidth;
    float screenWidth2;

    Vector2 locationDefMach;

    float postionValue;
    float sliderValue;

    bool PB_Unlocked;
    bool BX_Unlocked;
    bool GL_Unlocked;
    bool BY_Unlocked;

    int progress;

    // Start is called before the first frame update
    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense

        machineDefPos = machinery.transform.position.x;
        windowsDefPos = windows.transform.position.x;
        Debug.Log(locationDefMach);

        RectTransform rt = (RectTransform)alap.transform;
        screenWidth2 = rt.rect.width;
        screenWidth = Screen.currentResolution.width;

        scrollSlider.maxValue = 666;
    }

    // Update is called once per frame
    void Update()
    {
        PB_Unlocked = dataScript.giveTrashStatus("PetBottle");
        BX_Unlocked = dataScript.giveTrashStatus("Box");
        GL_Unlocked = dataScript.giveTrashStatus("Glass");
        BY_Unlocked = dataScript.giveTrashStatus("Battery");

        setMaxValue();
        machinery.transform.position = new Vector2(machineDefPos - scrollSlider.value, machinery.transform.position.y);
        windows.transform.position = new Vector2(windowsDefPos - scrollSlider.value , windows.transform.position.y);

    }

    void setMaxValue()
    {
        if(PB_Unlocked == false)
        {
            progress = 0;
        }
        else
        {
            if(BX_Unlocked == false)
            {
                progress = 1;
            }
            else
            {
                if(GL_Unlocked == false)
                {
                    progress = 2;
                }
                else
                {
                    if(BY_Unlocked == false)
                    {
                        progress = 3;
                    }
                    else
                    {
                        progress = 4;
                    }
                }
            }

        }



        screenWidth = Screen.currentResolution.width;
        switch (progress)
        {
            case 0: 
                scrollSlider.interactable = false;  
                break;
            case 1: 
                if(marker1.transform.position.x > screenWidth)
                {
                    scrollSlider.interactable = true;
                    scrollSlider.maxValue = ((marker1.transform.position.x - screenWidth2)/2);
                }
                else
                {
                    scrollSlider.interactable = false;
                }
                break;
            case 2:
                if (marker2.transform.position.x > screenWidth)
                {
                    scrollSlider.interactable = true;
                    scrollSlider.maxValue = ((marker2.transform.position.x - screenWidth2)/2);
                }
                else
                {
                    scrollSlider.interactable = false;
                }
                break;
            case 3:
            case 4:
                if (marker3.transform.position.x > screenWidth)
                {
                    scrollSlider.interactable = true;
                    scrollSlider.maxValue = ((marker3.transform.position.x - screenWidth2)/2);
                }
                else
                {
                    scrollSlider.interactable = false;
                }
                break;
            default: scrollSlider.interactable = true; break;
        }
    }

}
