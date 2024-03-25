using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float characterSpeed;
    [SerializeField] private float monsterSpeed;

    [SerializeField] public string characterName;
    [SerializeField] public int attackLV;

    [SerializeField] public bool state = true;
    [SerializeField] public bool monsterSpawn = true;
    [SerializeField] public int weaponcount;
    [SerializeField] public int supportcount;

    [SerializeField] public int[] itemLvCheck;
    [SerializeField] public bool[] itemNumberCheck;

    [SerializeField] public CharacterNumber charNumber;
    public int charNum;

    [SerializeField] public GameObject player;

    [SerializeField] public int playChapterNumber;

    [SerializeField] public Vector2 canvasScaler;
    [SerializeField] public float[] volume;

    [SerializeField] public float pwsBuff;
    [SerializeField] public float tcBuff;

    [SerializeField] public int monsterCount;

    [SerializeField] public int ikuminCount;

    [SerializeField] public List<GameObject> subWeaponList;

    public float MonsterSpeed
    {
        get { return monsterSpeed; }
        set { monsterSpeed = value; }
    }
    public float CharacterSpeed
    {
        get { return characterSpeed * pwsBuff * tcBuff; }
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
        monsterCount = 0;
        ikuminCount = 0;
    }

    public void GameOver()
    {
        state = false;
        monsterSpawn = false;
        DataManager.instance.data.shopCoin += UIManager.instance.DropCoin;
        DataManager.instance.Save();
        Instantiate(Resources.Load<GameObject>("PreFabs/UI/GameOver"));
        attackLV = 1;
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;
        GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = canvasScaler;
        if(SceneManager.GetActiveScene().name == "PlayScene")
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
