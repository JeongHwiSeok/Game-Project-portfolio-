using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SoundScene
{
    Title,
    Shop,
    CharacterScene,
    CharacterSelect,
    Play,
}

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] SoundScene soundScene;
    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioSource scenerySource;

    [SerializeField] AudioClip mainSound;

    [SerializeField] AudioClip[] playSound;

    private void Start()
    {
        scenerySource.volume = GameManager.instance.volume[0] * GameManager.instance.volume[1];
        effectSource.volume = GameManager.instance.volume[0] * GameManager.instance.volume[2];

        SoundOnOffCheck();
    }

    private void SoundOnOffCheck()
    {
        if (GameManager.instance.soundOnOff[0])
        {
            MainSoundOff();
        }
        else
        {
            MainSoundOn();
            scenerySource.mute = GameManager.instance.soundOnOff[1];
            effectSource.mute = GameManager.instance.soundOnOff[2];
        }
    }

    public void EffectSound(AudioClip audioClip)
    {
        effectSource.PlayOneShot(audioClip);
    }

    public void MainSoundOn()
    {
        effectSource.mute = false;
        scenerySource.mute = false;
    }

    public void MainSoundOff()
    {
        effectSource.mute = true;
        scenerySource.mute = true;
    }

    public void BackSoundOn()
    {
        scenerySource.mute = false;
    }

    public void BackSoundOff()
    {
        scenerySource.mute = true;
    }

    public void SoundValue(float a, float b, float c)
    {
        scenerySource.volume = a * b;
        effectSource.volume = a * c;

        GameManager.instance.volume[0] = a;
        GameManager.instance.volume[1] = b;
        GameManager.instance.volume[2] = c;

        DataManager.instance.data.volume[0] = a;
        DataManager.instance.data.volume[1] = b;
        DataManager.instance.data.volume[2] = c;

        DataManager.instance.Save();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scenerySource.clip = mainSound;

        scenerySource.loop = true;

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            scenerySource.clip = playSound[GameManager.instance.playChapterNumber];

            scenerySource.loop = true;

            scenerySource.Play();
        }
        else
        {
            if (scenerySource.isPlaying)
            {
                return;
            }
            else
            {
                scenerySource.Play();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
