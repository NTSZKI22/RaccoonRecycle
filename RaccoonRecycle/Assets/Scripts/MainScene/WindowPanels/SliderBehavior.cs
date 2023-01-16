using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    GettingProgress progressScript; // a feloldott haladást jelzi vissz

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

    // Start is called before the first frame update
    void Start()
    {
        progressScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<GettingProgress>(); //a scriptet kiveszi az adott objektumból mint komponense

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
        setMaxValue();
        machinery.transform.position = new Vector2(machineDefPos - scrollSlider.value, machinery.transform.position.y);
        windows.transform.position = new Vector2(windowsDefPos - scrollSlider.value , windows.transform.position.y);

    }

    void setMaxValue()
    {
        screenWidth = Screen.currentResolution.width;
        switch (progressScript.sendProgress())
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
