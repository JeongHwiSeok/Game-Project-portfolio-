using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float monsterSpeed = 1f;
    [SerializeField] private float characterSpeed = 1f;
    [SerializeField] private float weaponSpeed = 0.2f;

    [SerializeField] public bool state = true;

    public float MonsterSpeed
    {
        get { return monsterSpeed; }
        set { monsterSpeed = value; }
    }

    public float CharacterSpeed
    {
        get { return characterSpeed; }
        set { characterSpeed = value; }
    }

    public float WeaponSpeed
    {
        get { return weaponSpeed; }
        set { weaponSpeed = value; }
    }

    public void GameOver()
    {
        state = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
