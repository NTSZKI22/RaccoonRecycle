using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    DatabaseCommunication dataScript;

    public GameObject parent;
    public Slider scrollSlider;

    public GameObject machinery;
    public GameObject windows;

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

    void Start()
    {
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();

        machineDefPos = machinery.transform.position.x;
        windowsDefPos = windows.transform.position.x;

        RectTransform rt = (RectTransform)alap.transform;
        screenWidth2 = rt.rect.width;
        screenWidth = Screen.currentResolution.width;

        scrollSlider.maxValue = 666;
    }

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

    void selectMax(GameObject marker)
    {
        switch (marker.transform.position.x > screenWidth)
        {
            case true:
                scrollSlider.interactable = true;
                scrollSlider.maxValue = ((marker.transform.position.x - screenWidth2) / 2);
                break;
            case false: scrollSlider.interactable = false; break;
        }
    }

    void setMaxValue()
    {
        screenWidth = Screen.currentResolution.width;
        if (PB_Unlocked == false)
        {
            scrollSlider.interactable = false;
        }
        else
        {
            if(BX_Unlocked == false)
            {
                selectMax(marker1);
            }
            else
            {
                if(GL_Unlocked == false)
                {
                    selectMax(marker2);
                }
                else
                {
                    selectMax(marker3);
                }
            }
        }

    }
}
