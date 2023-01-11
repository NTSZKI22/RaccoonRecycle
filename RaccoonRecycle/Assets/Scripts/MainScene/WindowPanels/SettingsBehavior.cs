using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsBehavior : MonoBehaviour
{
    [SerializeField]
    public AudioMixer audioMixer;
    [SerializeField]
    public Slider volumeSlider;

    public TMP_Dropdown resolutionsDropdown;

    private string master = "VolumeMaster";

    Resolution[] resolutions;

    void Awake()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    void Start()
    {
        resolutions = Screen.resolutions;

        Screen.SetResolution(2560, 1440, Screen.fullScreen);

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.ClearOptions();
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

        
    }

    
    public void setResolution()
    {
        //Resolution resolution = resolutions[resolutionsDropdown.value]
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(master, volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        //Screen.fullScreen = isFullScreen;
        //Debug.Log(isFullScreen);
    }
    
}
