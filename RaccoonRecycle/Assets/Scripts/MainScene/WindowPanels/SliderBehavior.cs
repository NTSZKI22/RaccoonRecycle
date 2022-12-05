using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    public GameObject parent;
    public Slider scrollSlider;

    public GameObject machinery; //def -75, -42
    public GameObject windows; //def -50

    float machineDefPos;
    float windowsDefPos;

    Vector2 locationDefMach;

    float postionValue;
    float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        machineDefPos = machinery.transform.position.x;
        windowsDefPos = windows.transform.position.x;
        Debug.Log(locationDefMach);
    }

    // Update is called once per frame
    void Update()
    {
        machinery.transform.position = new Vector2(machineDefPos - scrollSlider.value, machinery.transform.position.y);
        windows.transform.position = new Vector2(windowsDefPos - scrollSlider.value , windows.transform.position.y);

    }

    void petBottleActive()
    {
        postionValue = 0;
    }

    void boxActive()
    {
        postionValue = 90;
    }

    void glassActive()
    {
        postionValue = 340;
    }

    void batteryActive()
    {
        postionValue = 385;
    }

}
