using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Dropdown dropDown;
    
    private bool isFullScreen;
    private Resolution[] _rsl;
    private List<string> _resolutions;
    


    private void Awake()
    {
        _resolutions = new List<string>();
        _rsl = Screen.resolutions;
        foreach (var i in _rsl)
        {
            _resolutions.Add(i.width + "x" + i.height);
        }
        dropDown.ClearOptions();
        dropDown.AddOptions(_resolutions);
    }

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        audioMixer.SetFloat("masterVolume", sliderValue);
    }

    public void Quality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void Resolution(int index)
    {
        Screen.SetResolution(_rsl[index].width, _rsl[index].height, isFullScreen);
    }
}
