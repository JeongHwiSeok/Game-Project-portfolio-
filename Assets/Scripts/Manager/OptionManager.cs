using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Text resolution;

    [SerializeField] Button backButton;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Toggle[] soundToggle;
    [SerializeField] Slider[] soundSlider;

    [SerializeField] CanvasScaler canvasScaler;

    private void Awake()
    {
        backButton.onClick.AddListener(Esc);
        leftButton.onClick.AddListener(SizeDown);
        rightButton.onClick.AddListener(SizeUp);

        canvasScaler = GameObject.Find("Canvas").transform.GetComponent<CanvasScaler>();
        resolution.text = GameManager.instance.canvasScaler.x.ToString() + " X " + GameManager.instance.canvasScaler.y.ToString();

        soundToggle[0].onValueChanged.AddListener(MainVolume);
        soundToggle[1].onValueChanged.AddListener(BackVolume);

        for (int i = 0; i < 3; i++)
        {
            soundSlider[i].value = GameManager.instance.volume[i];
        }
    }

    private void Update()
    {
        AudioManager.instance.SoundValue(soundSlider[0].value, soundSlider[1].value, soundSlider[2].value);
    }

    private void Esc()
    {
        Destroy(gameObject);
    }

    private void SizeUp()
    {
        float x = canvasScaler.referenceResolution.x;

        switch (x)
        {
            case 1280:
                canvasScaler.referenceResolution = new Vector2(1600, 900);
                resolution.text = "1600 X 900";
                break;
            case 1600:
                canvasScaler.referenceResolution = new Vector2(1920, 1080);
                resolution.text = "1920 X 1080";
                break;
            case 1920:
                canvasScaler.referenceResolution = new Vector2(2560, 1440);
                resolution.text = "2560 X 1440";
                break;
            case 2560:
                break;
        }
        GameManager.instance.canvasScaler = canvasScaler.referenceResolution;

        DataManager.instance.data.canvasScalerSize[0] = canvasScaler.referenceResolution.x;
        DataManager.instance.data.canvasScalerSize[1] = canvasScaler.referenceResolution.y;

        DataManager.instance.Save();
    }

    private void SizeDown()
    {
        float x = canvasScaler.referenceResolution.x;

        switch (x)
        {
            case 1280:
                break;
            case 1600:
                canvasScaler.referenceResolution = new Vector2(1280, 720);
                resolution.text = "1280 X 720";
                break;
            case 1920:
                canvasScaler.referenceResolution = new Vector2(1600, 900);
                resolution.text = "1600 X 900";
                break;
            case 2560:
                canvasScaler.referenceResolution = new Vector2(1920, 1080);
                resolution.text = "1920 X 1080";
                break;
        }
        GameManager.instance.canvasScaler = canvasScaler.referenceResolution;

        DataManager.instance.data.canvasScalerSize[0] = canvasScaler.referenceResolution.x;
        DataManager.instance.data.canvasScalerSize[1] = canvasScaler.referenceResolution.y;

        DataManager.instance.Save();
    }

    private void MainVolume(bool flag)
    {
        if (flag)
        {
            AudioManager.instance.MainSoundOn();
        }
        else
        {
            AudioManager.instance.MainSoundOff();
        }
    }

    private void BackVolume(bool flag)
    {
        if (flag)
        {
            AudioManager.instance.MainSoundOn();
        }
        else
        {
            AudioManager.instance.MainSoundOff();
        }
    }

    private void EffectVolume()
    {

    }
}
