using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    #region Global
    [SerializeField] public CanvasScaler canvasScaler;
    [SerializeField] public Resolution resolution;
    [SerializeField] public float[] volume;

    [SerializeField] public int playChapterNumber;
    [SerializeField] public CharacterNumber charNumber;
    public int charNum;

    [SerializeField] GameObject audioSelect;
    [SerializeField] bool audioSelectCheck;

    [SerializeField] public bool[] soundOnOff;
    #endregion

    #region Play
    [SerializeField] private float characterSpeed;
    [SerializeField] private float monsterSpeed;

    [SerializeField] public int attackLV;

    [SerializeField] public bool state = true;
    [SerializeField] public bool monsterSpawn = true;
    [SerializeField] public int weaponcount;
    [SerializeField] public int supportcount;

    [SerializeField] public int[] itemLvCheck;
    [SerializeField] public bool[] itemNumberCheck;

    [SerializeField] public GameObject player;

    [SerializeField] public int monsterCount;

    [SerializeField] public int ikuminCount;

    [SerializeField] public float time;
    [SerializeField] public int cnCount;

    [SerializeField] public List<GameObject> weaponItemList;
    [SerializeField] public List<GameObject> supportItemList;
    #endregion

    public float MonsterSpeed
    {
        get { return monsterSpeed; }
        set { monsterSpeed = value; }
    }
    public float CharacterSpeed
    {
        get { return characterSpeed * BuffDebuffManager.instance.aoiP1SpeedBuff * BuffDebuffManager.instance.pwsSpeedBuff; }
        set { characterSpeed = value; }
    }

    private void Update()
    {
        if (state)
        {
            time += Time.deltaTime;
        }
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
                player = Instantiate(Resources.Load<GameObject>("Tokimori Aoi"));
                break;
            case CharacterNumber.Iku:
                player = Instantiate(Resources.Load<GameObject>("Hoshifuri Iku"));
                break;
            case CharacterNumber.Meno:
                player = Instantiate(Resources.Load<GameObject>("Ibuki Meno"));
                break;
        }
        time = 0;
        monsterCount = 0;
        ikuminCount = 0;
        cnCount = 0;
        attackLV = 1;
    }

    public void GameOver()
    {
        state = false;
        monsterSpawn = false;
        DataManager.instance.data.shopCoin += UIManager.instance.DropCoin;
        DataManager.instance.Save();
        GameObject obj = Instantiate(Resources.Load<GameObject>("PreFabs/UI/GameOver"));
        obj.GetComponent<GameOverPanel>().result.text = "Game Over";
        attackLV = 1;
    }

    public void GameClear()
    {
        state = false;
        monsterSpawn = false;
        DataManager.instance.data.shopCoin += UIManager.instance.DropCoin;
        DataManager.instance.Save();
        GameObject obj = Instantiate(Resources.Load<GameObject>("PreFabs/UI/GameOver"));
        obj.GetComponent<GameOverPanel>().result.text = "Game Clear";
        attackLV = 1;
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeResolution()
    {
        Debug.Log(resolution);
        switch (resolution)
        {
            case Resolution.P720:
                canvasScaler.referenceResolution = new Vector2(1280, 720);
                Debug.Log("check1");
                break;
            case Resolution.P900:
                canvasScaler.referenceResolution = new Vector2(1600, 900);
                Debug.Log("check2");
                break;
            case Resolution.P1080:
                canvasScaler.referenceResolution = new Vector2(1920, 1080);
                Debug.Log("check3");
                break;
            case Resolution.P1440:
                canvasScaler.referenceResolution = new Vector2(2560, 1440);
                Debug.Log("check4");
                break;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;

        canvasScaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        resolution = DataManager.instance.data.resolution;

        for (int i = 0; i < 3; i++)
        {
            soundOnOff[i] = DataManager.instance.data.soundOnOff[i];
        }

        ChangeResolution();
        Screen.SetResolution((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, DataManager.instance.data.screenMode);

        for (int i = 0; i < 3; i++)
        {
            volume[i] = DataManager.instance.data.volume[i];
        }
        if (SceneManager.GetActiveScene().name == "MainScene" && audioSelectCheck == false)
        {
            Instantiate(audioSelect);
            audioSelectCheck = true;
        }
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            state = true;
            monsterSpawn = true;
            GameStart();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
