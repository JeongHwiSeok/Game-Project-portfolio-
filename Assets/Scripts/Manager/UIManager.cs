using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image characterImage;

    [SerializeField] public Image[] weaponImage;
    [SerializeField] public Image[] supportImage;

    [SerializeField] public Text[] weaponLv;
    [SerializeField] public Text[] supportLv;

    public int weaponNumber;
    public int supportNumber;

    public int[,] weaponItem;
    public int[,] supportItem;

    [SerializeField] Slider expBar;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider shieldBar;

    [SerializeField] float playerLv;
    [SerializeField] float needExp;

    [SerializeField] GameObject lvDisplay;

    [SerializeField] Text timer;
    [SerializeField] public float time;
    private int minute;
    private int second;

    private int dropCoin;

    public bool flag;

    public float PlayerLv
    {
        get { return playerLv; }
    }
    public int DropCoin
    {
        set { dropCoin = value; }
        get { return dropCoin; }
    }

    public static UIManager instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        instance = this;
        needExp = 30;
        weaponItem = new int[6,2];
        supportItem = new int[6,2];
        flag = true;
        shieldBar.value = 0;
        characterImage.sprite = SpriteManager.instance.CharacterSprite(GameManager.instance.charNum);
        for (int i = 0; i < 6; i++)
        {
            weaponLv[i].gameObject.SetActive(false);
            supportLv[i].gameObject.SetActive(false);
        }
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (GameManager.instance.state)
        {
            time += Time.deltaTime;
        }  
        expBar.value = PlayerManager.instance.exp / needExp;
        hpBar.value = PlayerManager.instance.Hp / DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        if (PlayerManager.instance.MaxShield != 0)
        {
            shieldBar.value = PlayerManager.instance.Shield / PlayerManager.instance.MaxShield;
        }
        LvUP();
    }

    private void expCheck()
    {
        if(playerLv <= 6)
        {
            needExp = 100;
        }
        else if (playerLv <= 50)
        {
            needExp = Mathf.Pow(playerLv, 2) * ((((playerLv + 1) / 3) + 24) / 50);
        }
        else
        {
            needExp = Mathf.Pow(playerLv, 2) * ((playerLv + 14) / 50);
        }
        //else
        //{
        //    needExp = Mathf.Pow(playerLv, 3) * (((playerLv / 2) + 32) / 50);
        //}
    }

    public void LvUP()
    {
        expCheck();
        if(PlayerManager.instance.exp >= needExp)
        {
            if (flag)
            {
                playerLv++;
                PlayerManager.instance.exp -= needExp;
                Instantiate(lvDisplay);
                flag = false;
                GameManager.instance.state = false;
            }   
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            minute = (int)(time / 60);
            second = (int)(time % 60);
            timer.text = minute.ToString("00") + " : " + second.ToString("00");
            yield return null;
        }
    }
}
