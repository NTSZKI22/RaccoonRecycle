using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsBehavior : MonoBehaviour
{
    IncomeNumbers incomeScript;

    [SerializeField]
    public AudioMixer audioMixer;
    [SerializeField]
    public Slider volumeSlider;

    public TMP_Dropdown resolutionsDropdown;

    private string master = "VolumeMaster";

    Resolution[] resolutions;

    bool isFullScreen;
    bool isOnDisplay;

    List<string> options = new List<string>();

    void Awake()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Start()
    {
        incomeScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<IncomeNumbers>();
        isFullScreen = true;
        isOnDisplay = true;

        resolutions = Screen.resolutions;

        Screen.SetResolution(1920, 1080, Screen.fullScreen);

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }
        }

        int s = 0;
        foreach (var item in options)
        {
            string[] res = item.Split("x");

            if (int.Parse(res[0]) == Screen.currentResolution.width && int.Parse(res[1]) == Screen.currentResolution.height)
            {
                currentResolutionIndex = s;
            }
            s++;
        }

        resolutionsDropdown.ClearOptions();
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    
    public void setResolution()
    {
        string[] res = options[resolutionsDropdown.value].Split("x");
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(master, volume);
    }

    public void SetFullscreen()
    {
        isFullScreen = !isFullScreen;
        Debug.Log(isFullScreen);
        Screen.fullScreen = isFullScreen;
    }

    public void DisplayIncome()
    {
        isOnDisplay = !isOnDisplay;
        Debug.Log(isOnDisplay);
        incomeScript.toggleOn(isOnDisplay);
    }
}
