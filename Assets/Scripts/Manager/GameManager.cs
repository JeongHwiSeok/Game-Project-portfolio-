using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float characterSpeed;
    [SerializeField] private float monsterSpeed;

    [SerializeField] public string characterName;

    [SerializeField] public bool state = true;
    [SerializeField] public bool monsterSpawn = true;

    [SerializeField] public CharacterNumber charNumber;
    public int charNum;

    [SerializeField] public GameObject player;

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

    public void CharacterNumberCheck()
    {
        switch (charNumber)
        {
            case CharacterNumber.Aoi:
                charNum = 0;
                break;
            case CharacterNumber.Iku:
                charNum = 1;
                break;
            case CharacterNumber.Meno:
                charNum = 2;
                break;
        }
    }
    
    public void GameStart()
    {
        switch (charNumber)
        {
            case CharacterNumber.Aoi:
                characterName = "Tokimori Aoi";
                player = Instantiate(Resources.Load<GameObject>("Tokimori Aoi"));
                break;
            case CharacterNumber.Iku:
                characterName = "Hoshifuri Iku";
                player = Instantiate(Resources.Load<GameObject>("Hoshifuri Iku"));
                break;
            case CharacterNumber.Meno:
                characterName = "Ibuki Meno";
                player = Instantiate(Resources.Load<GameObject>("Ibuki Meno"));
                break;
        }
    }

    public void GameOver()
    {
        state = false;
        monsterSpawn = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;
        if(SceneManager.GetActiveScene().name == "PlayScene")
        {
            GameStart();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
