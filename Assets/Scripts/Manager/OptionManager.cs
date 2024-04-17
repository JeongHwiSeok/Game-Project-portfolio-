using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Language
{
    English,
    Korean,
    Janpanes
}

public enum Resolution
{
    P720,
    P900,
    P1080,
    P1440,
}

public class OptionManager : Singleton<OptionManager>
{
    [SerializeField] FullScreenMode fullScreenMode;
    [SerializeField] Resolution resolutionSize;

    [SerializeField] Text resolution;

    [SerializeField] Toggle screenMode;

    [SerializeField] Button backButton;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Toggle[] soundToggle;
    [SerializeField] Slider[] soundSlider;

    [SerializeField] CanvasScaler canvasScaler;

    private void OnEnable()
    {
        backButton.onClick.AddListener(Esc);
        leftButton.onClick.AddListener(SizeDown);
        rightButton.onClick.AddListener(SizeUp);

        resolutionSize = GameManager.instance.resolution;
        fullScreenMode = DataManager.instance.data.screenMode;
        canvasScaler = GameObject.Find("Canvas").transform.GetComponent<CanvasScaler>();

        screenMode.onValueChanged.AddListener(ChangeWindowType);
        screenMode.isOn = DataManager.instance.data.fullScreenOnOff;

        soundToggle[0].onValueChanged.AddListener(MainVolume);
        soundToggle[0].isOn = DataManager.instance.data.soundOnOff[0];
        soundToggle[1].onValueChanged.AddListener(BackVolume);
        soundToggle[1].isOn = DataManager.instance.data.soundOnOff[1];

        for (int i = 0; i < 3; i++)
        {
            soundSlider[i].value = GameManager.instance.volume[i];
        }
    }

    private void Update()
    {
        resolution.text = canvasScaler.referenceResolution.x.ToString() + " X " + canvasScaler.referenceResolution.y.ToString();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SoundValue(soundSlider[0].value, soundSlider[1].value, soundSlider[2].value);
        }
        else if (SafeAudioManager.instance != null)
        {
            SafeAudioManager.instance.SoundValue(soundSlider[0].value, soundSlider[1].value, soundSlider[2].value);
        }
    }

    private void Esc()
    {
        Destroy(gameObject);
    }

    private void SizeUp()
    {
        switch (resolutionSize)
        {
            case Resolution.P720:
                resolutionSize = Resolution.P900;
                break;
            case Resolution.P900:
                resolutionSize = Resolution.P1080;
                break;
            case Resolution.P1080:
                resolutionSize = Resolution.P1440;
                break;
            case Resolution.P1440:
                break;
        }
        GameManager.instance.resolution = resolutionSize;
        GameManager.instance.ChangeResolution();

        DataManager.instance.data.resolution = resolutionSize;

        DataManager.instance.Save();
    }

    private void SizeDown()
    {
        switch (resolutionSize)
        {
            case Resolution.P720:
                break;
            case Resolution.P900:
                resolutionSize = Resolution.P720;
                break;
            case Resolution.P1080:
                resolutionSize = Resolution.P900;
                break;
            case Resolution.P1440:
                resolutionSize = Resolution.P1080;
                break;
        }
        GameManager.instance.resolution = resolutionSize;
        GameManager.instance.ChangeResolution();

        DataManager.instance.data.resolution = resolutionSize;

        DataManager.instance.Save();
    }

    private void ChangeWindowType(bool flag)
    {
        switch (flag)
        {
            case true:
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, fullScreenMode);
                DataManager.instance.data.fullScreenOnOff = true;
                break;
            case false:
                fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, fullScreenMode);
                DataManager.instance.data.fullScreenOnOff = false;
                break;
        }
        DataManager.instance.data.screenMode = fullScreenMode;
        DataManager.instance.Save();
    }

    private void MainVolume(bool flag)
    {
        if (flag)
        {
            AudioManager.instance.MainSoundOn();
            DataManager.instance.data.soundOnOff[0] = true;
        }
        else
        {
            AudioManager.instance.MainSoundOff();
            DataManager.instance.data.soundOnOff[0] = false;
        }
    }

    private void BackVolume(bool flag)
    {
        if (flag)
        {
            AudioManager.instance.MainSoundOn();
            DataManager.instance.data.soundOnOff[1] = true;
        }
        else
        {
            AudioManager.instance.MainSoundOff();
            DataManager.instance.data.soundOnOff[1] = false;
        }
    }

    private void EffectVolume()
    {

    }
}
